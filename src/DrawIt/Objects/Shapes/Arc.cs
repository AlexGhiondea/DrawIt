using System;
using System.Diagnostics;
using System.Drawing;

namespace DrawIt
{
    public class Arc : Segment
    {
        public double Radius;
        public Arc(Entry start, Entry end, double radius, Color color, float width)
            : base(start, end, color, width)
        {
            this.Radius = radius;
        }

        public override void Draw(int gridSize, Graphics g)
        {
            int deltaX = (End.X - Start.X);
            int deltaY = (End.Y - Start.Y);

            // This is the size of the rectangle that will hold the ellipse
            SizeF arcSizeRotate = new SizeF((float)Length * gridSize, (float)(Radius * 2 * gridSize));

            // Perform the rotations and translations.
            using (System.Drawing.Drawing2D.Matrix transformMatrix = new System.Drawing.Drawing2D.Matrix())
            {
                Point start = Start.ToPoint(gridSize);
                Point end = End.ToPoint(gridSize);

                // We might need to swap what we consider as start/end.
                if (Start.X > End.X || Start.Y < End.Y)
                {
                    start = End.ToPoint(gridSize);
                    end = Start.ToPoint(gridSize);
                }

                // Calculate angle from offset.
                double radians = Math.Atan((double)(end.Y - start.Y) / (double)(end.X - start.X));
                double degrees = radians * ((double)180 / Math.PI);

                // Rotate by the specified angle
                transformMatrix.RotateAt((float)degrees, start, System.Drawing.Drawing2D.MatrixOrder.Prepend);

                // Convert back into radians, but with the absolute value for degrees.
                degrees = Math.Abs(degrees) * Math.PI / 180;

                int signX = Math.Sign(deltaX);
                int signY = Math.Sign(deltaY);

                // translate by the width of the line as the box is drawn away from the line
                if (signX == 1 && signY == 1)
                {
                    transformMatrix.Translate(-(float)(Math.Cos(degrees) * (arcSizeRotate.Width)), 0, System.Drawing.Drawing2D.MatrixOrder.Append);
                    transformMatrix.Translate(0, -(float)(Math.Sin(degrees) * (arcSizeRotate.Width)), System.Drawing.Drawing2D.MatrixOrder.Append);
                }

                // always translate on the x axis
                transformMatrix.Translate(0, -(float)(Math.Cos(degrees) * (arcSizeRotate.Height / 2)), System.Drawing.Drawing2D.MatrixOrder.Append);

                // translate on the y axis eiter by adding or substracting depending on the way the line was drawn
                transformMatrix.Translate((signY == signX ? 1 : -1) * (float)(Math.Sin(degrees) * (arcSizeRotate.Height / 2)), 0, System.Drawing.Drawing2D.MatrixOrder.Append);

                //figure out the angle

                int startDegrees = 180;
                if (signX == -1 || (signX == 0 && signY == 1))
                {
                    startDegrees = 0;
                }

                g.Transform = transformMatrix;

                Pen pen = new Pen(new SolidBrush(Color), Width);
                g.DrawArc(pen, new RectangleF(start, arcSizeRotate), (float)(startDegrees), (float)(180));

                g.ResetTransform();
            }
        }

        public override Shape DeepClone()
        {
            return new Arc(Start.Clone(), End.Clone(), Radius, Color, Width);
        }

        public override bool ContainsPoint(int gridSize, Point location)
        {
            //TODO: implement the check on a rotated ellipse

            // for now, only check for the 2 points (start and end)

            if ((location.X - Start.ToPoint(gridSize).X <= Width && location.X - Start.ToPoint(gridSize).X >= 0) &&
                (location.Y - Start.ToPoint(gridSize).Y <= Width && location.Y - Start.ToPoint(gridSize).Y >= 0))
                return true;

            if ((location.X - End.ToPoint(gridSize).X <= Width && location.X - End.ToPoint(gridSize).X >= 0) &&
                (location.Y - End.ToPoint(gridSize).Y <= Width && location.Y - End.ToPoint(gridSize).Y >= 0))
                return true;

            return false;
        }

        public override Container GetBounds()
        {
            // we are going to be very conservative and assume that we need 2xRadius
            var topLeft = new Entry((int)(Math.Min(Start.X, End.X) - Radius), (int)(Math.Min(Start.Y, End.Y) - Radius));
            var bottomRight = new Entry((int)(Math.Max(Start.X, End.X) + Radius), (int)(Math.Max(Start.Y, End.Y) + Radius));

            // TODO: these need to be adjusted for the angle of the line.

            Container c = new Container();
            c.TopLeft = topLeft;
            c.BottomRight = bottomRight;

            return c;
        }
    }
}
