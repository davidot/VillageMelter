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
            Components.Add(new FrameRateCounter(this));
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

            //Instances

            input = new InputHandler(this);
            level = new World(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight,this);


            //graphics.ToggleFullScreen();
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

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

            Size currentSize = (Size) GraphicsDevice.Viewport.Bounds;
            
            level.Render(spriteBatch,graphics.GraphicsDevice,currentSize);

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
