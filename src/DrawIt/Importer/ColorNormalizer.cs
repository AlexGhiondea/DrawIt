using System;
using System.Drawing;

namespace DrawIt.Importer
{
    internal class ColorNormalizer
    {
        private static Color[] s_colors = new[]
        {
            Color.Red, Color.Green, Color.Yellow, Color.Black, Color.White, Color.Pink,
            Color.Orange, Color.Brown, Color.Blue
        };

        internal static Color Normalize(Color c)
        {
            int[] score = new int[s_colors.Length];
            Color minColor = c;
            int min = int.MaxValue;

            for (int i = 0; i < s_colors.Length; i++)
            {
                var a = Math.Abs(c.A - s_colors[i].A);
                var r = Math.Abs(c.R - s_colors[i].R);
                var g = Math.Abs(c.G - s_colors[i].G);
                var b = Math.Abs(c.B - s_colors[i].B);

                int currScore = a + r + g + b;
                if (currScore < min)
                {
                    min = currScore;
                    minColor = s_colors[i];
                }

            }

            return minColor;
        }
    }
}
