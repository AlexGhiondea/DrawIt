using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawIt
{
    public static class PointExtensions
    {
        public static Entry ToEntry(this Point p, int gridSize)
        {
            return new Entry(p.X / gridSize, p.Y / gridSize);
        }
        public static Point AddToX(this Point p, int value)
        {
            return new Point(p.X + value, p.Y);
        }
        public static Point AddToY(this Point p, int value)
        {
            return new Point(p.X, p.Y + value);
        }
    }

    public static class SizeExtensions
    {
        public static Size ToSizeFromPoint(this Entry Start, Entry End, int gridSize)
        {
            return new Size((Start.X - End.X) * gridSize, (Start.Y - End.Y) * gridSize);
        }
    }

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
    }
}
