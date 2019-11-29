using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombat
{
    public class Point2D
    {
        int X { get; set; }
        int Y { get; set; }

        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int DistanceTo(Point2D other)
        {
            return (int)
                Math.Sqrt(
                    Math.Pow(other.X - this.X, 2)
                    +
                    Math.Pow(other.Y - this.Y, 2)
                );
        }

    }
}
