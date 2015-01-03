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

        bool _maypass;

        public DefualtTerrain(int id,Color color,bool mayPass) : base(id) {
            this._color = new Color[]{color};
            _maypass = mayPass;
        }

        public DefualtTerrain(int id, Color[] color,bool mayPass) : base(id)
        {
            this._color = color;
            _maypass = mayPass;
        }

        public override Color GetColor(int data)
        {
            return _color[data % _color.Length];
        }

        public override bool MayPass(Entities.Entity e, int data)
        {
            return _maypass;
        }

    }
}
