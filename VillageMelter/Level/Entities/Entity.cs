﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageMelter.Base;

namespace VillageMelter.Level.Entities
{
    public abstract class Entity
    {

        public Rotation Direction
        {
            get;

            internal set;
        }

        public Size Bounds
        {
            get { return GetBounds(); }

        }

        public int X
        {
            get;

            internal set;
        }

        public int Y
        {
            get;

            internal set;
        }

        public int dX
        {
            get;

            internal set;
        }

        public int dY
        {
            get;

            internal set;
        }

        public bool Alive
        {
            get;

            private set;
        }

        protected World World 
        {
            get;

            private set;
        }

        public Entity(int x,int y)
        {
            X = x;
            Y = y;
        }

        public void AddToWorld(World world)
        {
            this.World = world;
            OnWorldAdd();
        }

        public abstract void OnWorldAdd();

        public void Move(int dX, int dY)
        {
            if (dX != 0 && dY != 0)
            {
                Move(dX, 0);
                Move(0, dY);
                return;
            }
            World.CanMove(this, dX, dY);
        }

        public Rectangle GetSourceRectangle()
        {
            return Rectangle.Empty;
        }

        public void Draw(SpriteBatch batch)
        {
            Rectangle source = GetSourceRectangle();
            Texture2D texture = GetTexture();
            if (source == Rectangle.Empty) 
            { 
                source = texture.Bounds;
            }
            batch.Draw(texture, new Vector2(X, Y), source, Color.White, Direction.ToGraphicRotation(), new Vector2(source.Width / 2, source.Height / 2),World.Zoom, SpriteEffects.None, 1.1f);
        }

        public abstract Texture2D GetTexture();

        public abstract void Update();

        public abstract Size GetBounds();

        public static explicit operator Rectangle(Entity e)
        {
            return e.Bounds.CreateRectangle(e.X, e.Y);
        }

    }
}
