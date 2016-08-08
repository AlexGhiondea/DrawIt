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
        public List<Measurement> Measurements;
        public List<List<Line>> Segments;

        private List<Line> _currentLine;

        public string Unit;
        public double ConversionRatio;

        public bool HasChanges() { return _hasChanges; }

        public Drawing(double ratio, string unit)
        {
            ConversionRatio = ratio;
            Unit = unit;

            Measurements = new List<Measurement>();
            Segments = new List<List<Line>>();
        }

        internal void NewSequence()
        {
            if (_currentLine == null)
            {
                _currentLine = new List<Line>();
                Segments.Add(_currentLine);
            }
            else if (_currentLine.Count != 0)
            {
                _currentLine = new List<Line>();
                Segments.Add(_currentLine);

            }
        }

        internal void AddToCurrentSegment(Line newLine)
        {
            _currentLine.Add(newLine);
            _hasChanges = true;
        }

        internal void RemoveSegmentAtPoint(Point point, int gridSize)
        {
            for (int j = 0; j < Segments.Count; j++)
            {
                var segment = Segments[j];

                for (int i = 0; i < segment.Count; i++)
                {
                    if (PointOnLineSegment(segment[i].Start.ToPoint(gridSize), segment[i].End.ToPoint(gridSize), point, 4 /*This should come from the actual segment*/))
                    {
                        segment.RemoveAt(i);
                        _hasChanges = true;
                    }
                }
            }

            // check the measurements as well
            for (int i = 0; i < Measurements.Count; i++)
            {
                if (PointOnLineSegment(Measurements[i].Start.ToPoint(gridSize), Measurements[i].End.ToPoint(gridSize), point, 2 /* this should come from the segment itslef s_measurePen.Width*/))
                {
                    Measurements.RemoveAt(i);
                    _hasChanges = true;
                    i--; // still need to process the rest of the list
                }
            }


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

            Measurements.Add(measurement);
            _hasChanges = true;
        }

        internal void Draw(int gridSize, Graphics g)
        {
            DrawUserSegments(gridSize, g);
            DrawMeasurements(gridSize, g);
        }

        private void DrawUserSegments(int gridSize, Graphics g)
        {
            foreach (var segmentList in Segments)
            {
                foreach (var segment in segmentList)
                {
                    segment.Draw(gridSize, g);
                }
            }
        }

        private void DrawMeasurements(int gridSize, Graphics g)
        {
            foreach (var measurement in Measurements)
            {
                measurement.Draw(g, gridSize, ConversionRatio, Unit);
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
            // TODO: Make sure we don't run below 0 at any point!

            foreach (var segment in Segments)
            {
                foreach (var lineEntry in segment)
                {
                    lineEntry.Start.Adjust(x, y);
                    lineEntry.End.Adjust(x, y);
                }
            }

            foreach (var measure in Measurements)
            {
                measure.Start.Adjust(x, y);

                measure.End.Adjust(x, y);
            }
        }

        internal Size GetContainingRectangle(int gridSize)
        {
            // find the max X,Y
            int maxWidth = 0;
            int maxHeight = 0;

            foreach (var list in Segments)
            {
                foreach (var segment in list)
                {
                    if (segment.Start.X > maxWidth) maxWidth = segment.Start.X;
                    if (segment.End.X > maxWidth) maxWidth = segment.End.X;

                    if (segment.Start.Y > maxHeight) maxHeight = segment.Start.Y;
                    if (segment.End.Y > maxHeight) maxHeight = segment.End.Y;
                }
            }

            foreach (var segment in Measurements)
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
    }
}
