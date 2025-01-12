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

namespace HuffnPuff
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

        Random rand;

        int width, height;

        Texture2D HuffNPuff;

        Rectangle[] lImages, rImages, feathers;
        Rectangle boy, bob;
        Rectangle f, feather;

        int bleftAnim, bRightAnim;
        int fAnim;

        int timer;
        int timer2;

        int dx, dy;

        Boolean touched;
        int score;

        Boolean playAgain;

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
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

            touched = false;
            score = 0;

            timer = 0;
            timer2 = 0;

            oldKb = Keyboard.GetState();

            rand = new Random();

            lImages = new Rectangle[4] 
            {
                new Rectangle(0, 0, 24, 34),
                new Rectangle(25, 0, 24, 34),
                new Rectangle(50, 0, 24, 34),
                new Rectangle(75, 0, 24, 34)
            };

            rImages = new Rectangle[4]
            {
                new Rectangle(0, 35, 24, 34),
                new Rectangle(25, 35, 24, 34),
                new Rectangle(50, 35, 24, 34),
                new Rectangle(75, 35, 24, 34)
            };

            feathers = new Rectangle[4]
            {
                new Rectangle(0, 70, 24, 24),
                new Rectangle(25, 70, 24, 24),
                new Rectangle(50, 70, 24, 24),
                new Rectangle(75, 70, 24, 24)
            };

            bleftAnim = 0;
            bRightAnim = 0;
            fAnim = 0;

            boy = new Rectangle(width / 2, height - 50, 24, 34);
            f = new Rectangle(width / 2, 0, 24, 24);
            bob = lImages[0];
            feather = feathers[0];

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
            HuffNPuff = Content.Load<Texture2D>("HuffNPuff");
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

            // Resetting the game
            if (kb.IsKeyDown(Keys.R))
            {
                f.X = width / 2;
                f.Y = 0;
                boy.X = width / 2;
                boy.Y = height - 50;
                touched = false;
                score = 0;
            }

            if (!touched)
            {
                timer++;
                f.Y++;

                f.X += dx;
                f.Y += dy;

                // Resetting game if feather touches ground
                if (f.Y > height - 50)
                {
                    f.Y = height - 50;
                    touched = true;
                }

                // Animating Feather
                if (timer % 6 == 0 && fAnim == 3)
                {
                    fAnim = 0;
                    feather = feathers[0];
                }
                else if (timer % 6 == 0)
                {
                    fAnim++;
                    feather = feathers[fAnim];
                }

                // Animating player
                if (kb.IsKeyDown(Keys.Left))
                {
                    boy.X -= 2;

                    if (timer % 6 == 0 && bleftAnim == 3)
                    {
                        bob = lImages[0];
                        bleftAnim = 0;

                    }
                    else if (timer % 6 == 0)
                    {
                        bleftAnim++;
                        bob = lImages[bleftAnim];
                    }
                }

                if (kb.IsKeyDown(Keys.Right))
                {
                    boy.X += 2;

                    if (timer % 6 == 0 && bRightAnim == 3)
                    {
                        bRightAnim = 0;
                        bob = rImages[0];
                    }
                    else if (timer % 6 == 0)
                    {
                        bRightAnim++;
                        bob = rImages[bRightAnim];
                    }
                }

                // Restricting player within bounds
                if (boy.X <= 0)
                    boy.X = 0;
                else if (boy.X >= width - boy.Width)
                    boy.X = width - boy.Width;

                // Puffing feather based on space bar click
                if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space) && Math.Abs(boy.Y - f.Y) <= 100)
                {
                    timer2 = 0;
                    dy = rand.Next(-10, 0);
                    dx = rand.Next(-5, 5);

                    if (!touched)
                        score++;
                }

                if (timer2 < 10)
                {
                    timer2++;
                }
                else
                {
                    dx = 0;
                    dy = 0;
                }

                // Restricting feather within screen bounds
                if (f.X <= 0)
                    f.X = 0;
                else if (f.X >= width - f.Width)
                    f.X = width - f.Width;
                
                

                oldKb = kb;
                base.Update(gameTime);
            }

            else
            {

            }
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
            spriteBatch.Draw(HuffNPuff, boy, bob, Color.White);
            spriteBatch.Draw(HuffNPuff, f, feather, Color.White);
            spriteBatch.DrawString(font, score + "", new Vector2(width / 2, 0), Color.Black);
            if (touched)
            {
                spriteBatch.DrawString(font, "You Lost", new Vector2(width / 2 - 30, height / 2), Color.Black);
                spriteBatch.DrawString(font, "Press R to Reset", new Vector2(width / 2 - 75, height / 2 + 20), Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
