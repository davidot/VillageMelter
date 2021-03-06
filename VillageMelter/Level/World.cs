﻿using System;
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
using VillageMelter.Level.Terrains;
using Microsoft.Xna.Framework.Content;
using VillageMelter.Level.Entities;

namespace VillageMelter.Level
{
    public class World : GameComponent
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

        public Size LastRenderSize
        {
            get;

            private set;
        }

        int[][,] terrains;


        public const int TerrainIndex = 0;
        public const int DataIndex = 1;

        int xScroll = 0;
        int yScroll = 0;

        private int _zoom = 1;

        Building test;

        List<BuildingInstance> buildings = new List<BuildingInstance>();

        List<Entity> entities = new List<Entity>();

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

        public InputHandler _input;

        public InputHandler GetInput()
        {
            return _input;
        }

        public World(int width, int height,VillageMelter game) : base(game)
        {
            this._width = width;
            this._height = height;

            _zoom = 3;

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
            _input = game.input;
            LoadContent();
        }

        ContentManager _contentManager;
        Texture2D testImage;

        public void LoadContent()
        {
            
            _contentManager = new ContentManager(this.Game.Services,"Content");
            test = new TextureBuilding(_contentManager, "image/cityHall");
            testImage = _contentManager.Load<Texture2D>("entity/player");
            Add(new Player(10,10));
        }

        public Terrain GetTerrain(int x, int y)
        {
            x /= TerrainSize;
            y /= TerrainSize;
            if (InRange(x, y))
            {
                return Terrain.get(terrains[TerrainIndex][x, y]);
            }
            return Terrain.NullTerrain;
        }

