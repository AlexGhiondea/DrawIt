using System.Drawing;

namespace DrawIt
{
    public static class SizeExtensions
    {
        public static Size ToSizeFromPoint(this Entry Start, Entry End, int gridSize)
        {
            return new Size((Start.X - End.X) * gridSize, (Start.Y - End.Y) * gridSize);
        }
    }
}
