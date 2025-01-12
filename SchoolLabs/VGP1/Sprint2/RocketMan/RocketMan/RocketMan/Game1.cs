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

namespace RocketMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Boolean flag;

        SpriteFont font;

        Texture2D ship;
        Rectangle shipR;

        int speed;
        int maxSpeed;

        double heading;

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
            shipR = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 - 50, 100, 100);

            flag = false;

            speed = 15;
            maxSpeed = 30;

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
            ship = Content.Load<Texture2D>("spaceship-removebg-preview");

            font = Content.Load<SpriteFont>("SpriteFont1");
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
            Vector2 right = pad.ThumbSticks.Right;
            heading = MathHelper.ToDegrees((float)Math.Atan2(-right.X, right.Y));
            

            if (shipR.X > GraphicsDevice.Viewport.Width + 100)
                shipR.X = -100;

            else if (shipR.X < -100)
                shipR.X = GraphicsDevice.Viewport.Width + 100;

            if (shipR.Y < -100)
                shipR.Y = GraphicsDevice.Viewport.Height + 100;

            else if (shipR.Y > GraphicsDevice.Viewport.Height + 100)
                shipR.Y = -100;

            if (pad.ThumbSticks.Left.Y >= 0.5f && !flag && (speed < maxSpeed && speed > 0))
            {
                speed++;
                flag = true;
            }

            else if (speed == 0 && !flag)
            {
                if(pad.ThumbSticks.Left.Y >= 0.5f && !flag)
                {
                    speed++;
                    flag = true;
                }
            }

            if (pad.ThumbSticks.Left.Y <= -0.5 & !flag && (speed < maxSpeed && speed > 0))
            {
                speed--;
                flag = true;
            }

            else if (speed == 30 && !flag)
            {
                if (pad.ThumbSticks.Left.Y <= -0.5)
                {
                    speed--;
                    flag = true;
                }
            }



            if (pad.ThumbSticks.Left.Y == 0)
                flag = false;

            shipR.X += (int)(speed * pad.ThumbSticks.Right.X);
            shipR.Y -= (int)(speed * pad.ThumbSticks.Right.Y);

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

            spriteBatch.Draw(ship, shipR, Color.White);
            spriteBatch.DrawString(font, "Speed: " + speed, new Vector2(50, 25), Color.Black);
            spriteBatch.DrawString(font, "Heading: " + Math.Round(heading, 2), new Vector2(50, 50), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
