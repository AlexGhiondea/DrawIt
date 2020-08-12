using System;
using System.Drawing;

namespace DrawIt
{
    public class Measurement : Line
    {
        public double ConversionRate;
        public string Unit;
        private const float MeasurementWidth = 1f;
        private static Font measureFont = new Font("Calibri", 10, FontStyle.Bold);

        public Measurement(Entry start, Entry end, Color color, MeasurementLocation location, double rate, string unit)
            : base(start, end, color, MeasurementWidth)
        {
            this.ConversionRate = rate;
            this.Unit = unit;
            this.Location = location;
        }


        public MeasurementLocation Location;

        public override void Draw(int gridSize, Graphics g)
        {
            using (Pen pen = new Pen(new SolidBrush(Color), MeasurementWidth))
            {
                g.DrawLine(pen, Start.ToPoint(gridSize), End.ToPoint(gridSize));

                // figure out the angle
                double degrees = DrawFacts.ComputeSlopeInDegrees(Start, End);

                // we need to translate the ends at each of the points (start and end)
                DrawEndLine(gridSize, g, Start, degrees, pen);
                DrawEndLine(gridSize, g, End, degrees, pen);

                DrawRotatedMeasureText(gridSize, g, degrees);
            }
        }

        private void DrawRotatedMeasureText(int gridSize, Graphics g, double degrees)
        {
            string text = string.Format("{0} {1}", (double)(Math.Round(Length * ConversionRate, 1)), Unit);
            SizeF textSize = g.MeasureString(text, measureFont);

            // Calculate the middle part of the segment
            Point start, end;
            start = Start.ToPoint(gridSize);
            end = End.ToPoint(gridSize);
            PointF middlePoint = new PointF((start.X + end.X) / 2f, (start.Y + end.Y) / 2f);

            // we should find the middle of the segment and rotate based on that
            // at that point, we can easily translate it to not overlap the line
            using (System.Drawing.Drawing2D.Matrix transformMatrix = new System.Drawing.Drawing2D.Matrix())
            {
                // we rotate the text to match the slope of the line.
                transformMatrix.RotateAt((float)degrees, middlePoint);

                // translate by half the width
                transformMatrix.Translate(-textSize.Width / 2, 0);

                // if we want to put the text above the line, we need to translate the height as well.
                if ((Location & MeasurementLocation.Above) == MeasurementLocation.Above)
                {
                    transformMatrix.Translate(0, -textSize.Height);
                }

                g.Transform = transformMatrix;

                g.DrawString(text, measureFont, new SolidBrush(Color), middlePoint);

                g.ResetTransform();
            }
        }

        private void DrawEndLine(int gridSize, Graphics g, Entry location, double degrees, Pen pen)
        {
            using (System.Drawing.Drawing2D.Matrix transformMatrix = new System.Drawing.Drawing2D.Matrix())
            {
                transformMatrix.RotateAt((float)degrees, location.ToPoint(gridSize));

                g.Transform = transformMatrix;
                g.DrawLine(pen, location.ToPoint(gridSize).AddToY(-(gridSize / 2)), location.ToPoint(gridSize).AddToY((gridSize / 2)));

                g.ResetTransform();
            }
        }

        public override Shape DeepClone()
        {
            return new Measurement(Start.Clone(), End.Clone(), Color, Location, ConversionRate, Unit);
        }

        public override Container GetBounds()
        {
            Container c = base.GetBounds();
            c.TopLeft.Adjust(0, -1);
            c.BottomRight.Adjust(0, 2);
            return c;
        }
    }
}
