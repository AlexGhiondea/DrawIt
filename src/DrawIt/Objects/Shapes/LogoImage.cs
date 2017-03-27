using System;
using System.Drawing;
using System.IO;

namespace DrawIt
{
    public class LogoImage : Image
    {
        public int LogoHeight;

        public LogoImage(Entry topLeft, Entry bottomRight, string encodedImage, int logoHeight)
            : base(topLeft, bottomRight, encodedImage)
        {
            LogoHeight = logoHeight;
        }

        public override void Draw(int gridSize, Graphics g)
        {
            //create the image from the encodedString.
            byte[] imageBytes = Convert.FromBase64String(EncodedImage);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            using (Bitmap bmp = new Bitmap(ms))
            {
                //scale the actual height and width
                int allowedHeight = LogoHeight * gridSize;

                double scaleFactor = (double)bmp.Height / (double)allowedHeight;

                int newHeight = (int)(bmp.Height / scaleFactor);
                int newWidth = (int)(bmp.Width / scaleFactor);

                int x = TopLeft.X * gridSize;
                int y = TopLeft.Y * gridSize;

                // draw the image scaled
                g.DrawImage(bmp, x, y, newWidth, newHeight);
            }
        }

        public override Shape DeepClone()
        {
            return new LogoImage(TopLeft.Clone(), BottomRight.Clone(), EncodedImage, LogoHeight);
        }
    }
}
