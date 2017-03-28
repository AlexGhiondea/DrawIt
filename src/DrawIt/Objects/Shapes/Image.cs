using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace DrawIt
{
    public class Image : Shape
    {
        public Entry Start;
        public Entry End;
        public string EncodedImage;

        private Entry BottomRight; // this is computed.

        public Image(Entry topLeft, Entry bottomRight, string encodedImage)
            : base(Color.Black) // hardcode the value as we don't actually use it.
        {

            this.Start = topLeft;
            this.End = bottomRight;
            this.EncodedImage = encodedImage;
        }

        public override void Draw(int gridSize, Graphics g)
        {
            //create the image from the encodedString.
            byte[] imageBytes = Convert.FromBase64String(EncodedImage);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            using (Bitmap bmp = new Bitmap(ms))
            {
                int newWidth, newHeight;

                ComputeScaleFactor(gridSize, bmp.Width, bmp.Height, out newWidth, out newHeight);

                var topLeftPoint = Start.ToPoint(gridSize);

                // draw the scaled image
                g.DrawImage(bmp, topLeftPoint.X, topLeftPoint.Y, newWidth, newHeight);
            }
        }

        private void ComputeScaleFactor(int gridSize, int sourceWidth, int sourceHeight, out int newWidth, out int newHeight)
        {
            // decide which one we keep fixed.
            // we only support measurements on horizontal and vertical lines
            bool horizontal = Start.Y == End.Y;
            int distanceInGridSize = horizontal ? Math.Abs(Start.X - End.X) : Math.Abs(Start.Y - End.Y);

            double allowedSize = distanceInGridSize * gridSize;

            double scaleFactor = 1;
            if (horizontal) //we drew the line as a horizontal line, the width is the fixed size
            {
                scaleFactor = sourceWidth / allowedSize;
            }
            else
            {
                scaleFactor = sourceHeight / allowedSize;
            }

            newWidth = (int)(sourceWidth / scaleFactor);
            newHeight = (int)(sourceHeight / scaleFactor);

            BottomRight = new Entry(Start.X + newWidth / gridSize, End.Y + newHeight / gridSize);
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            return DrawFacts.PointInsideRectangle(Start.ToPoint(gridSize), BottomRight.ToPoint(gridSize), p);
        }

        public override void Translate(int x, int y)
        {
            Start.Adjust(x, y);
            End.Adjust(x, y);
        }

        public override Container GetBounds()
        {
            return new Container()
            {
                TopLeft = Start.Clone(),
                BottomRight = BottomRight.Clone()
            };
        }

        public override Shape DeepClone()
        {
            return new Image(Start.Clone(), End.Clone(), EncodedImage);
        }
    }
}
