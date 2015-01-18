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
            get { return Bounds.X; }
        }

        public int Y
        {
            get { return Bounds.Y; }
        }

        public Rotation Orientation
        {
            get;

            private set;
        }

        public Rectangle Bounds
        {
            get;

            private set;
        }

        public BuildingInstance(Building bType, int x,int y,Rotation rot)
        {
            this.BuildingType = bType;
            this.Orientation = rot;
            this.Bounds = BuildingType.GetSize().Rotate(rot).CreateRectangle(x,y); //Get the size of the building, rotate it and add the coordinates
        }



        public static explicit operator Rectangle(BuildingInstance building)
        {
            return building.Bounds;
        }

        public Texture2D GetTexture()
        {
            return BuildingType.GetTexture();
        }


        public bool BlockEntity(Point p)
        {
            return p.X >= X && p.X <= X + Bounds.Width &&
                    p.Y >= Y && p.Y <= Y + Bounds.Height;
        }

        public abstract void Update();
    }
}
