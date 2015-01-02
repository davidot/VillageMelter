using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public enum Rotation
    {
        NORTH = 1,
        EAST,
        SOUTH,
        WEST
    }

    public static class Extensions
    {
        public static float ToGraphicRotation(this Rotation rot)
        {
            return (float)((((float)rot)/2) * Math.PI);
        }
    }
}
