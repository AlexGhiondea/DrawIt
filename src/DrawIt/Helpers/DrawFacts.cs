﻿using System;
using System.Drawing;

namespace DrawIt
{
    public static class DrawFacts
    {
        public static bool PointOnLineSegment(Point left, Point right, Point pt, double epsilon)
        {
            if (pt.X - Math.Max(left.X, right.X) > epsilon ||
                Math.Min(left.X, right.X) - pt.X > epsilon ||
                pt.Y - Math.Max(left.Y, right.Y) > epsilon ||
                Math.Min(left.Y, right.Y) - pt.Y > epsilon)
                return false;

            if (Math.Abs(right.X - left.X) < epsilon)
                return Math.Abs(left.X - pt.X) < epsilon || Math.Abs(right.X - pt.X) < epsilon;
            if (Math.Abs(right.Y - left.Y) < epsilon)
                return Math.Abs(left.Y - pt.Y) < epsilon || Math.Abs(right.Y - pt.Y) < epsilon;

            double x = left.X + (pt.Y - left.Y) * (right.X - left.X) / (right.Y - left.Y);
            double y = left.Y + (pt.X - left.X) * (right.Y - left.Y) / (right.X - left.X);

            return Math.Abs(pt.X - x) < epsilon || Math.Abs(pt.Y - y) < epsilon;
        }

        public static bool PointOnRectangle(Point topLeft, Point bottomRight, Point pt, double epsilon)
        {
            // check for all 4 lines of the rectangle.
            Point topRight = new Point(bottomRight.X, topLeft.Y);
            Point bottomLeft = new Point(topLeft.X, bottomRight.Y);

            bool upper = PointOnLineSegment(topLeft, topRight, pt, epsilon);
            bool lower = PointOnLineSegment(bottomLeft, bottomRight, pt, epsilon);
            bool left = PointOnLineSegment(topLeft, bottomLeft, pt, epsilon);
            bool right = PointOnLineSegment(topRight, bottomRight, pt, epsilon);

            return upper || lower || left || right;
        }

        public static bool PointInsideRectangle(Point topLeft, Point bottomRight, Point pt)
        {
            return (pt.X >= topLeft.X && pt.X <= bottomRight.X) &&
                   (pt.Y >= topLeft.Y && pt.Y <= bottomRight.Y);
        }

        public static double ComputeSlopeInDegrees(Entry segmentStart, Entry segmentEnd)
        {
            // we are using a translation to point using a constant as the gridSize is irrelevant to the slope of the segment
            Point start = segmentStart.ToPoint(1);
            Point end = segmentEnd.ToPoint(1);

            // We might need to swap what we consider as start/end.
            if (segmentStart.X > segmentEnd.X || segmentStart.Y < segmentEnd.Y)
            {
                start = segmentEnd.ToPoint(1);
                end = segmentStart.ToPoint(1);
            }

            // Calculate angle from offset.
            double radians = Math.Atan((double)(end.Y - start.Y) / (double)(end.X - start.X));

            double degrees = radians * ((double)180 / Math.PI);
            return degrees;
        }
    }
}
