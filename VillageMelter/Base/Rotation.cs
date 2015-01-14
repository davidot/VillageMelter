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
    public static class RotationMethods
    {
        public static float ToGraphicRotation(this Rotation rot)
        {
            return (float)((((float)rot) / 2) * Math.PI);
        }

        public static Rotation GetOpposite(this Rotation rot)
        {
            switch (rot)
            {
                case Rotation.NORTH:
                    return Rotation.SOUTH;
                case Rotation.EAST:
                    return Rotation.WEST;
                case Rotation.SOUTH:
                    return Rotation.NORTH;
                case Rotation.WEST:
                    return Rotation.EAST;
                default:
                    return Rotation.NORTH;
            }
        }    

    }

}
