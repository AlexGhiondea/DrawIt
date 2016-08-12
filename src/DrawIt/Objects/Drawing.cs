﻿using Newtonsoft.Json;
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

        internal void AddSegment(Line newLine)
        {
            Segments.Add(newLine);
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

        internal void DrawWithTranslation(int gridSize, Graphics g, Entry translateBy)
        {
            Drawing translatedDrawing = this.Clone();

            translatedDrawing.TranslateDrawing(-translateBy.X, -translateBy.Y);

            translatedDrawing.Draw(gridSize, g);
        }

        internal Drawing Clone()
        {
            Drawing newDrawing = new Drawing(this.ConversionRatio, this.Unit);
            foreach (var item in Segments)
            {
                if (item is Line)
                {
                    Line l = item as Line;
                    newDrawing.AddSegment(new Line(l.Start.Clone(), l.End.Clone(), l.Color, l.Width));
                }
                else if (item is Measurement)
                {
                    Measurement m = item as Measurement;
                    newDrawing.AddMeasurement(new Measurement(m.Start.Clone(), m.End.Clone(), m.Color, m.Location, m.ConversionRate, m.Unit));
                }
            }

            return newDrawing;
        }

        private void DrawUserSegments(int gridSize, Graphics g)
        {
            foreach (var segment in Segments)
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
            TranslateSegments(Segments, x, y);
        }

        internal Size GetContainingRectangle(int gridSize, out Entry startPoint)
        {
            // find the max X,Y
            int maxWidth = 0;
            int maxHeight = 0;

            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;

            foreach (var segment in Segments)
            {
                if (segment.Start.X > maxWidth) maxWidth = segment.Start.X;
                if (segment.End.X > maxWidth) maxWidth = segment.End.X;

                if (segment.Start.Y > maxHeight) maxHeight = segment.Start.Y;
                if (segment.End.Y > maxHeight) maxHeight = segment.End.Y;

                // see if we have entries below (0,0)

                if (segment.Start.X < minX) minX = segment.Start.X;
                if (segment.End.X < minX) minX = segment.End.X;

                if (segment.Start.Y < minY) minY = segment.Start.Y;
                if (segment.End.Y < minY) minY = segment.End.Y;
            }

            // add 30% more to make the drawing look nicer.
            minX = (int)(minX * 1.3);
            minY = (int)(minY * 1.3);

            startPoint = new Entry(Math.Min(0, minX), Math.Min(0, minY));

            // we need to account for elements that you cannot see as they are below (0,0)
            if (minY < 0 || minX < 0)
            {
                maxWidth += minX;
                maxHeight += minY;
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
