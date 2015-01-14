using VillageMelter.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using VillageMelter.Level.Terrains;
using VillageMelter.Level;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System;

namespace VillageMelter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class VillageMelter : Game
    {

        public const int DefaultWidth = 1000;

        public const int DefaultHeight = DefaultWidth / 16 * 9;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<ContentManager> managers = new List<ContentManager>();
        SpriteFont font;
        Level.World level;

        internal InputHandler input;

        float scale = 1.0f;
        Texture2D image;
        Texture2D[] players;

        public VillageMelter()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = DefaultWidth;
            graphics.PreferredBackBufferHeight = DefaultHeight;

            this.IsMouseVisible = true;
            this.Window.Title = "Village melter";
            this.Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Initing");

            
            
            Terrain.Init();

            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Console.WriteLine("Loading content");

            //Initial content

            font = Content.Load<SpriteFont>("DorpBuilderFont");
            image = Content.Load<Texture2D>("image/cityHall");

            Texture2D texture = Content.Load<Texture2D>("entity/player");
            players = texture.SplitHorizontal(3).ToArray<Texture2D>();

            //Instances

            input = new InputHandler(this);
            level = new World(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight,this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            input.Update();

            level.Update();

            // TODO: Add your update logic here
            scale += 0.01f;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp,DepthStencilState.None,RasterizerState.CullCounterClockwise);

            Size currentSize = new Size(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //foreach (Vector2 loc in buildingLocation)
            //{
                //spriteBatch.Draw(image, new Vector2(loc.X - image.Width / 2, loc.Y - image.Height / 2), Color.White);
            //}

            //spriteBatch.Draw(image, new Vector2(mouseLocation.X-image.Width/2, mouseLocation.Y-image.Height/2), Color.White);
            
            //spriteBatch.DrawString(font, text, new Vector2(x, 150), Color.Black,1f,middle,1f,SpriteEffects.None,1f);
            //spriteBatch.DrawString(font, rotation + " f", new Vector2(250, 250), Color.Chocolate);
            //spriteBatch.DrawString(font, gameTime.ElapsedGameTime.TotalMilliseconds + "", new Vector2(300, 300), Color.DarkMagenta);
            //spriteBatch.DrawString(font, time + "", new Vector2(500, 300), Color.DarkMagenta);
            level.Render(spriteBatch,graphics.GraphicsDevice,currentSize);

            spriteBatch.DrawString(font, "This is a string", new Vector2(10, 10), Color.Black);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Unload()
        {
            foreach(ContentManager manager in managers)
            {
                manager.Unload();
            }

            Content.Unload();
        }

        public ContentManager CreateNewContentManager()
        {
            ContentManager manager = new ContentManager(Services, "Content");
            managers.Add(manager);
            return manager;
        }

    }
}
