using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Level.Terrains
{
    class DefualtTerrain : Terrain
    {

        private Color[] _color;

        public Color[] Color
        {
            get { return _color; }
        }

        public DefualtTerrain(int id,Color color) : base(id) {
            this._color = new Color[]{color};
        }

        public DefualtTerrain(int id, Color[] color) : base(id)
        {
            this._color = color;
        }

        public override Color GetColor(int data)
        {
            return _color[data % _color.Length];
        }


    }
}
