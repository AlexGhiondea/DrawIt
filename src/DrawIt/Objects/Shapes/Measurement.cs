using System;
using System.Drawing;

namespace DrawIt
{
    public class Measurement : Line
    {
        public double ConversionRate;
        public string Unit;
        private const float MeasurementWidth = 1f;

        public Measurement(Entry start, Entry end, Color color, MeasurementLocation location, double rate, string unit)
            : base(start, end, color, MeasurementWidth)
        {
            this.ConversionRate = rate;
            this.Unit = unit;
            this.Location = location;
        }

        private static Font measureFont = new Font("Calibri", 10, FontStyle.Bold);
        private static Pen measurePen = new Pen(new SolidBrush(Color.Green), MeasurementWidth);
        public MeasurementLocation Location;

        public override void Draw(int gridSize, Graphics g)
        {
            // we only support measurements on horizontal and vertical lines
            bool horizontal = Start.Y == End.Y;
            int distanceInGridSize = horizontal ? Math.Abs(Start.X - End.X) : Math.Abs(Start.Y - End.Y);

            // Always draw the line where it was selected
            measurePen.Color = Color;
            g.DrawLine(measurePen, Start.ToPoint(gridSize), End.ToPoint(gridSize));

            // find the middle
            float middle;
            PointF StartText;

            string text = string.Format("{0} {1}", distanceInGridSize * ConversionRate, Unit);
            SizeF textSize = g.MeasureString(text, measureFont);

            Point StartPoint = Start.ToPoint(gridSize);
            Point EndPoint = End.ToPoint(gridSize);

            if (horizontal)
            {
                //draw End lines
                g.DrawLine(measurePen, StartPoint.AddToY(-(gridSize / 2)), StartPoint.AddToY((gridSize / 2)));
                g.DrawLine(measurePen, EndPoint.AddToY(-(gridSize / 2)), EndPoint.AddToY((gridSize / 2)));

                middle = Math.Min(Start.X, End.X) * gridSize + (Math.Abs(Start.X - End.X) * gridSize / 2);
                middle -= textSize.Width / 2;

                StartText = new PointF(middle, ((float)Start.Y * gridSize));

                if ((Location & MeasurementLocation.Above) == MeasurementLocation.Above)
                {
                    StartText.Y -= textSize.Height;
                }
                else
                {
                    StartText.Y += 2 * measurePen.Width;
                }
            }
            else
            {
                //draw End lines
                g.DrawLine(measurePen, StartPoint.AddToX(-(gridSize / 2)), StartPoint.AddToX(+(gridSize / 2)));
                g.DrawLine(measurePen, EndPoint.AddToX(-(gridSize / 2)), EndPoint.AddToX(+(gridSize / 2)));

                middle = Math.Min(Start.Y, End.Y) * gridSize + (Math.Abs(Start.Y - End.Y) * gridSize / 2);
                middle -= textSize.Height / 2;

                StartText = new PointF(((float)Start.X * gridSize + 2 * measurePen.Width), middle);
                if ((Location & MeasurementLocation.Left) == MeasurementLocation.Left)
                {
                    StartText.X -= textSize.Width + 2 * measurePen.Width;
                }
                else
                {
                    StartText.X += measurePen.Width;
                }
            }

            g.DrawString(text, measureFont, new SolidBrush(Color), StartText);
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
