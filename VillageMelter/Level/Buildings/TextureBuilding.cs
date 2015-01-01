﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageMelter.Base;

namespace VillageMelter.Level.Buildings
{
    public class TextureBuilding : Building
    {

        Texture2D _texture;
        Texture2D _buildTexture;

        public TextureBuilding(Texture2D texture,Texture2D buildTexture)
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

        public override bool CanPlace(Level level, Rectangle rect)
        {
            return level.HasTerrainType(Terrain.Water, rect);
        }

        public override BuildingInstance CreateInstance(int x,int y,Rotation orientation)
        {
            return new TextureBuildingInstance(this,x,y,orientation);
        }

        public override Texture2D GetTexture()
        {
            return _texture;
        }

    }
}