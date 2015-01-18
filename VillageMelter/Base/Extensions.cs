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

        #region Image extensions
        public static Texture2D GetPartOf(this Texture2D texture, Rectangle rect)
        {
            Color[] colors = new Color[rect.Width * rect.Height];
            texture.GetData<Color>(0, rect, colors, 0, colors.Length);
            Texture2D newTexture = new Texture2D(texture.GraphicsDevice, rect.Width, rect.Height);
            newTexture.SetData<Color>(colors);
            return newTexture;
        }


        public static IEnumerable<Texture2D> SplitHorizontal(this Texture2D texture, int times)
        {
            int timesWidth = texture.Width / times;
            for (int x = 0; x < texture.Width; x += timesWidth)
            {
                yield return texture.GetPartOf(new Rectangle(x, 0, timesWidth, texture.Height));
            }
        }

        public static IEnumerable<Texture2D> SplitVertical(this Texture2D texture, int times)
        {
            int timesHeight = texture.Height / times;
            for (int y = 0; y < texture.Height; y += timesHeight)
            {
                yield return texture.GetPartOf(new Rectangle(0, y, texture.Width, timesHeight));
            }
        }

        public static void DrawRectangle(this SpriteBatch batch,Texture2D t, GraphicsDevice device, Rectangle r, Color color,int width)
        {
            batch.Draw(t, new Rectangle(r.Left, r.Top, width, r.Height), color); // Left
            batch.Draw(t, new Rectangle(r.Right, r.Top, width, r.Height), color); // Right
            batch.Draw(t, new Rectangle(r.Left, r.Top, r.Width, width), color); // Top
            batch.Draw(t, new Rectangle(r.Left, r.Bottom, r.Width, width), color); // Bottom
        }
        #endregion


    }
}
