using System.Drawing;

namespace DrawIt
{
    public class Line : Segment
    {
        public Line(Entry start, Entry end, Color color, float width)
            :base(start, end, color, width)
        {
        }

        public override void Draw(int gridSize, Graphics g)
        {
            Pen pen = new Pen(new SolidBrush(Color), Width);
            g.DrawLine(pen, Start.ToPoint(gridSize), End.ToPoint(gridSize));
        }

        public override Shape DeepClone()
        {
            return new Line(Start.Clone(), End.Clone(), Color, Width);
        }
    }
}
