using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Level.Buildings
{
    public class CircleBuilding : Building
    {
        public override BuildingInstance CreateInstance(int x, int y, Base.Rotation orientation)
        {
            throw new NotImplementedException();
        }

        public override bool CanPlace(World level, Microsoft.Xna.Framework.Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetBuildingTexture()
        {
            throw new NotImplementedException();
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture()
        {
            throw new NotImplementedException();
        }

        public override Base.Size GetSize()
        {
            throw new NotImplementedException();
        }
    }
}
