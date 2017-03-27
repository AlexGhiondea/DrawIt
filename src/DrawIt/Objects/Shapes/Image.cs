using System;
using System.Drawing;
using System.IO;

namespace DrawIt
{
    public class Image : Shape
    {
        public Entry TopLeft;
        public Entry BottomRight;
        public string EncodedImage;

        public Image(Entry topLeft, Entry bottomRight, string encodedImage)
            : base(Color.Black) // hardcode the value as we don't actually use it.
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
            this.EncodedImage = encodedImage;
        }

        public override void Draw(int gridSize, Graphics g)
        {
            //create the image from the encodedString.
            byte[] imageBytes = Convert.FromBase64String(EncodedImage);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            using (Bitmap bmp = new Bitmap(ms))
            {
                g.DrawImage(bmp,TopLeft.ToPoint(gridSize));
            }
        }

        public override bool ContainsPoint(int gridSize, Point p)
        {
            //check all 4 lines.
            return DrawFacts.PointInsideRectangle(TopLeft.ToPoint(gridSize), BottomRight.ToPoint(gridSize), p);
        }

        public override void Translate(int x, int y)
        {
            TopLeft.Adjust(x, y);
            BottomRight.Adjust(x, y);
        }

        public override Container GetBounds()
        {
            return new Container()
            {
                TopLeft = TopLeft.Clone(),
                BottomRight = BottomRight.Clone()
            };
        }

        public override Shape DeepClone()
        {
            return new Image(TopLeft.Clone(), BottomRight.Clone(), EncodedImage);
        }
    }
}
