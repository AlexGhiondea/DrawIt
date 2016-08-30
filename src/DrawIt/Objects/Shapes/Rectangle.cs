using System;
using System.Drawing;

namespace DrawIt
{
    public class Rectangle : Shape
    {
        public Entry TopLeft;
        public Entry BottomRight;
        public float Width;

        public Rectangle(Entry topLeft, Entry bottomRight, float width, Color color)
            : base(color)
        {
            this.TopLeft = new Entry(Math.Min(topLeft.X, bottomRight.X), Math.Min(topLeft.Y, bottomRight.Y));
            this.BottomRight = new Entry(Math.Max(topLeft.X, bottomRight.X), Math.Max(topLeft.Y, bottomRight.Y));
            this.Width = width;
        }

        public override void Draw(int gridSize, Graphics g)
        {
            Pen p = new Pen(new SolidBrush(Color), Width);

            g.DrawRectangle(p, new System.Drawing.Rectangle(TopLeft.ToPoint(gridSize),
                new Size(Math.Abs((TopLeft.X - BottomRight.X) * gridSize), Math.Abs((TopLeft.Y - BottomRight.Y) * gridSize))));
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            //check all 4 lines.
            return DrawFacts.PointOnRectangle(TopLeft.ToPoint(gridSize), BottomRight.ToPoint(gridSize), p, Width);
        }

        public override void Translate(int x, int y)
        {
            TopLeft.Adjust(x, y);
            BottomRight.Adjust(x, y);
        }

        public override Container GetBounds()
        {
            return new Container()
            {
                TopLeft = TopLeft.Clone(),
                BottomRight = BottomRight.Clone()
            };
        }

        public override Shape DeepClone()
        {
            return new Rectangle(TopLeft.Clone(), BottomRight.Clone(), Width, Color);
        }
    }
}
