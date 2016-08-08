using System.Drawing;

namespace DrawIt
{
    public class Entry
    {
        public int X;
        public int Y;
        public Entry(int x, int y)
        {
            X = x; Y = y;
        }

        public Point ToPoint(int gridSize)
        {
            return new Point(X * gridSize, Y * gridSize);
        }

        public void Adjust(int x, int y)
        {
            X += x;
            Y += y;
        }

        public override string ToString()
        {
            return "X:" + X + ", Y:" + Y;
        }

        public override bool Equals(object obj)
        {
            Entry other = obj as Entry;
            if (other == null) return false;

            return this.X == other.X && this.Y == other.Y;
        }
    }
}
