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
}
