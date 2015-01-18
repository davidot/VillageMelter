using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageMelter.Base;

namespace VillageMelter.Level.Entities
{
    public class Player : Entity
    {

        Animation animation;
        InputHandler input;

        Size _bounds;

        public Player(int x, int y) : base(x,y)
        {
        }

        public override void OnWorldAdd()
        {
            Texture2D texture = World.LoadTexture<Texture2D>("entity/player");
            Texture2D[] textures = texture.SplitHorizontal(3).ToArray<Texture2D>();
            animation = new Animation(textures, 12, textures[0]);
            animation.Playing = true;
            animation.Loop = true;
            _bounds = animation.GetMaxBounds();
            input = World._input;
        }

        public override Texture2D GetTexture()
        {
            return animation.GetTexture();
        }

        int moveSpeed = 3;

        public override void Update()
        {
            animation.Update();
            bool left = input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left);
            bool right = input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right);
            bool up = input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up);
            bool down = input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down);
            if (left && !right)
            {
                Move(-moveSpeed, 0);
            }
            if (right && !left)
            {
                Move(moveSpeed, 0);
            }
            if (up && !down)
            {
                Move(0, -moveSpeed);
            }
            if (down && !up)
            {
                Move(0, moveSpeed);
            }
        }

        public override Size GetBounds()
        {
            return _bounds;
        }

    }
}
