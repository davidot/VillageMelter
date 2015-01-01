using VillageMelter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Level
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

        public Rotation Rotation
        {
            get;

            private set;
        }

        public BuildingInstance(Level level, int x,int y,Rotation rot)
        {
            this.Level = level;
            this.X = x;
            this.Y = y;
            this.Rotation = rot;
        }



    }
}
