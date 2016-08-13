using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DrawIt
{
    public class Drawing
    {
        private bool _hasChanges;
        public List<Shape> Shapes;

        public string Unit;
        public double ConversionRatio;

        public bool HasChanges() { return _hasChanges; }

        public Drawing(double ratio, string unit)
        {
            ConversionRatio = ratio;
            Unit = unit;

            Shapes = new List<Shape>();
        }

        internal void AddShape(Shape shape)
        {
            Shapes.Add(shape);
            _hasChanges = true;
        }

        internal void RemoveSegmentAtPoint(Point point, int gridSize)
        {
            var segment = GetSegmentAtPoint(point, gridSize);
            if (segment == null)
            {
                return;
            }

            foreach (var toRemove in segment)
            {
                Shapes.Remove(toRemove);
                _hasChanges = true;
            }
        }

        internal List<Shape> GetSegmentAtPoint(Point point, int gridSize)
        {
            List<Shape> segments = new List<Shape>();
            for (int i = 0; i < Shapes.Count; i++)
            {
                if (Shapes[i].ContainsPoint(gridSize, point))
                {
                    segments.Add(Shapes[i]);
                }
            }

            return segments.Count > 0 ? segments : null;
        }

        internal void AddMeasurement(Measurement measurement)
        {
            if (measurement.Start.Equals(measurement.End) || (measurement.Start.X != measurement.End.X && measurement.Start.Y != measurement.End.Y))
            {
                return;
            }

            Shapes.Add(measurement);

            _hasChanges = true;
        }

        internal void Draw(int gridSize, Graphics g)
        {
            DrawUserSegments(gridSize, g);
        }

        internal Drawing Clone()
        {
            Drawing newDrawing = new Drawing(this.ConversionRatio, this.Unit);
            foreach (var item in Shapes)
            {
                // measurement derives from line, so check it first
                // TODO: implement clone at the shape level
                if (item is Measurement)
                {
                    Measurement m = item as Measurement;
                    newDrawing.AddMeasurement(new Measurement(m.Start.Clone(), m.End.Clone(), m.Color, m.Location, m.ConversionRate, m.Unit));
                }
                else if (item is Line)
                {
                    Line l = item as Line;
                    newDrawing.AddShape(new Line(l.Start.Clone(), l.End.Clone(), l.Color, l.Width));
                }
            }

            return newDrawing;
        }

        private void DrawUserSegments(int gridSize, Graphics g)
        {
            foreach (var segment in Shapes)
            {
                segment.Draw(gridSize, g);
            }
        }

        internal void Save(string fileName)
        {
            try
            {
                var s = JsonSerializer.CreateDefault();
                s.TypeNameHandling = TypeNameHandling.All;
                using (StreamWriter sw = new StreamWriter(fileName))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    s.Serialize(jw, this);
                }
                _hasChanges = false;
            }
            catch
            {
                MessageBox.Show("Could not save to file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void TranslateDrawing(int x, int y)
        {
            TranslateSegments(Shapes, x, y);
        }

        internal Size GetContainingRectangle(int gridSize, out Entry startPoint)
        {
            // find the max X,Y
            int maxWidth = 0;
            int maxHeight = 0;

            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;

            foreach (var segment in Shapes)
            {
                Container bounds = segment.GetBounds();

                // see if the current bounds are larger that what we saw so far.
                if (bounds.BottomRight.X > maxWidth) maxWidth = bounds.BottomRight.X;
                if (bounds.BottomRight.Y > maxHeight) maxHeight = bounds.BottomRight.Y;

                if (bounds.TopLeft.X < minX) minX = bounds.TopLeft.X;
                if (bounds.TopLeft.Y < minY) minY = bounds.TopLeft.Y;
            }

            // add 30% more to make the drawing look nicer.
            minX = (int)(minX * 1.3);
            minY = (int)(minY * 1.3);

            startPoint = new Entry(Math.Min(0, minX), Math.Min(0, minY));

            // we need to account for elements that you cannot see as they are below (0,0)
            if (minY < 0 || minX < 0)
            {
                maxWidth += Math.Abs(minX);
                maxHeight += Math.Abs(minY);
            }

            // for good measure, add 10% more grid sizes.
            maxWidth = (int)(maxWidth * 1.1);
            maxHeight = (int)(maxHeight * 1.1);

            System.Diagnostics.Debug.WriteLine("Container size: w:{0},h:{1}", maxWidth, maxHeight);

            return new Size(maxWidth * gridSize, maxHeight * gridSize);
        }

        internal void TranslateSegments(List<Shape> _shapesToMove, int x, int y)
        {
            foreach (var shape in _shapesToMove)
            {
                shape.Translate(x, y);
            }
            if (_shapesToMove.Count > 0)
            {
                _hasChanges = true;
            }
        }
    }
}
