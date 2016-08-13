using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawIt.Objects.Shapes
{
    public class Text : Shape
    {
        public Entry Start;
        public Entry End;
        public string String;
        public string Font;
        public float Size;

        public Text(Entry start, Entry end, string text, float fontSize, string fontFamily, Color color)
            : base(color)
        {
            this.Start = start;
            this.String = text;
            this.Size = fontSize;
            this.Font = fontFamily;
            this.End = end;
        }

        public override void Draw(int gridSize, System.Drawing.Graphics g)
        {
            Font textFont = new Font(Font, Size);
            g.DrawString(String, textFont, new SolidBrush(Color), Start.ToPoint(gridSize));
        }

        public override bool ContainsPoint(int gridSize, System.Drawing.Point p)
        {
            var startPoint = Start.ToPoint(gridSize);
            var endPoint = End.ToPoint(gridSize);

            if (p.X >= startPoint.X && p.X <= endPoint.X &&
                p.Y >= startPoint.Y && p.Y <= endPoint.Y)
                return true;

            return false;
        }

        public override void Translate(int x, int y)
        {
            Start.Adjust(x, y);
        }

        public override Container GetBounds()
        {
            int leftMostPoint = Math.Min(Start.X, End.X);
            int topMostPoint = Math.Max(Start.Y, End.Y);

            int rightMostPoint = Math.Max(Start.X, End.X);
            int bottomMostPoint = Math.Min(Start.Y, End.Y);

            Container bounds = new Container();
            bounds.TopLeft = new Entry(leftMostPoint, topMostPoint);
            bounds.BottomRight = new Entry(rightMostPoint, bottomMostPoint);
            return bounds;
        }
    }
}
