﻿using DrawIt.Objects;
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
                sfd.Filter = "DrawIt project (*.dit)|*.dit";
                if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    _drawing.Save(fileName);
                    tssActiveStatus.Text = "Saved " + fileName;
                }
            }
        }

        private List<Segment> _segmentsToMove = null;

        private void AddObjectToDrawing(int x, int y, DrawObject obj)
        {
            switch (obj)
            {
                case DrawObject.Line:
                    {
                        // create a line from the 2 points.
                        _drawing.AddSegment(new Line(previousEntry.Clone(), new Entry(x, y), lblDrawColor.BackColor, (float)nupDrawWidth.Value));

                        previousEntry = new Entry(x, y);
                        break;
                    }
                case DrawObject.Rectangle:
                    {
                        // we need to create 4 segments.
                        _drawing.AddSegment(new Line(previousEntry.Clone(), new Entry(previousEntry.X, y), lblDrawColor.BackColor, (float)nupDrawWidth.Value));
                        _drawing.AddSegment(new Line(previousEntry.Clone(), new Entry(x, previousEntry.Y), lblDrawColor.BackColor, (float)nupDrawWidth.Value));
                        _drawing.AddSegment(new Line(new Entry(previousEntry.X, y), new Entry(x, y), lblDrawColor.BackColor, (float)nupDrawWidth.Value));
                        _drawing.AddSegment(new Line(new Entry(x, previousEntry.Y), new Entry(x, y), lblDrawColor.BackColor, (float)nupDrawWidth.Value));

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
                // start  with a white background.
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                DrawGrid(saveRectangle.Size, g);

                // we need to translate the drawing if we have entries that are hidden

                _drawing.DrawWithTranslation(gridSize, g, startPoint);

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
        }
    }
}

