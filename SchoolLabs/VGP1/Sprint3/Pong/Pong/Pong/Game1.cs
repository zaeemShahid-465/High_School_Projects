using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;

        KeyboardState oldKb;

        float spin;

        int screenWidth;
        int screenHeight;

        Texture2D spriteSheet;

        Rectangle bg, destBg;
        Rectangle mid;
        Vector2 posMid;

        Rectangle blue, dBlue, grey, dGrey;

        Texture2D ballTex;
        Rectangle ballRect;
        int ballSpeedX;
        int ballSpeedY;

        Rectangle top;
        Rectangle bottom;
        Rectangle left;
        Rectangle right;

        int bScore;
        int gScore;

        int bWins;
        int gWins;

        Boolean wFlagB;
        Boolean wFlagG;

        Boolean pointScored;

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
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            top = new Rectangle(0, 0, screenWidth, 5);
            bottom = new Rectangle(0, screenHeight, screenWidth, 20);
            left = new Rectangle(0, 0, 0, screenHeight);
            right = new Rectangle(screenWidth, 0, 0, screenHeight);
            ballRect = new Rectangle(50, 50, 20, 20);
            ballSpeedX = 2;
            ballSpeedY = 3;

            bWins = 0;
            gWins = 0;

            spin = new Random().Next(-3, 4);

            wFlagB = false;
            wFlagG = false;

            oldKb = Keyboard.GetState();

            destBg = new Rectangle(0, 0, 800, 480);
            bg = new Rectangle(0, 481, 800, 480);

            posMid = new Vector2(screenWidth / 2 - 8, 0);
            mid = new Rectangle(867, 714, 16, 16);

            blue = new Rectangle(801, 714, 32, 128);
            dBlue = new Rectangle(0, screenHeight / 2 - 32, 16, 64);

            grey = new Rectangle(834, 714, 32, 128);
            dGrey = new Rectangle(screenWidth - 16, screenHeight / 2 - 32, 16, 64);

            bScore = 0;
            gScore = 0;

            pointScored = false;

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
            ballTex = Content.Load<Texture2D>("orange ping pong ball");

            spriteSheet = Content.Load<Texture2D>("Pong Sprite Sheet");

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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            ballRect.X += ballSpeedX + (int)spin;
            ballRect.Y += ballSpeedY;

            // Moving paddles

            /// Blue Paddle
            if (kb.IsKeyDown(Keys.W))
            {
                if (dBlue.Y > 0)
                    dBlue.Y -= 6;
                else
                    dBlue.Y = 0;
            }

            if (kb.IsKeyDown(Keys.S))
            {
                if (dBlue.Y + dBlue.Height < screenHeight)
                    dBlue.Y += 6;
                else
                    dBlue.Y = 480 - dBlue.Height;
            }

            /// Grey Paddle
            if (kb.IsKeyDown(Keys.Up))
            {
                if (dGrey.Y > 0)
                    dGrey.Y -= 6;
                else
                    dGrey.Y = 0;
            }

            if (kb.IsKeyDown(Keys.Down))
            {
                if (dGrey.Y + dGrey.Height < screenHeight)
                    dGrey.Y += 6;
                else
                    dGrey.Y = 480 - dGrey.Height;
            }

            // Bouncing ball off paddles
            if (ballRect.Intersects(dBlue))
            {
                ballSpeedX *= -1;
            }
            if (ballRect.Intersects(dGrey))
            {
                ballSpeedX *= -1;
                spin--;
            }

            // Scoring when ball hits opposing side
            if (pointScored)
            {
                ballRect.X = screenWidth / 2;
                ballRect.Y = screenHeight / 2;
                spin = new Random().Next(-3, 4);

                ballSpeedX = new Random().Next(2, 7);
                ballSpeedY = new Random().Next(3, 9);

                pointScored = false;
            }

            if (wFlagG)
            {
                gWins++;
                wFlagG = false;
            }


            if (gScore >= 11 && Math.Abs(gScore - bScore) >= 2 && !wFlagG)
            {
                gScore = 0;
                bScore = 0;
                wFlagG = true;
            }



            if (bScore >= 11 && Math.Abs(gScore - bScore) >= 2 && !wFlagB)
            {
                wFlagB = true;
                gScore = 0;
                bScore = 0;
            }

            if (wFlagB)
            {
                bWins++;
                wFlagB = false;
            }

            if (ballRect.Intersects(left))
            {
                pointScored = true;
                gScore++;
            }
            if (ballRect.Intersects(right))
            {
                pointScored = true;
                bScore++;
            }

            // Bouncing ball across top and bottom
            if (ballRect.Intersects(top))
            {
                ballSpeedY *= -1;
                spin -= 0.5f;
            }
            if (ballRect.Intersects(bottom))
            {
                ballSpeedY *= -1;
                spin -= 0.5f;
            }

            oldKb = kb;
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
            spriteBatch.Draw(spriteSheet, destBg, bg, Color.White);
            spriteBatch.Draw(spriteSheet, dBlue, blue, Color.White);
            spriteBatch.Draw(spriteSheet, dGrey, grey, Color.White);
            spriteBatch.DrawString(font, bScore + "", new Vector2(350, screenHeight - 30), Color.LightBlue);
            spriteBatch.DrawString(font, gScore + "", new Vector2(440, screenHeight - 30), Color.LightGray);
            spriteBatch.DrawString(font, bWins + "", new Vector2(350, 0), Color.LightBlue);
            spriteBatch.DrawString(font, gWins + "", new Vector2(440, 0), Color.LightGray);


            // Drawing midpoint dashed line
            for (int i = 0; i <= screenHeight; i += 20)
            {
                posMid.Y = i;
                spriteBatch.Draw(spriteSheet, posMid, mid, Color.White);
            }

            spriteBatch.Draw(ballTex, top, Color.White);
            spriteBatch.Draw(ballTex, ballRect, null, Color.White, spin, new Vector2(ballTex.Width / 2, ballTex.Height / 2), SpriteEffects.None, 0f);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
