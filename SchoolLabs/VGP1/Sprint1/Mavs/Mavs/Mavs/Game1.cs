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

namespace Mavs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D basketballPlayer;
        Rectangle playerRect;

        Texture2D basketballTexture;
        Rectangle basketballRect;

        Texture2D backgroundTexture;
        Rectangle backgroundRect;

        SpriteFont scoreFont;

        int countdown;
        int timer;

        double initialVelX;
        double initialVelY;
        double ballAngle;

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

            // Graphics Initialization
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            basketballRect = new Rectangle(500, 340, 50, 50);

            playerRect = new Rectangle(450, 300, 100, 100);

            // Score Initialization
            countdown = 5;
            timer = 0;

            // Ball velocity Initialization
            ballAngle = MathHelper.ToRadians(90);

            initialVelX = -5 * Math.Cos(MathHelper.ToRadians((float)0.1));
            initialVelY = -4 * 5 * Math.Sin(ballAngle);

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
            basketballPlayer = Content.Load<Texture2D>("bballplayer");

            backgroundTexture = this.Content.Load<Texture2D>("ballCourt");

            basketballTexture = Content.Load<Texture2D>("basketball");

            scoreFont = Content.Load<SpriteFont>("SpriteFont1");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            timer++;


            // Countdown Timer
            if (timer == 60)
                countdown--;
            else if (timer == 120)
                countdown--;
            else if (timer == 180)
                countdown--;
            else if (timer == 240)
                countdown--;
            else if (timer == 300)
                countdown--;

            if ((timer / 60) == countdown)
                countdown--;

            // Change player animation / ball position
            if (countdown == 0)
            {
                basketballPlayer = Content.Load<Texture2D>("playerShooting1");
                basketballRect.X += (int)initialVelX;
                basketballRect.Y += (int)initialVelY;
                initialVelY += 0.4;
            }





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

            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            spriteBatch.Draw(basketballPlayer, playerRect, Color.White);

            spriteBatch.Draw(basketballTexture, basketballRect, Color.White);

            spriteBatch.DrawString(scoreFont, countdown.ToString(), new Vector2((GraphicsDevice.Viewport.Width / 2), 0), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
