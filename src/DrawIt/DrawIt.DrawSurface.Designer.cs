using DrawIt.Objects;
using DrawIt.Objects.Shapes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DrawIt
{
    public partial class DrawIt
    {
        private void drawSurface_MouseDown(object sender, MouseEventArgs e)
        {
            tssActiveStatus.Text = string.Empty;

            // if right click, cancel the previous segment and start a new one.
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CreateNewSegment();
                return;
            }

            var x = SnapToClosest(e.X);
            var y = SnapToClosest(e.Y);

            if (_state == EditorState.Move)
            {
                Cursor = Cursors.SizeAll;

                // see if there is anything under the cursor at this point.
                _shapesToMove = _drawing.GetSegmentAtPoint(new Point(e.X, e.Y), gridSize);
            }

            if (_state == EditorState.Delete)
            {
                // delete
                _drawing.RemoveSegmentAtPoint(new Point(e.X, e.Y), gridSize);

                drawSurface.Refresh();
                return;
            }
            else if (_state == EditorState.Text)
            {
                if (!string.IsNullOrEmpty(txtTextToDraw.Text))
                {
                    var g = drawSurface.CreateGraphics();
                    g.PageUnit = GraphicsUnit.Pixel;
                    var textSize = g.MeasureString(txtTextToDraw.Text, _textFont);

                    int txtX = SnapToClosest(e.X + (int)Math.Round(textSize.Width, MidpointRounding.AwayFromZero));
                    int txtY = SnapToClosest(e.Y + (int)Math.Round(textSize.Height, MidpointRounding.AwayFromZero));

                    var end = new Entry(txtX, txtY - 1);

                    _drawing.Shapes.Add(new Text(new Entry(x, y - 1), end, txtTextToDraw.Text, _textFont.Size, _textFont.Name, _textFont.Style, lblTextColor.BackColor));
                    drawSurface.Refresh();
                }
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
                var measurement = new Measurement(previousEntry, new Entry(x, y), lblMeasureColor.BackColor,
                    ((MeasurementLocation)cboHorizontalAlignment.SelectedItem) | ((MeasurementLocation)cboVerticalAlignment.SelectedItem),
                    _drawing.ConversionRatio,
                    _drawing.Unit
                );

                _drawing.AddMeasurement(measurement);

                // for measurements always reset
                previousEntry = null;
            }
            else if (_state == EditorState.Image)
            {
                string file = txtImagePath.Text;

                if (!string.IsNullOrWhiteSpace(file) && File.Exists(file))
                {
                    var img = new Image(previousEntry, new Entry(x, y), Convert.ToBase64String(File.ReadAllBytes(file)));
                    _drawing.AddImage(img);
                }

                // for image drawing always reset
                previousEntry = null;
            }

            drawSurface.Refresh();
        }

        private void drawSurface_MouseEnter(object sender, EventArgs e)
        {
            drawSurface.Focus();
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
        private void drawSurface_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
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

                if (_shapesToMove == null)
                {
                    _drawing.TranslateDrawing(translateX, translateY);
                }
                else
                {
                    _drawing.TranslateSegments(_shapesToMove, translateX, translateY);
                }


                previousEntry = null;

                Cursor = Cursors.Default;

                drawSurface.Refresh();
            }
        }
    }
}
