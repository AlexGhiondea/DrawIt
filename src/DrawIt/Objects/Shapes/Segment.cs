using System;
using System.Drawing;

namespace DrawIt
{
    public abstract class Segment : Shape
    {
        public Entry Start;
        public Entry End;
        public float Width;

        protected double Length
        {
            get
            {
                //compute the length of the line
                int deltaX = (End.X - Start.X);
                int deltaY = (End.Y - Start.Y);
                return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            }
        }

        public Segment(Entry start, Entry end, Color color, float width)
            : base(color)
        {
            this.Start = start;
            this.End = end;
            this.Width = width;
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            return DrawFacts.PointOnLineSegment(Start.ToPoint(gridSize), End.ToPoint(gridSize), p, Width);
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
            int topMostPoint = Math.Min(Start.Y, End.Y);

            int rightMostPoint = Math.Max(Start.X, End.X);
            int bottomMostPoint = Math.Max(Start.Y, End.Y);

            Container bounds = new Container();
            bounds.TopLeft = new Entry(leftMostPoint, topMostPoint);
            bounds.BottomRight = new Entry(rightMostPoint, bottomMostPoint);
            return bounds;
        }
    }
}
