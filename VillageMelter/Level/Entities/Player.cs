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

        public Player(int x, int y) : base(x,y)
        {
        }

        public void OnWorldAdd()
        {
            Texture2D texture = World.LoadTexture<Texture2D>("entity/player");

        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture()
        {
            return animation.GetTexture();
        }

        public override void Update()
        {
            animation.Update();
        }


    }
}
