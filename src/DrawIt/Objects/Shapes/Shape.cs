using System;
using System.Drawing;

namespace DrawIt
{
    public abstract class Shape
    {
        public Color Color;
        public Shape(Color color)
        {
            this.Color = color;
        }
        public abstract void Draw(int gridSize, Graphics g);

        public abstract bool ContainsPoint(int gridSize, Point p);

        public abstract void Translate(int x, int y);

        public abstract Container GetBounds();

        public abstract Shape DeepClone();
    }
}
