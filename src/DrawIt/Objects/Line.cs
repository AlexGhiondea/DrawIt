using System.Drawing;

namespace DrawIt
{
    public class Line : Segment
    {
        public float Width;

        public void Draw(int gridSize, Graphics g)
        {
            Pen pen = new Pen(new SolidBrush(Color), Width);
            g.DrawLine(pen, Start.ToPoint(gridSize), End.ToPoint(gridSize));
        }
    }
}
