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
                            _drawing.AddShape(new Arc(previousEntry.Clone(), new Entry(x, y), (int)nupArcSize.Value, lblDrawColor.BackColor, (float)nupDrawWidth.Value));

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
            string unit = Configuration.GetSetting(Constants.Document.MeasurementUnit) ?? "in";
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
                var s = JsonSerializer.CreateDefault();
                s.TypeNameHandling = TypeNameHandling.All;
                using (StreamReader sw = new StreamReader(fileName))
                using (JsonReader jr = new JsonTextReader(sw))
                {
                    _drawing = s.Deserialize<Drawing>(jr);

                    CreateNewSegment();
                }

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
            // I want to get the largest between the size of the screen and the size that needs to be drawn.

            Entry startPoint;
            Size requiredSize = _drawing.GetContainingRectangle(gridSize, out startPoint);
            Size saveSize = new Size(
                Math.Max(requiredSize.Width, drawSurface.Width),
                Math.Max(requiredSize.Height, drawSurface.Height));

            var saveRectangle = new System.Drawing.Rectangle(new Point(0, 0), saveSize);
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
                translatedDrawing.Draw(gridSize, g);

                //drawSurface.DrawToBitmap(bmp, saveRectangle);
                bmp.Save(fileName, format);
            }
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
            Configuration.SaveSetting(Constants.Measurement.BelowOrAbove, cboHorizontalAlignment.SelectedItem.ToString());
            Configuration.SaveSetting(Constants.Measurement.LeftOrRight, cboVerticalAlignment.SelectedItem.ToString());
            Configuration.SaveSetting(Constants.Measurement.Color, lblMeasureColor.BackColor.ToArgb().ToString());

            Configuration.SaveSetting(Constants.Draw.Color, lblDrawColor.BackColor.ToArgb().ToString());
            Configuration.SaveSetting(Constants.Draw.Width, nupDrawWidth.Value.ToString());
            Configuration.SaveSetting(Constants.Draw.DrawObject, cboDrawElements.SelectedItem.ToString());

            Configuration.SaveSetting(Constants.Text.Color, lblTextColor.BackColor.ToArgb().ToString());
            Configuration.SaveSetting(Constants.Text.FontName, _textFont.Name);
            Configuration.SaveSetting(Constants.Text.FontSize, _textFont.Size.ToString());
            Configuration.SaveSetting(Constants.Text.FontStyle, _textFont.Style.ToString());

            Configuration.SaveSetting(Constants.Draw.Arc.Radius, nupArcSize.Value.ToString());
        }

        private void LoadUserPreferences()
        {
            // Measure
            cboHorizontalAlignment.SelectedItem = Configuration.GetSettingOrDefault<MeasurementLocation>(Constants.Measurement.BelowOrAbove, Enum.TryParse<MeasurementLocation>, MeasurementLocation.Below);
            cboVerticalAlignment.SelectedItem = Configuration.GetSettingOrDefault<MeasurementLocation>(Constants.Measurement.LeftOrRight, Enum.TryParse<MeasurementLocation>, MeasurementLocation.Left);
            lblMeasureColor.BackColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Measurement.Color, int.TryParse, unchecked((int)0xff008000))); //  green

            // Draw
            cboDrawElements.SelectedItem = Configuration.GetSettingOrDefault<DrawObject>(Constants.Draw.DrawObject, Enum.TryParse<DrawObject>, DrawObject.Line);
            lblDrawColor.BackColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Draw.Color, int.TryParse, unchecked((int)0xff000000))); // black
            nupDrawWidth.Value = Configuration.GetSettingOrDefault<int>(Constants.Draw.Width, int.TryParse, 2);

            // Text
            lblTextColor.BackColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Text.Color, int.TryParse, unchecked((int)0xff000000))); // black
            float fontSize = Configuration.GetSettingOrDefault<float>(Constants.Text.FontSize, float.TryParse, 10f);
            string fontName = Configuration.GetSetting(Constants.Text.FontName) ?? "Calibri";
            FontStyle fontStyle = Configuration.GetSettingOrDefault<FontStyle>(Constants.Text.FontStyle, Enum.TryParse<FontStyle>, FontStyle.Regular);
            _textFont = new Font(fontName, fontSize, fontStyle);
            lblFont.Text = string.Format("{0}, {1} {2}", _textFont.Name, _textFont.Size, _textFont.Style);

            // Arc
            nupArcSize.Value = Configuration.GetSettingOrDefault<int>(Constants.Draw.Arc.Radius, int.TryParse, 4);
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
    }
}

