﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public struct Size : ICloneable
    {

        private int _width;
        private int _height;

        public int Width
        {
            get { return _width; }

        }

        public int Height
        {
            get { return _height; }

        }

        public Size(int w, int h)
        {
            this._width = w;
            this._height = h;
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
                    return (Size)Clone();
                case Rotation.NORTH:
                case Rotation.SOUTH:
                default:
                    return new Size(Height, Width);
            }
        }


        public object Clone()
        {
            return new Size(Width, Height);
        }

        public override string ToString()
        {
            return "VillageMelter.Base.Size[Width=" + Width + ",Height=" + Height + "]";
        }
            
        public static Size FromTexture(Texture2D texture)
        {
            return new Size(texture.Width, texture.Height);
        }
        
        public static Size FromRectangle(Rectangle r)
        {
            return new Size(r.Width, r.Height);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(0, 0, Width, Height);
        }

        public static Rectangle ToRectangle(Size s)
        {
            return s.ToRectangle();
        }

        public static explicit operator Rectangle(Size s)
        {
            return ToRectangle(s);
        }

        public static explicit operator Size(Rectangle r)
        {
            return FromRectangle(r);
        }
             
    }
}
