using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageMelter;
using Microsoft.Xna.Framework.Graphics;
using VillageMelter.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using VillageMelter.Level.Buildings;

namespace VillageMelter.Level
{
    public class Level
    {

        public const int TerrainSize = 4;

        private int _width;

        public int Width
        {
            get { return this._width; }
        }

        private int _height;



        public int Height
        {
            get { return this._height; }
        }

        int[][,] terrains;


        public const int TerrainIndex = 0;
        public const int DataIndex = 1;

        int xScroll = 0;
        int yScroll = 0;

        private int _zoom = 1;

        Building test;

        private List<BuildingInstance> buildings = new List<BuildingInstance>();

        public int Zoom
        {
            get { return _zoom; }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("Cant set zoom to less than 1");
                double oldZoom = _zoom;
                double newZoom = value;
                _zoom = value;
                xScroll = (int)((xScroll / oldZoom) * newZoom);
                yScroll = (int)((yScroll / oldZoom) * newZoom);
            }
        }

        public Level(int width, int height)
        {
            this._width = width;
            this._height = height;

            terrains = new int[2][,];
            for (int i = 0; i < terrains.Length; i++)
            {
                terrains[i] = new int[width, height];
            }

            Random rand = new Random();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    terrains[TerrainIndex][x, y] = (rand.Next(2) == 0 ? Terrain.Grass : Terrain.Dirt).ID;
                    terrains[DataIndex][x, y] = rand.Next(2);
                }
            }
            LoadContent();
        }

        public Terrain GetTerrain(int x, int y)
        {
            if (InRange(x, y))
            {
                return Terrain.get(terrains[TerrainIndex][x, y]);
            }
            return Terrain.NullTerrain;
        }

        public int GetData(int x, int y)
        {
            if (InRange(x, y))
            {
                return terrains[DataIndex][x, y];
            }
            return 0;
        }

        public bool InRange(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;
            return true;
        }


        public Color GetColor(int x, int y)
        {
            return GetTerrain(x, y).GetColor(GetData(x, y));
        }


        public void Render(SpriteBatch spriteBatch, GraphicsDevice graphics, Size renderSize)
        {

            int terrainZoomSize = TerrainSize * Zoom;


            Texture2D texture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });

            int xScrollOff = xScroll % terrainZoomSize;
            int yScrollOff = yScroll % terrainZoomSize;

            int firstXTile = (xScroll) / terrainZoomSize;
            int firstYTile = (yScroll) / terrainZoomSize;

            int lastXTile = (xScroll + renderSize.Width) / terrainZoomSize;
            int lastYTile = (yScroll + renderSize.Height) / terrainZoomSize;

            Rectangle renderRect = Util.RectangleFromTwoPoints(firstXTile, firstYTile, lastXTile, lastYTile);



            for (int x = firstXTile; x <= lastXTile; x++)
            {
                for (int y = firstYTile; y <= lastYTile; y++)
                {

                    spriteBatch.Draw(texture, new Rectangle((x - firstXTile) * terrainZoomSize - xScrollOff, (y - firstYTile) * terrainZoomSize - yScrollOff, terrainZoomSize, terrainZoomSize), GetColor(x, y));
                }
            }

            foreach(BuildingInstance building in buildings)
            {
                if(renderRect.Intersects(building))
                {
                    //todo render building
                }
            }

        }

        private int scrollChange = 4;

        public void Update(InputHandler handler)
        {
            if (handler.IsKeyDown(Keys.Left))
            {
                xScroll = Math.Max(0, xScroll - scrollChange);
            }
            if (handler.IsKeyDown(Keys.Right))
            {
                xScroll = Math.Min(Width * Zoom, xScroll + scrollChange);
            }

            if (handler.IsKeyDown(Keys.Up))
            {
                yScroll = Math.Max(0, yScroll - scrollChange);
            }
            if (handler.IsKeyDown(Keys.Down))
            {
                yScroll = Math.Min(Height * Zoom, yScroll + scrollChange);
            }
            if (handler.IsLeftMouseDown())
            {
                Point p = handler.MousePosition();
                int xPos = p.X / (TerrainSize * Zoom) - xScroll;
                int yPos = p.Y / (TerrainSize * Zoom) - yScroll;
                
            }
            int scrollWheel = handler.ScrollWheelDifference();
            if (scrollWheel > 0)
            {
                Zoom++;
            }
            if (scrollWheel < 0 && Zoom > 1)
            {
                Zoom--;
            }
            

        }

        public void Center(Point point, Size renderSize)
        {



        }

        public void Add(BuildingInstance b)
        {
            buildings.Add(b);
        }



        public bool HasTerrainType(Terrain search, Rectangle rect)
        {
            for (int x = rect.X; x < rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    if (GetTerrain(x, y) == search)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
