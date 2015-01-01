using VillageMelter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace VillageMelter.Level.Buildings
{
    public abstract class BuildingInstance
    {

        protected Level Level
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

        public BuildingInstance(Level level, int x,int y,Rotation rot,Building b)
        {
            this.Level = level;
            this.X = x;
            this.Y = y;
            this.Orientation = rot;
            this.Bounds = b.GetSize().Rotate(rot);
        }

        public static implicit operator Rectangle(BuildingInstance building)
        {
            return building.Bounds.CreateRectangle(building.X, building.Y);
        }

    }
}
