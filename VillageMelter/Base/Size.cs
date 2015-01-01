using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public class Size
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

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }


        public Rectangle CreateRectangle(int X, int Y)
        {
            return new Rectangle(X, Y, Width, Height);
        }

    }
}
