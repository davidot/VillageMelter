using VillageMelter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageMelter.Level.Buildings
{
    public abstract class BuildingInstance
    {

        public Building BuildingType
        {
            get;

            private set;
        }

        public int X
        {
            get;

            private set;
        }

        public int Y
        {
            get;

            private set;
        }

        public Rotation Orientation
        {
            get;

            private set;
        }

        public Size Bounds
        {
            get;

            private set;
        }

        public BuildingInstance(Building bType, int x,int y,Rotation rot)
        {
            this.BuildingType = bType;
            this.X = x;
            this.Y = y;
            this.Orientation = rot;
            this.Bounds = BuildingType.GetSize().Rotate(rot);
        }



        public static explicit operator Rectangle(BuildingInstance building)
        {
            return building.Bounds.CreateRectangle(building.X, building.Y);
        }

        public Texture2D GetTexture()
        {
            return BuildingType.GetTexture();
        }


        public bool Contains(Point p)
        {
            return p.X >= X && p.X <= X + Bounds.Width &&
                    p.Y >= Y && p.Y <= Y + Bounds.Height;
        }

        public abstract void Update();
    }
}
