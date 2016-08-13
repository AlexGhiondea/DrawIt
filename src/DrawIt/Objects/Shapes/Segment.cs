using System;
using System.Drawing;

namespace DrawIt
{
    public abstract class Segment : Shape
    {
        public Entry Start;
        public Entry End;
        public float Width;

        public Segment(Entry start, Entry end, Color color, float width)
            : base(color)
        {
            this.Start = start;
            this.End = end;
            this.Width = width;
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            return PointOnLineSegment(Start.ToPoint(gridSize), End.ToPoint(gridSize), p, Width);
        }

        private static bool PointOnLineSegment(Point left, Point right, Point pt, double epsilon)
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

        /// <summary>
        /// Translate the shape by (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void Translate(int x, int y)
        {
            Start.Adjust(x, y);
            End.Adjust(x, y);
        }

        public override Container GetBounds()
        {
            int leftMostPoint = Math.Min(Start.X, End.X);
            int topMostPoint = Math.Max(Start.Y, End.Y);

            int rightMostPoint = Math.Max(Start.X, End.X);
            int bottomMostPoint = Math.Min(Start.Y, End.Y);

            Container bounds = new Container();
            bounds.TopLeft = new Entry(leftMostPoint, topMostPoint);
            bounds.BottomRight = new Entry(rightMostPoint, bottomMostPoint);
            return bounds;
        }
    }
}
