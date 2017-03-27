using DrawIt.Objects;
using DrawIt.Objects.Shapes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawIt
{
    public partial class DrawIt : Form
    {
        private int gridSize = 15;
        private Drawing _drawing;
        private static Pen s_gridPen = new Pen(new SolidBrush(Color.LightGray), 1);
        private static Entry previousEntry = null;

        private EditorState _state = EditorState.Draw;

        public DrawIt()
        {
            InitializeComponent();

            // Enable double buffering for the drawing surface
            typeof(Panel).InvokeMember("DoubleBuffered",
               BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
               null, drawSurface, new object[] { true });
        }

        private void SaveProject()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = ".dit";
                sfd.Filter = "DrawIt project (*.dit)|*.dit";
                if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    _drawing.Save(fileName);
                    tssActiveStatus.Text = "Saved " + fileName;
                }
            }
        }

        private List<Shape> _shapesToMove = null;

        private void AddObjectToDrawing(int x, int y, DrawObject obj)
        {
            switch (obj)
            {
                case DrawObject.Line:
                    {
                        // only add it if we have something to add.
                        if (previousEntry.X - x != 0 || previousEntry.Y - y != 0)
                        {
                            // create a line from the 2 points.
                            _drawing.AddShape(new Line(previousEntry.Clone(), new Entry(x, y), lblDrawColor.BackColor, (float)nupDrawWidth.Value));

                            previousEntry = new Entry(x, y);
                        }
                        break;
                    }
                case DrawObject.Arc:
                    {
                        // only add it if we have something to add.
                        if (previousEntry.X - x != 0 || previousEntry.Y - y != 0)
                        {
                            // create an arc from the 2 points and the specified width
                            _drawing.AddShape(new Arc(previousEntry.Clone(), new Entry(x, y), (double)nupArcSize.Value / _drawing.ConversionRatio, lblDrawColor.BackColor, (float)nupDrawWidth.Value));

                            previousEntry = null;
                        }
                        break;
                    }
                case DrawObject.Rectangle:
                    {
                        // only add it if we have something there..
                        if (previousEntry.X - x == 0 || previousEntry.Y - y == 0)
                        {
                            return;
                        }

                        _drawing.AddShape(new Rectangle(previousEntry.Clone(), new Entry(x, y), (float)nupDrawWidth.Value, lblDrawColor.BackColor));
                        previousEntry = null;
                        break;
                    }
                case DrawObject.Circle:
                    {
                        int deltaX = x - previousEntry.X;
                        int deltaY = y - previousEntry.Y;

                        int radius = (int)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                        if (deltaX == 0 && deltaY == 0)
                        {
                            // we can't draw a circle with 0 radius
                            return;
                        }

                        _drawing.AddShape(new Circle(previousEntry.Clone(), radius, (float)nupDrawWidth.Value, lblDrawColor.BackColor));

                        previousEntry = null;
                        break; ;
                    }

                default:
                    break;
            }
        }

        private int SnapToClosest(int actual)
        {
            int whole = actual / gridSize;
            int extra = (int)Math.Round((double)(actual % gridSize) / gridSize);

            return (whole + extra);
        }

        private void btnNewSegment_Click(object sender, EventArgs e)
        {
            // make sure the temporary line is gone.
            CreateNewSegment();
        }

        private void CreateNewSegment()
        {
            previousEntry = null;
            drawSurface.Refresh();
        }

        private void ShowNewDocumentDialog()
        {
            if (_drawing.HasChanges())
            {
                if (MessageBox.Show("Abandon changes?", "Are you sure?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            NewDrawing nd = new NewDrawing(_drawing);
            if (nd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreateNewDocument(nd.Document);
            }
        }

        private void rtbDraw_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (!radioButton.Checked)
            {
                // transitions to the false state are not interesting.
                return;
            }

            // update current state
            if (radioButton == rtbDraw) _state = EditorState.Draw;
            else if (radioButton == rtbMeasure) _state = EditorState.Measure;
            else if (radioButton == rtbDelete) _state = EditorState.Delete;
            else if (radioButton == rtbMove) _state = EditorState.Move;
            else if (radioButton == rtbText) _state = EditorState.Text;
            else throw new InvalidOperationException("Unknown state");

            // get rid of temporary line
            drawSurface.Refresh();

            switch (_state)
            {
                case EditorState.Draw:
                    {
                        ShowControl(grpDraw);
                        CreateNewSegment();
                        break;
                    }
                case EditorState.Measure:
                    {
                        ShowControl(grpMeasure);
                        previousEntry = null;
                        break;
                    }
                case EditorState.Text:
                    {
                        ShowControl(grpText);
                        previousEntry = null;
                        break;
                    }
                case EditorState.Move:
                case EditorState.Delete:
                    {
                        previousEntry = null;

                        ShowControl<GroupBox>(null);

                        break;
                    }
            }
        }

        private void ShowControl<TControl>(TControl control)
            where TControl : Control
        {
            foreach (var item in this.Controls.OfType<TControl>())
            {
                if (item == control)
                {
                    item.Visible = true;
                }
                else
                {
                    item.Visible = false;
                }
            }
        }

        private void DrawIt_Load(object sender, EventArgs e)
        {
            // setup the 2 combo-boxes for measurements.
            cboHorizontalAlignment.Items.Add(MeasurementLocation.Above);
            cboHorizontalAlignment.Items.Add(MeasurementLocation.Below);

            cboVerticalAlignment.Items.Add(MeasurementLocation.Left);
            cboVerticalAlignment.Items.Add(MeasurementLocation.Right);

            // populate the list of objects it can draw.
            Array enumValues = Enum.GetValues(typeof(DrawObject));

            IEnumerator enumerator = enumValues.GetEnumerator();
            while (enumerator.MoveNext())
            {
                cboDrawElements.Items.Add(enumerator.Current);
            }

            LoadUserPreferences();

            // Read the configuration defaults from the app.config.
            string unit = Configuration.GetSetting(Constants.Document.MeasurementUnit) ?? "square foot";
            float conversionRate = Configuration.GetSettingOrDefault<float>(Constants.Document.ConversionRate, float.TryParse, 1);

            CreateNewDocument(new Drawing(conversionRate, unit));

            drawSurface.MouseWheel += drawSurface_MouseWheel;

        }

        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            gridSize = tbZoom.Value;
            drawSurface.Refresh();
        }

        private void ShowOpenFileDialog()
        {
            if (_drawing.HasChanges())
            {
                if (MessageBox.Show("Abandon changes?", "Are you sure?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.CheckFileExists = true;
                ofd.Filter = "DrawIt project (*.dit)|*.dit";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = ofd.FileName;

                    OpenDrawing(fileName);
                }
            }
        }

        private void OpenDrawing(string fileName)
        {
            try
            {
                _drawing = FileHelpers.LoadObjectFromDisk<Drawing>(fileName);

                stsDocData.Text = string.Format("1 square = {1} {0}", _drawing.Unit, _drawing.ConversionRatio);
                //setup data bindings
                lblNupArcUnits.DataBindings.Clear();
                lblNupArcUnits.DataBindings.Add("Text", _drawing, "Unit", false, DataSourceUpdateMode.OnPropertyChanged);

                CreateNewSegment();

                tssActiveStatus.Text = "Loaded " + fileName;
            }
            catch
            {
                MessageBox.Show("Could not load file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PickColorLabel_Click(object sender, EventArgs e)
        {
            PickColor(sender as Label);
        }

        private void PickColor(Label sender)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sender.BackColor = cd.Color;
            }
        }

        private void CreateNewDocument(Drawing newDrawing)
        {
            _drawing = newDrawing;
            CreateNewSegment();

            stsDocData.Text = string.Format("1 square = {1} {0}", _drawing.Unit, _drawing.ConversionRatio);
            //setup data bindings
            lblNupArcUnits.DataBindings.Clear();
            lblNupArcUnits.DataBindings.Add("Text", _drawing, "Unit", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void DrawIt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CreateNewSegment();
            }
        }

        private void ExportImage(ImageFormat format)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = format.ToString();
                sfd.Filter = string.Format("*.{0}|*.{0}", format.ToString());
                if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = sfd.FileName;

                    DrawToFile(format, fileName);

                    tssActiveStatus.Text = "Saved " + fileName;
                }
            }

        }

        private void DrawToFile(ImageFormat format, string fileName)
        {
            // add the logo
            int LogoHeight = Configuration.GetSettingOrDefault(Constants.Application.Logo.Height, int.TryParse, 0); // default to zero if it is not set.
            string LogoEncodedImage = Configuration.GetSetting(Constants.Application.Logo.Image) ?? string.Empty;
            int headerHeight = 0; // we don't assume we have a header

            if (LogoHeight > 0)
            {
                headerHeight = LogoHeight + 2; //we add 2 to provide space below and above for the logo.
            }

            // I want to get the largest between the size of the screen and the size that needs to be drawn.

            Entry startPoint;
            var saveRectangle = ComputeSaveRectangle(headerHeight, out startPoint);

            using (Bitmap bmp = new Bitmap(saveRectangle.Width, saveRectangle.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // anti-aliasing
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // start  with a white background.
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                DrawGrid(saveRectangle.Size, g);

                // we need to translate the drawing if we have entries that are hidden
                Drawing translatedDrawing = _drawing.Clone();
                translatedDrawing.TranslateDrawing(-startPoint.X, -startPoint.Y);

                // If we have a header, add it
                if (headerHeight > 0)
                {
                    //translate everything so that we have space for the logo
                    translatedDrawing.TranslateDrawing(0, headerHeight);

                    // white out the place for the logo
                    g.FillRectangle(Brushes.White, 0, 0, bmp.Width, headerHeight * gridSize);

                    AddLogoShapesToDocument(LogoHeight, LogoEncodedImage, headerHeight, saveRectangle, translatedDrawing, g);
                }

                translatedDrawing.Draw(gridSize, g);
                //drawSurface.DrawToBitmap(bmp, saveRectangle);
                bmp.Save(fileName, format);
            }
        }

        private void AddLogoShapesToDocument(int LogoHeight, string LogoEncodedImage, int headerHeight, System.Drawing.Rectangle saveRectangle, Drawing translatedDrawing, Graphics g)
        {
            // add a line between the header and the actual drawing.
            translatedDrawing.AddShape(new Line(new Entry(0, headerHeight), new Entry(saveRectangle.Width / gridSize, headerHeight), Color.DarkGray, 2));

            if (!string.IsNullOrEmpty(LogoEncodedImage))
            {
                translatedDrawing.AddShape(new LogoImage(new Entry(1, 1), new Entry(1, 1), LogoEncodedImage, LogoHeight));
            }

            string headerText = Configuration.GetSetting(Constants.Application.Header.Text) ?? string.Empty;

            if (!string.IsNullOrEmpty(headerText))
            {
                Font headerFont = FontHelpers.GetHeaderFont();
                Color color = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Application.Header.TextColor, int.TryParse, Constants.Measurement.Defaults.Black));
                //sadly, we need to create the image here as well, to see how big it is...
                // consider moving this in the logoimage

                int newWidth = GetLogoWidth(LogoHeight, LogoEncodedImage);

                // the size of the remaining space is:
                int remainingWidth = saveRectangle.Width - newWidth;

                var stringSize = g.MeasureString(headerText, headerFont);

                //compute the top corner for the text.
                float x = ((newWidth / gridSize) * gridSize) + (remainingWidth - stringSize.Width) / 2;
                float y = ((headerHeight * gridSize) - stringSize.Height) / 2;

                var top = new Point((int)x, (int)y).ToEntry(gridSize);
                var bottom = new Entry(top.X + (int)(stringSize.Width / gridSize), top.Y + (int)(stringSize.Height / gridSize));

                translatedDrawing.AddShape(new Text(top, bottom, headerText, headerFont.Size, headerFont.FontFamily.Name, headerFont.Style, color));
            }
        }

        private int GetLogoWidth(int LogoHeight, string LogoEncodedImage)
        {
            if (string.IsNullOrEmpty(LogoEncodedImage))
                return 0;

            int newWidth;
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(LogoEncodedImage)))
            using (Bitmap bmp = new Bitmap(ms))
            {
                int allowedHeight = LogoHeight * gridSize;
                double scaleFactor = (double)bmp.Height / (double)allowedHeight;

                newWidth = (int)(bmp.Width / scaleFactor);
            }

            return newWidth;
        }

        private System.Drawing.Rectangle ComputeSaveRectangle(int headerHeight, out Entry startPoint)
        {
            Size requiredSize = _drawing.GetContainingRectangleForSaving(gridSize, out startPoint, headerHeight);

            // the saveSize is the max between the size shown on the screen and the size of the rectangle containing the drawing.
            Size saveSize = new Size(
                Math.Max(requiredSize.Width, drawSurface.Width),
                Math.Max(requiredSize.Height, drawSurface.Height));

            // we want to end at a grid boundary -- this makes it look a bit nicer.
            saveSize.Height = (saveSize.Height / gridSize) * gridSize;
            saveSize.Width = (saveSize.Width / gridSize) * gridSize;

            // create the saveRectangle
            var saveRectangle = new System.Drawing.Rectangle(new Point(0, 0), saveSize);
            return saveRectangle;
        }

        private void DrawIt_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length == 1)
            {
                if (_drawing.HasChanges())
                {
                    if (MessageBox.Show("Abandon changes?", "Are you sure?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                }

                OpenDrawing(files[0]);
            }
        }

        private void DrawIt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void cboDrawElements_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_drawing != null)
                CreateNewSegment();

            // if drawing an arc, show the control.
            // if drawing a line, show the new button
            DrawObject obj = (DrawObject)cboDrawElements.SelectedItem;
            lblNupArcUnits.Visible = false;
            nupArcSize.Visible = false;
            lblNupArcDescription.Visible = false;
            if (obj == DrawObject.Arc)
            {
                nupArcSize.Visible = true;
                lblNupArcUnits.Visible = true;
                lblNupArcDescription.Visible = true;
            }
        }

        private void DrawIt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_drawing.HasChanges())
            {
                if (MessageBox.Show("Save changes to the document?", "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            // save settings.
            SaveUserPreferences();
        }

        private void SaveUserPreferences()
        {
            Configuration.SetSetting(Constants.Measurement.BelowOrAbove, cboHorizontalAlignment.SelectedItem.ToString());
            Configuration.SetSetting(Constants.Measurement.LeftOrRight, cboVerticalAlignment.SelectedItem.ToString());
            Configuration.SetSetting(Constants.Measurement.Color, lblMeasureColor.BackColor.ToArgb().ToString());

            Configuration.SetSetting(Constants.Draw.Color, lblDrawColor.BackColor.ToArgb().ToString());
            Configuration.SetSetting(Constants.Draw.Width, nupDrawWidth.Value.ToString());
            Configuration.SetSetting(Constants.Draw.DrawObject, cboDrawElements.SelectedItem.ToString());

            Configuration.SetSetting(Constants.Text.Color, lblTextColor.BackColor.ToArgb().ToString());
            Configuration.SetSetting(Constants.Text.FontName, _textFont.Name);
            Configuration.SetSetting(Constants.Text.FontSize, _textFont.Size.ToString());
            Configuration.SetSetting(Constants.Text.FontStyle, _textFont.Style.ToString());

            Configuration.SetSetting(Constants.Draw.Arc.Radius, nupArcSize.Value.ToString());
            Configuration.SaveToDisk();
        }

        private void LoadUserPreferences()
        {
            // Measure
            cboHorizontalAlignment.SelectedItem = Configuration.GetSettingOrDefault<MeasurementLocation>(Constants.Measurement.BelowOrAbove, Enum.TryParse<MeasurementLocation>, MeasurementLocation.Below);
            cboVerticalAlignment.SelectedItem = Configuration.GetSettingOrDefault<MeasurementLocation>(Constants.Measurement.LeftOrRight, Enum.TryParse<MeasurementLocation>, MeasurementLocation.Left);
            lblMeasureColor.BackColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Measurement.Color, int.TryParse, Constants.Measurement.Defaults.Green));

            // Draw
            cboDrawElements.SelectedItem = Configuration.GetSettingOrDefault<DrawObject>(Constants.Draw.DrawObject, Enum.TryParse<DrawObject>, DrawObject.Line);
            lblDrawColor.BackColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Draw.Color, int.TryParse, Constants.Measurement.Defaults.Black));
            nupDrawWidth.Value = Configuration.GetSettingOrDefault<int>(Constants.Draw.Width, int.TryParse, Constants.Measurement.Defaults.DrawWidth);

            // Text
            lblTextColor.BackColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Text.Color, int.TryParse, Constants.Measurement.Defaults.Black));
            float fontSize = Configuration.GetSettingOrDefault<float>(Constants.Text.FontSize, float.TryParse, Constants.Measurement.Defaults.FontSize);
            string fontName = Configuration.GetSetting(Constants.Text.FontName) ?? Constants.Measurement.Defaults.FontFamily;
            FontStyle fontStyle = Configuration.GetSettingOrDefault<FontStyle>(Constants.Text.FontStyle, Enum.TryParse<FontStyle>, FontStyle.Regular);
            _textFont = new Font(fontName, fontSize, fontStyle);
            lblFont.Text = string.Format("{0}, {1} {2}", _textFont.Name, _textFont.Size, _textFont.Style);

            // Arc
            nupArcSize.Value = Configuration.GetSettingOrDefault<int>(Constants.Draw.Arc.Radius, int.TryParse, Constants.Measurement.Defaults.ArcSize);
        }

        private Font _textFont = null;

        private void lblFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = _textFont;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _textFont = fd.Font;
                lblFont.Text = string.Format("{0}, {1} {2}", _textFont.Name, _textFont.Size, _textFont.Style);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.ShowDialog(this);
        }
    }
}

