using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public class Animation
    {

        Texture2D[] textures;
        int frameDelay;
        int frame = 0;
        int delay = 0;
        Texture2D pauseTexture;

        bool _playing = false;

        public bool Playing
        {
            get { return _playing; }

            set { Play(value); }
        }

        public bool Loop
        {
            get;

            set;
        }

        public Animation(Texture2D[] textures, int frameDelay,Texture2D defaultTexture)
        {
            this.textures = textures;
            this.frameDelay = frameDelay;
            pauseTexture = defaultTexture;
        }

        public void Play(bool state)
        {
            _playing = state;
        }

        public void Stop()
        {
            Play(false);
            frame = 0;
            delay = 0;
        }

        public void Update()
        {
            if (Playing)
            {
                if (delay >= frameDelay)
                {
                    if ((frame + 1) >= textures.Length)
                    {
                        if (Loop)
                        {
                            frame = 0;
                        }
                        else
                        {
                            Play(false);
                        }
                    }
                    else
                    {
                        frame++;
                    }

                    delay = 0;
                }
                else
                {
                    delay++;
                }
            }
        }

        public Texture2D GetTexture()
        {
            if (!Playing)
                return pauseTexture;
            return textures[frame];
        }



    }
}
