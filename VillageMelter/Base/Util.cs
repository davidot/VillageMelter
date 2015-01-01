using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public class Util
    {

        public static Rectangle RectangleFromTwoPoints(int X1,int Y1,int X2,int Y2)
        {
            return RectangleFromTwoPoints(new Point(Math.Min(X1, X2), Math.Min(Y1, Y2)), new Point(Math.Max(X1, X2), Math.Max(Y1, Y2)));
        }


        public static Rectangle RectangleFromTwoPoints(Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
        }


    }
}
