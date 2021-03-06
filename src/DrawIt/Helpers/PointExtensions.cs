﻿using System.Drawing;

namespace DrawIt
{
    public static class PointExtensions
    {
        public static Entry ToEntry(this Point p, int gridSize)
        {
            return new Entry(p.X / gridSize, p.Y / gridSize);
        }
        public static Point AddToX(this Point p, int value)
        {
            return new Point(p.X + value, p.Y);
        }
        public static Point AddToY(this Point p, int value)
        {
            return new Point(p.X, p.Y + value);
        }
    }
}
