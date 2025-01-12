using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SideKick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int WIDTH;
        int HEIGHT;

        Texture2D big, small, stationary;
        Rectangle bigR, smallR, stationaryR;

        Color smallC;
        Color bigC;

        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WIDTH = GraphicsDevice.Viewport.Width;
            HEIGHT = GraphicsDevice.Viewport.Height;

            bigR = new Rectangle(50, 40, 150, 100);
            smallR = new Rectangle(50, 150, 50, 50);
            stationaryR = new Rectangle(WIDTH / 2, HEIGHT / 2, 100, 100);

            smallC = Color.White;
            bigC = Color.White;

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

            // TODO: use this.Content to load your game content here
            big = Content.Load<Texture2D>("BigMovingObj");
            small = Content.Load<Texture2D>("SmallMovingObj");
            stationary = Content.Load<Texture2D>("StationaryObj");
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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            smallR.X++;
            bigR.X++;

            if (smallR.Intersects(new Rectangle(stationaryR.X, smallR.Y, stationaryR.Width, stationaryR.Height)))
                smallC = Color.Red;
            else if (!smallR.Intersects(new Rectangle(stationaryR.X, smallR.Y, stationaryR.Width, stationaryR.Height)))
                smallC = Color.White;

            if (bigR.Intersects(new Rectangle(stationaryR.X, bigR.Y, stationaryR.Width, stationaryR.Height)))
                bigC = Color.Red;
            else if (!bigR.Intersects(new Rectangle(stationaryR.X, bigR.Y, stationaryR.Width, stationaryR.Height)))
                bigC = Color.White;





            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(stationary, stationaryR, Color.White);
            spriteBatch.Draw(big, bigR, bigC);
            spriteBatch.Draw(small, smallR, smallC);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
