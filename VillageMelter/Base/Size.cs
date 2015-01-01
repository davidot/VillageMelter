using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public class Size : ICloneable
    {


        public int Width
        {
            get;

            private set;
        }

        public int Height
        {
            get;

            private set;
        }

        public Size(Microsoft.Xna.Framework.Graphics.Texture2D texture) : this(texture.Width, texture.Height)
        {
        }

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        
        public Rectangle CreateRectangle(int X, int Y)
        {
            return new Rectangle(X, Y, Width, Height);
        }


        public Size Rotate(Rotation rot)
        {
            switch (rot)
            {
                case Rotation.EAST:
                case Rotation.WEST:
                    return new Size(Height, Width);
                case Rotation.NORTH:
                case Rotation.SOUTH:
                default:
                    return (Size)Clone();
            }
        }


        public object Clone()
        {
            return new Size(Width, Height);
        }

    }
}
