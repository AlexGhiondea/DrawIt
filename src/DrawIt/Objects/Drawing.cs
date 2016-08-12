using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DrawIt
{
    public class Drawing
    {
        private bool _hasChanges;
        public List<Segment> Segments;

        public string Unit;
        public double ConversionRatio;

        public bool HasChanges() { return _hasChanges; }

        public Drawing(double ratio, string unit)
        {
            ConversionRatio = ratio;
            Unit = unit;

            Segments = new List<Segment>();
        }

        internal void AddToCurrentSegment(Line newLine)
        {
            Segments.Add(newLine);
            _hasChanges = true;
        }

        internal void RemoveSegmentAtPoint(Point point, int gridSize)
        {
            foreach (var toRemove in GetSegmentAtPoint(point, gridSize))
            {
                Segments.Remove(toRemove);
                _hasChanges = true;
            }
        }

        internal List<Segment> GetSegmentAtPoint(Point point, int gridSize)
        {
            List<Segment> segments = new List<Segment>();
            for (int i = 0; i < Segments.Count; i++)
            {
                if (PointOnLineSegment(Segments[i].Start.ToPoint(gridSize), Segments[i].End.ToPoint(gridSize), point, 4 /*This should come from the actual segment*/))
                {
                    segments.Add(Segments[i]);
                }
            }

            return segments.Count > 0 ? segments : null;
        }

        private static bool PointOnLineSegment(Point pt1, Point pt2, Point pt, double epsilon)
        {
            if (pt.X - Math.Max(pt1.X, pt2.X) > epsilon ||
                Math.Min(pt1.X, pt2.X) - pt.X > epsilon ||
                pt.Y - Math.Max(pt1.Y, pt2.Y) > epsilon ||
                Math.Min(pt1.Y, pt2.Y) - pt.Y > epsilon)
                return false;

            if (Math.Abs(pt2.X - pt1.X) < epsilon)
                return Math.Abs(pt1.X - pt.X) < epsilon || Math.Abs(pt2.X - pt.X) < epsilon;
            if (Math.Abs(pt2.Y - pt1.Y) < epsilon)
                return Math.Abs(pt1.Y - pt.Y) < epsilon || Math.Abs(pt2.Y - pt.Y) < epsilon;

            double x = pt1.X + (pt.Y - pt1.Y) * (pt2.X - pt1.X) / (pt2.Y - pt1.Y);
            double y = pt1.Y + (pt.X - pt1.X) * (pt2.Y - pt1.Y) / (pt2.X - pt1.X);

            return Math.Abs(pt.X - x) < epsilon || Math.Abs(pt.Y - y) < epsilon;
        }

        internal void AddMeasurement(Measurement measurement)
        {
            if (measurement.Start.Equals(measurement.End) || (measurement.Start.X != measurement.End.X && measurement.Start.Y != measurement.End.Y))
            {
                return;
            }

            Segments.Add(measurement);

            _hasChanges = true;
        }

        internal void Draw(int gridSize, Graphics g)
        {
            DrawUserSegments(gridSize, g);
        }

        private void DrawUserSegments(int gridSize, Graphics g)
        {
            foreach (var segment in Segments)
            {
                if (segment is Line)
                {
                    (segment as Line).Draw(gridSize, g);
                }
                else if (segment is Measurement)
                {
                    (segment as Measurement).Draw(g, gridSize, ConversionRatio, Unit);
                }
            }

        }

        internal void Save(string fileName)
        {
            try
            {
                var s = JsonSerializer.CreateDefault();
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
            TranslateSegments(Segments, x, y);
        }

        internal Size GetContainingRectangle(int gridSize)
        {
            // find the max X,Y
            int maxWidth = 0;
            int maxHeight = 0;

            foreach (var segment in Segments)
            {
                if (segment.Start.X > maxWidth) maxWidth = segment.Start.X;
                if (segment.End.X > maxWidth) maxWidth = segment.End.X;

                if (segment.Start.Y > maxHeight) maxHeight = segment.Start.Y;
                if (segment.End.Y > maxHeight) maxHeight = segment.End.Y;
            }

            // for good measure, add 10% more grid sizes.
            maxWidth = (int)(maxWidth * 1.1);
            maxHeight = (int)(maxHeight * 1.1);

            return new Size(maxWidth * gridSize, maxHeight * gridSize);
        }

        internal void TranslateSegments(List<Segment> _segmentsToMove, int x, int y)
        {
            foreach (var lineEntry in _segmentsToMove)
            {
                lineEntry.Start.Adjust(x, y);
                lineEntry.End.Adjust(x, y);
            }
        }
    }
}
