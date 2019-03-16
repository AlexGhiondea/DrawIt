using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawIt.Importer
{
    public class ImportForBeads
    {
        public Drawing CreateFromImage(string imagePath, int width, int height)
        {
            Drawing dr = new Drawing(1, "px");
            using (Bitmap bmp = new Bitmap(imagePath))
            {
                var sizeInPixelWidth = bmp.Width / width;
                var sizeInPixelHeight = bmp.Height / height;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        long a = 0, r = 0, g = 0, b = 0;
                        // compute the average color for this block
                        for (int ini = i * sizeInPixelWidth; ini < (i + 1) * sizeInPixelWidth; ini++)
                        {
                            for (int inj = j * sizeInPixelHeight; inj < (j + 1) * sizeInPixelHeight; inj++)
                            {
                                var pixel = bmp.GetPixel(ini, inj);
                                a += pixel.A;
                                r += pixel.R;
                                g += pixel.G;
                                b += pixel.B;
                            }
                        }
                        Color c = Color.FromArgb((int)(a / (sizeInPixelHeight * sizeInPixelWidth)),
                            (int)(r / (sizeInPixelHeight * sizeInPixelWidth)),
                            (int)(g / (sizeInPixelHeight * sizeInPixelWidth)),
                            (int)(b / (sizeInPixelHeight * sizeInPixelWidth)));

                        Color normalizedColor = ColorNormalizer.Normalize(c);

                        dr.AddShape(new FilledRectangle(new Entry(i, j), new Entry(i + 1, j + 1), 1, normalizedColor));
                    }
                }

                //dr.Save(imagePath + ".dit");

            }

            return dr;
        }
    }
}
