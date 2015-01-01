using VillageMelter.Base;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace VillageMelter.Level.Buildings
{
    public abstract class Building
    {


        public abstract BuildingInstance CreateInstance();

        public abstract bool CanPlace(Level level, Rectangle rect);

        public abstract Texture2D GetBuildingTexture();

        public abstract Size GetSize();
    }
}
