using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public static class Extensions
    {

        public static Texture2D GetPartOf(this Texture2D texture, Rectangle rect)
        {
            Color[] colors = new Color[rect.Width * rect.Height];
            texture.GetData<Color>(0, rect, colors, 0, colors.Length);
            Texture2D newTexture = new Texture2D(texture.GraphicsDevice, rect.Width, rect.Height);
            newTexture.SetData<Color>(colors);
            return newTexture;
        }


    }
}
