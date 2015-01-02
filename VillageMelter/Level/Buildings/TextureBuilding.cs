using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageMelter.Base;
using VillageMelter.Level.Terrains;

namespace VillageMelter.Level.Buildings
{
    public class TextureBuilding : Building
    {

        Texture2D _texture;
        Texture2D _buildTexture;

        public TextureBuilding(ContentManager manager, string buildName)
            : this(manager.Load<Texture2D>(buildName), manager.Load<Texture2D>(buildName + "Build"))
        {
        }

        public TextureBuilding(Texture2D texture, Texture2D buildTexture)
        {
            _texture = texture;
            _buildTexture = buildTexture;
        }


        public override Base.Size GetSize()
        {
            return new Base.Size(_texture);
        }


        public override Texture2D GetBuildingTexture()
        {
            return _buildTexture;
        }

        public override bool CanPlace(World level, Rectangle rect)
        {
            return !level.HasTerrainType(Terrain.Water, rect);
        }

        public override BuildingInstance CreateInstance(int x, int y, Rotation orientation)
        {
            return new TextureBuildingInstance(this, x, y, orientation);
        }

        public override Texture2D GetTexture()
        {
            return _texture;
        }

    }
}
