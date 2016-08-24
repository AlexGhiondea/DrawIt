using System.Drawing;

namespace DrawIt
{
    public class Circle : Shape
    {
        public Entry Center;
        public int Radius;
        public float Width;

        public Circle(Entry center, int radius, float width, Color color)
            : base(color)
        {
            this.Center = center;
            this.Radius = radius;
            this.Width = width;
        }

        public override void Draw(int gridSize, Graphics g)
        {
            Pen p = new Pen(new SolidBrush(Color), Width);
            var point = new Entry(Center.X - Radius, Center.Y - Radius).ToPoint(gridSize);

            g.DrawArc(p, new System.Drawing.Rectangle(point, new Size(2 * Radius * gridSize, 2 * Radius * gridSize)), 0, 360);
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            var center = Center.ToPoint(gridSize);

            int radiusAfterTranslation = (int)System.Math.Sqrt(((p.X - center.X) * (p.X - center.X)) + ((p.Y - center.Y) * (p.Y - center.Y)));

            int compareTo = (Radius * gridSize);

            return radiusAfterTranslation >= compareTo - Width && radiusAfterTranslation <= compareTo + Width;
        }

        public override void Translate(int x, int y)
        {
            Center.Adjust(x, y);
        }

        public override Container GetBounds()
        {
            return new Container()
            {
                TopLeft = new Entry(Center.X - Radius, Center.Y - Radius),
                BottomRight = new Entry(Center.X + Radius, Center.Y + Radius),
            };
        }

        public override Shape DeepClone()
        {
            return new Circle(Center.Clone(), Radius, Width, Color);
        }
    }
}
