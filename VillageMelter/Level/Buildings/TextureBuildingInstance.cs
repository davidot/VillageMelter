using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageMelter.Base;

namespace VillageMelter.Level.Buildings
{
    public class TextureBuildingInstance : BuildingInstance
    {
        public TextureBuildingInstance(TextureBuilding b, int x, int y, Rotation r)
            : base(b, x, y, r)
        {
        }


        public override void Update()
        {
        }


    }
}
