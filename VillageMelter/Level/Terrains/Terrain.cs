using VillageMelter.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Level.Terrains
{
    public abstract class Terrain
    {
        private static Dictionary<int, Terrain> terrains = new Dictionary<int, Terrain>();

        public static Terrain NullTerrain
        {
            get;

            private set;
        }

        public static Terrain Dirt
        {
            get;

            private set;
        }

        public static Terrain Grass
        {
            get;

            private set;
        }

        public static Terrain Water
        {
            get;

            private set;
        }

        private static bool _init = false;

        public static void Init()
        {
            if (_init)
            {
                return;
            }
            _init = true;
            NullTerrain = new DefualtTerrain(0,Color.Black);
            Dirt = new DefualtTerrain(1, new Color[] { new Color(89, 61, 40), new Color(121, 86, 58) });
            Grass = new DefualtTerrain(2, new Color[] { Color.Green, new Color(55,119,66) });
            Water = new DefualtTerrain(3, new Color[] { Color.DarkBlue, Color.Blue });
        }


        private int _id;

        public int ID {
            get {return _id;}
        }

        public Terrain(int id)
        {
            if (terrains.ContainsKey(id))
            {
                throw new InvalidOperationException("Error already have a tile with this id");
            }
            this._id = id;
            terrains.Add(id,this);
        }

        public abstract Color GetColor(int data);

        public static Terrain get(int id) {
            if (terrains.ContainsKey(id))
            {
                return terrains[id];
            }
            return NullTerrain;
        }


    }
}
