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

namespace MiniMap
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D map;
        Rectangle mapR;

        Texture2D mini;
        Rectangle miniR;

        Texture2D player;
        Rectangle playerR;

        public Game1()
        {
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
            mapR = new Rectangle(0, 0, GraphicsDevice.Viewport.Width * 2, GraphicsDevice.Viewport.Height * 2);

            miniR = new Rectangle(0, GraphicsDevice.Viewport.Height - 150, 267, 150);

            playerR = new Rectangle(60, (GraphicsDevice.Viewport.Height - 150) + 20, 30, 30);

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
            map = Content.Load<Texture2D>("map");

            mini = map;

            player = Content.Load<Texture2D>("outline");
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
            GamePadState pad = GamePad.GetState(PlayerIndex.One);
            pad.ThumbSticks.Left.Normalize();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            // Moving around the map
            if (pad.ThumbSticks.Left.X >= 0.5)
            {
                mapR.X -= (int)(20 * pad.ThumbSticks.Left.X);
                playerR.X += 4;
            }

            if (pad.ThumbSticks.Left.X <= -0.5)
            {
                mapR.X -= (int)(20 * pad.ThumbSticks.Left.X);
                playerR.X -= 4;
            }

            if (pad.ThumbSticks.Left.Y >= 0.5)
            {
                mapR.Y += (int)(20 * pad.ThumbSticks.Left.Y);
                playerR.Y -= 3;
            }

            if (pad.ThumbSticks.Left.Y <= -0.5)
            {
                mapR.Y += (int)(20 * pad.ThumbSticks.Left.Y);
                playerR.Y += 3;
            }

            // Preventing user to move off screen
            if (mapR.X <= -800)
                mapR.X = -800;
            if (mapR.X >= 0)
                mapR.X = 0;

            if (mapR.Y <= -450)
                mapR.Y = -450;
            if (mapR.Y >= 0)
                mapR.Y = 0;

            // Preventing player indicator to move off minimap
            if (playerR.X >= 205)
                playerR.X = 205;
            if (playerR.X <= 60)
                playerR.X = 60;

            if (playerR.Y >= GraphicsDevice.Viewport.Height - 60)
                playerR.Y = GraphicsDevice.Viewport.Height - 60;
            if (playerR.Y <= GraphicsDevice.Viewport.Height - 130)
                playerR.Y = GraphicsDevice.Viewport.Height - 130;



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(map, mapR, Color.White);
            spriteBatch.Draw(mini, miniR, Color.White);
            spriteBatch.Draw(player, playerR, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
