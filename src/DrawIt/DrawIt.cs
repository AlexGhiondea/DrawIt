using DrawIt.Objects;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
                sfd.Filter = "*.dit|*.dit";
                if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    _drawing.Save(fileName);
                    tssActiveStatus.Text = "Saved " + fileName;
                }
            }
        }

        private List<Segment> _segmentsToMove = null;

        private void drawSurface_MouseDown(object sender, MouseEventArgs e)
        {
            tssActiveStatus.Text = string.Empty;

            // if right click, cancel the previous segment and start a new one.
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (_state == EditorState.Draw)
                {
                    CreateNewSegment();
                }
                else if (_state == EditorState.Measure)
                {
                    previousEntry = null;
                }

                drawSurface.Refresh();
                return;
            }

            var x = SnapToClosest(e.X);
            var y = SnapToClosest(e.Y);

            if (_state == EditorState.Move)
            {
                Cursor = Cursors.SizeAll;

                // see if there is anything under the cursor at this point.
                _segmentsToMove = _drawing.GetSegmentAtPoint(new Point(e.X, e.Y), gridSize);
            }

            if (_state == EditorState.Delete)
            {
                // delete
                _drawing.RemoveSegmentAtPoint(new Point(e.X, e.Y), gridSize);

                drawSurface.Refresh();
                return;
            }

            // if we are not in a delete state, but this is the first point down,
            // record it and move on
            if (previousEntry == null)
            {
                previousEntry = new Entry(x, y);
                return;
            }

            if (_state == EditorState.Draw)
            {
                DrawObject obj = (DrawObject)cboDrawElements.SelectedItem;
                AddObjectToDrawing(x, y, obj);
            }
            else if (_state == EditorState.Measure)
            {
                var measurement = new Measurement()
                {
                    Start = previousEntry,
                    End = new Entry(x, y),
                    Location = ((MeasurementLocation)cboHorizontalAlignment.SelectedItem) | ((MeasurementLocation)cboVerticalAlignment.SelectedItem),
                    Color = lblMeasureColor.BackColor
                };

                _drawing.AddMeasurement(measurement);

                // for measurements always reset
                previousEntry = null;
            }

            drawSurface.Refresh();
        }

        private void AddObjectToDrawing(int x, int y, DrawObject obj)
        {
            switch (obj)
            {
                case DrawObject.Line:
                    {
                        // create a line from the 2 points.
                        _drawing.AddToCurrentSegment(new Line()
                        {
                            Start = previousEntry.Clone(),
                            End = new Entry(x, y),
                            Color = lblDrawColor.BackColor,
                            Width = (float)nupDrawWidth.Value
                        });

                        previousEntry = new Entry(x, y);
                        break;
                    }
                case DrawObject.Rectangle:
                    {
                        // we need to create 4 segments.

                        _drawing.AddToCurrentSegment(new Line()
                        {
                            Start = previousEntry.Clone(),
                            End = new Entry(previousEntry.X, y),
                            Color = lblDrawColor.BackColor,
                            Width = (float)nupDrawWidth.Value
                        });

                        _drawing.AddToCurrentSegment(new Line()
                        {
                            Start = previousEntry.Clone(),
                            End = new Entry(x, previousEntry.Y),
                            Color = lblDrawColor.BackColor,
                            Width = (float)nupDrawWidth.Value
                        });

                        _drawing.AddToCurrentSegment(new Line()
                        {
                            Start = new Entry(previousEntry.X, y),
                            End = new Entry(x, y),
                            Color = lblDrawColor.BackColor,
                            Width = (float)nupDrawWidth.Value
                        });

                        _drawing.AddToCurrentSegment(new Line()
                        {
                            Start = new Entry(x, previousEntry.Y),
                            End = new Entry(x, y),
                            Color = lblDrawColor.BackColor,
                            Width = (float)nupDrawWidth.Value
                        });

                        previousEntry = null;
                        break;
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

        private void drawSurface_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(this.Size, e.Graphics);

            _drawing.Draw(gridSize, e.Graphics);
        }

        private void DrawGrid(Size rectangle, Graphics g)
        {
            int w = rectangle.Width;
            int h = rectangle.Height;

            for (int i = 0; i < h; i += gridSize)
            {
                g.DrawLine(s_gridPen, new Point(0, i), new Point(w, i));
            }

            for (int i = 0; i < w; i += gridSize)
            {
                g.DrawLine(s_gridPen, new Point(i, 0), new Point(i, h));
            }
        }

        private void btnNewSegment_Click(object sender, EventArgs e)
        {
            // make sure the temporary line is gone.
            drawSurface.Refresh();
            CreateNewSegment();
        }

        private void CreateNewSegment()
        {
            previousEntry = null;
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
            else if (radioButton == rbMove) _state = EditorState.Move;
            else throw new InvalidOperationException("Unknown state");

            // get rid of temporary line
            drawSurface.Refresh();

            switch (_state)
            {
                case EditorState.Draw:
                    {
                        grpDraw.Visible = true;
                        grpMeasure.Visible = false;

                        CreateNewSegment();
                        break;
                    }
                case EditorState.Measure:
                    {
                        grpDraw.Visible = false;
                        grpMeasure.Visible = true;

                        previousEntry = null;
                        break;
                    }
                case EditorState.Move:
                case EditorState.Delete:
                    {
                        previousEntry = null;
                        grpDraw.Visible = grpMeasure.Visible = false;
                        break;
                    }
            }
        }

        private void DrawIt_Load(object sender, EventArgs e)
        {
            // setup the 2 combo-boxes for measurements.
            cboHorizontalAlignment.Items.Add(MeasurementLocation.Above);
            cboHorizontalAlignment.Items.Add(MeasurementLocation.Below);
            cboHorizontalAlignment.SelectedItem = MeasurementLocation.Below;

            cboVerticalAlignment.Items.Add(MeasurementLocation.Left);
            cboVerticalAlignment.Items.Add(MeasurementLocation.Right);
            cboVerticalAlignment.SelectedItem = MeasurementLocation.Right;

            // populate the list of objects it can draw.
            Array enumValues = Enum.GetValues(typeof(DrawObject));

            IEnumerator enumerator = enumValues.GetEnumerator();

            while (enumerator.MoveNext())
            {
                cboDrawElements.Items.Add(enumerator.Current);
            }
            cboDrawElements.SelectedItem = DrawObject.Line;

            CreateNewDocument(new Drawing(1, "in"));

            drawSurface.MouseWheel += drawSurface_MouseWheel;

        }

        void drawSurface_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (gridSize < tbZoom.Maximum && tbZoom.Value + 1 <= tbZoom.Maximum)
                {
                    tbZoom.Value++;
                    gridSize = tbZoom.Value;
                }
            }
            else if (e.Delta < 0)
            {
                if (gridSize > tbZoom.Minimum && tbZoom.Value - 1 > 0)
                {
                    tbZoom.Value--;
                    gridSize = tbZoom.Value;
                }
            }

            drawSurface.Refresh();
        }

        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            gridSize = tbZoom.Value;
            drawSurface.Refresh();
        }

        private void drawSurface_MouseMove(object sender, MouseEventArgs e)
        {
            var gridX = SnapToClosest(e.X);
            var gridY = SnapToClosest(e.Y);

            if (previousEntry != null)
            {
                //redraw the panel to get rid of the temporary line
                drawSurface.Refresh();
            }

            string extraInfo = string.Empty;
            if (previousEntry != null)
            {
                // at this point we have already clicked once on the surface
                int deltaX = gridX - previousEntry.X;
                int deltaY = gridY - previousEntry.Y;
                extraInfo = string.Format("(From previous: X:{0}{2}, Y:{1}{2})", deltaX * _drawing.ConversionRatio, deltaY * _drawing.ConversionRatio, _drawing.Unit);
            }

            // draw a temporary line to help with drawing.
            if (previousEntry != null)
            {
                drawSurface.CreateGraphics().DrawLine(_tempLinePen, previousEntry.ToPoint(gridSize), new Entry(gridX, gridY).ToPoint(gridSize));
            }

            stsPosition.Text = string.Format("X: {0}, Y: {1} {2}", gridX, gridY, extraInfo);
        }

        Pen _tempLinePen = new Pen(new SolidBrush(Color.Gray), 1);

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
                using (StreamReader sw = new StreamReader(fileName))
                using (JsonReader jr = new JsonTextReader(sw))
                {
                    _drawing = s.Deserialize<Drawing>(jr);

                    CreateNewSegment();
                    drawSurface.Refresh();
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
            drawSurface.Refresh();

            stsDocData.Text = string.Format("Unit: {0}, Ratio: {1}", _drawing.Unit, _drawing.ConversionRatio);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewDocumentDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }

        private void DrawIt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (rtbDraw.Checked)
                {
                    CreateNewSegment();
                }
                else
                {
                    previousEntry = null;
                }

                drawSurface.Refresh();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_drawing.HasChanges())
            {
                if (MessageBox.Show("Exit without saving changes?", "Are you sure?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }
            this.Close();
        }

        private void drawSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (_state == EditorState.Move)
            {
                if (previousEntry == null)
                {
                    return;
                }

                var newX = SnapToClosest(e.X);
                var newY = SnapToClosest(e.Y);

                var translateX = -(previousEntry.X - newX);
                var translateY = -(previousEntry.Y - newY);

                Debug.WriteLine(translateX);
                Debug.WriteLine(translateY);

                // do we move everything or just a few segments?

                if (_segmentsToMove == null)
                {
                    _drawing.TranslateDrawing(translateX, translateY);
                }
                else
                {
                    _drawing.TranslateSegments(_segmentsToMove, translateX, translateY);
                }


                previousEntry = null;

                Cursor = Cursors.Default;

                drawSurface.Refresh();
            }
        }

        private void jpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage(ImageFormat.Jpeg);
        }

        private void bmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage(ImageFormat.Bmp);
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
            Size requiredSize = _drawing.GetContainingRectangle(gridSize);
            Size saveSize = new Size(
                Math.Max(requiredSize.Width, drawSurface.Width),
                Math.Max(requiredSize.Height, drawSurface.Height));

            var saveRectangle = new Rectangle(new Point(0, 0), saveSize);
            using (Bitmap bmp = new Bitmap(saveRectangle.Width, saveRectangle.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // start  with a white background.
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                DrawGrid(saveRectangle.Size, g);
                _drawing.Draw(gridSize, g);

                //drawSurface.DrawToBitmap(bmp, saveRectangle);
                bmp.Save(fileName, format);
            }
        }

        private void drawSurface_MouseEnter(object sender, EventArgs e)
        {
            drawSurface.Focus();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog(this);
        }

        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage(ImageFormat.Png);
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
        }
    }
}

