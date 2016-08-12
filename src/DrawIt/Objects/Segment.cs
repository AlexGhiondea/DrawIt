using System.Drawing;

namespace DrawIt
{
    //public abstract class Shape
    //{
    //    public Color Color;
    //    public Shape(Color color)
    //    {
    //        this.Color = color;
    //    }
    //    public abstract void Draw(int gridSize, Graphics g);
    //}

    //public abstract class RectangleMy : Shape
    //{
    //    public Entry Start;
    //    public Entry End;
    //    public int Width;

    //    public RectangleMy(Entry start, Entry end, int width, Color color) :
    //        base(color)
    //    {
    //        this.Start = start;
    //        this.End = end;
    //        this.Width = width;
    //    }

    //    public override void Draw(int gridSize, Graphics g)
    //    {
    //        Pen pen = new Pen(new SolidBrush(Color), Width);

    //        Rectangle rect = new Rectangle(Start.ToPoint(gridSize), Start.ToSizeFromPoint(End, gridSize));

    //        g.DrawRectangle(pen, rect);
    //    }
    //}

    public abstract class Segment 
    {
        public Entry Start;
        public Entry End;
        public Color Color;

        public Segment(Entry start, Entry end, Color color)
        {
            this.Start = start;
            this.End = end;
            this.Color = color;
        }

        public abstract void Draw(int gridSize, Graphics g);
    }
}