        public int GetData(int x, int y)
        {
            x /= TerrainSize;
            y /= TerrainSize;
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

            LastRenderSize = renderSize;

            Texture2D texture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });

            int renderTileWidth = (renderSize.Width + terrainZoomSize - 1) / terrainZoomSize;
            int renderTileHeight = (renderSize.Height + terrainZoomSize - 1) / terrainZoomSize;

            int subXScroll = xScroll % TerrainSize;
            int subYScroll = yScroll % TerrainSize;

            int firstTileX = xScroll / TerrainSize;
            int firstTileY = yScroll / TerrainSize;

            int lastX = firstTileX + renderTileWidth;
            int lastY = firstTileY + renderTileHeight;

            Rectangle renderRect = Util.RectangleFromTwoPoints(xScroll, yScroll, xScroll + renderSize.Width/Zoom, yScroll + renderSize.Height/Zoom);

            for (int x = firstTileX; x <= lastX; x++)
            {
                for (int y = firstTileY; y <= lastY; y++)
                {
                    spriteBatch.Draw(texture, new Rectangle((x  - firstTileX)* terrainZoomSize - subXScroll, (y  - firstTileY)* terrainZoomSize - subYScroll, terrainZoomSize, terrainZoomSize), GetColor(x * TerrainSize, y * TerrainSize));
                }
            }

            var t = new Texture2D(graphics, 1, 1);
            t.SetData(new[] { Color.White });

            foreach(BuildingInstance building in buildings)
            {
                if(renderRect.Intersects((Rectangle)building))
                {
                    spriteBatch.Draw(building.GetTexture(), new Vector2((building.X - xScroll) * Zoom + building.Bounds.Width/2, (building.Y - yScroll) * Zoom + building.Bounds.Height/2), null, Color.White, building.Orientation.ToGraphicRotation(), new Vector2(building.Bounds.Width/2,building.Bounds.Height/2), (float)Zoom, SpriteEffects.None, 1.0f);
                    Rectangle r = new Rectangle((building.X - xScroll) * Zoom, (building.Y - yScroll) * Zoom, (building.Bounds.Width) * Zoom, (building.Bounds.Height) * Zoom);
                    spriteBatch.DrawRectangle(t, graphics, r, Color.Blue, 2);
                }
            }

            foreach (Entity entity in entities)
            {

                if (renderRect.Intersects((Rectangle)entity))
                {
                    Texture2D entityTexture = entity.GetTexture();

                    spriteBatch.Draw(entityTexture, new Vector2((entity.X - xScroll) * Zoom, (entity.Y - yScroll) * Zoom), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), (float)Zoom, entity.Direction == Rotation.WEST ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);

                    Rectangle r = new Rectangle((entity.X - xScroll) * Zoom, (entity.Y - yScroll) * Zoom, (entity.Bounds.Width) * Zoom, (entity.Bounds.Height) * Zoom);
                    spriteBatch.DrawRectangle(t, graphics, r, Color.Red, 2);
                }
            }
        }

        public bool MayPass(Entity e, int x, int y)
        {
            return GetTerrain(x, y).MayPass(e, GetData(x, y));
        }

        public void CanMove(Entity e, int dX, int dY)
        {
            if (dX != 0 && dY != 0)
            {
                Console.WriteLine("Cant move in two directions");
                return;
            }
            int maxD = 0;
            int xTime = 0, yTime = 0;
            if (Math.Abs(dX) > Math.Abs(dY))
            {
                maxD = Math.Abs(dX);
                xTime = dX < 0 ? -1:1;
            }
            else
            {
                maxD = Math.Abs(dY);
                yTime = dY < 0 ? -1 : 1;
            }

            int currentX = e.X;
            int currentY = e.Y;

            int currentXSide = currentX + e.Bounds.Width;
            int currentYSide = currentX + e.Bounds.Height;
            int i = 0;
            for (i = 0; i < maxD; i++)
            {
                currentX += xTime;
                currentY += yTime;
                currentXSide = currentX + e.Bounds.Width;
                currentYSide = currentY + e.Bounds.Height;
                if (!MayPass(e, currentX, currentY) || !MayPass(e, currentXSide, currentYSide))
                {
                    Console.WriteLine("Blocked by tile");

                    break;
                }
                Point[] points = new Point[] { new Point(currentX, currentY), new Point(currentX, currentYSide), new Point(currentXSide, currentY), new Point(currentXSide, currentYSide) };

                if (HasBuildingAtPoints(points))
                {
                    Console.WriteLine("Blocked by building");

                    break;
                }

            }

            e.dX += i * xTime;
            e.dY += i * yTime;
        }


        public void Update()
        {
            if (_input.IsLeftMousePressed() || _input.IsRightButtonDown())
            {
                Point p = _input.MousePosition();
                int xPos = (p.X + xScroll);
                int yPos = (p.Y + yScroll);
                Rotation r = (Rotation)new Random().Next(4) + 1;
                Rectangle placeRect = test.GetSize().Rotate(r).CreateRectangle(xPos,yPos);
                if(test.CanPlace(this,placeRect))
                    Add(test.CreateInstance(xPos, yPos,r));
            }
            int scrollWheel = _input.ScrollWheelDifference();
            scrollWheel /= 120; //TODO check if its different for different mices
            if (scrollWheel > 0)
            {
                Zoom+= (int)Math.Sqrt(scrollWheel);
            }
            if (scrollWheel < 0 && Zoom > 1)
            {
                int tempZoom = Zoom - (int)Math.Sqrt(Math.Abs(scrollWheel));
                Zoom = Math.Max(tempZoom, 1);
            }
            foreach (BuildingInstance building in buildings)
            {
                building.Update();
            }
            foreach (Entity entity in entities)
            {
                entity.Update();
                if (entity.dX != 0)
                {
                    if (entity.dX > 0)
                    {
                        entity.X += entity.dX;
                        entity.dX = 0;
                        entity.Direction = Rotation.EAST;
                    }
                    else if (entity.dX < 0)
                    {
                        entity.X += entity.dX;
                        entity.dX = 0;
                        entity.Direction = Rotation.WEST;
                    }
                }
                if (entity.dY != 0)
                {
                    if (entity.dY > 0)
                    {
                        entity.Y += entity.dY;
                        entity.dY = 0;
                        entity.Direction = Rotation.SOUTH;
                    }
                    else if (entity.dY < 0)
                    {
                        entity.Y += entity.dY;
                        entity.dY = 0;
                        entity.Direction = Rotation.NORTH;
                    }
                }
                if (entity is Player)
                {
                    xScroll = Math.Max(0, entity.X - LastRenderSize.Width / 2 * Zoom);
                    yScroll = Math.Max(0, entity.Y - LastRenderSize.Height / 2 * Zoom);
                }
                
            }

        }

        public void Center(Point point, Size renderSize)
        {
            int maxWidthTiles = (renderSize.Width + 31) / 32;
            int maxHeightTiles = (renderSize.Height + 31) / 32;
        }

        public void Add(BuildingInstance b)
        {
            buildings.Add(b);
        }

        public void Add(Entity entity)
        {
            entities.Add(entity);
            entity.AddToWorld(this);
        }

        public IEnumerable<BuildingInstance> GetBuildingsAt(int x, int y)
        {
            Point p = new Point(x, y);
            foreach (BuildingInstance building in buildings)
            {
                if (building.BlockEntity(p))
                {
                    yield return building;
                }
            }
        }

        public IEnumerable<BuildingInstance> GetBuildingsAtPoints(params Point[] points)
        {
            foreach (BuildingInstance building in buildings)
            {
                foreach (Point point in points)
                {
                    if (building.BlockEntity(point))
                    {
                        yield return building;
                    }
                }
            }
        }

        public bool HasBuildingAtPoints(params Point[] points)
        {
            foreach (BuildingInstance building in buildings)
            {
                foreach (Point point in points)
                {
                    if (building.BlockEntity(point))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasTerrainType(Terrains.Terrain search, Rectangle rect)
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

        public T LoadTexture<T>(string name)
        {
            return _contentManager.Load<T>(name);
        }

    }
}
