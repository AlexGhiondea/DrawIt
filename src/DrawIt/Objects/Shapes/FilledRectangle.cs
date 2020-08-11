using System;
using System.Drawing;

namespace DrawIt
{
    public class FilledRectangle : Rectangle
    {
        public FilledRectangle(Entry topLeft, Entry bottomRight, float width, Color color)
            : base(topLeft, bottomRight, width, color)
        {

        }

        public override void Draw(int gridSize, Graphics g)
        {
            //Pen p = new Pen(new SolidBrush(Color), Width);

            g.FillRectangle(new SolidBrush(Color), new System.Drawing.Rectangle(TopLeft.ToPoint(gridSize),
                new Size(Math.Abs((TopLeft.X - BottomRight.X) * gridSize), Math.Abs((TopLeft.Y - BottomRight.Y) * gridSize))));
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            //check all 4 lines.
            return DrawFacts.PointOnRectangle(TopLeft.ToPoint(gridSize), BottomRight.ToPoint(gridSize), p, Width) ||
                DrawFacts.PointInsideRectangle(TopLeft.ToPoint(gridSize), BottomRight.ToPoint(gridSize), p);
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
            return new FilledRectangle(TopLeft.Clone(), BottomRight.Clone(), Width, Color);
        }
    }
}
