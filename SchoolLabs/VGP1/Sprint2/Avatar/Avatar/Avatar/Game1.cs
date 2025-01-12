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

namespace Avatar
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float manTransparency, bishopTransparency, copTransparency, glassesTransparency, girlTransparency, outlineTransparency;

        GamePadState oldPad;

        Texture2D man, bishop, cop, glasses, girl;
        Rectangle manR, bishopR, copR, glassesR, girlR;

        Rectangle bigManR, bigBishopR, bigCopR, bigGlassesR, bigGirlR;

        Texture2D outline;
        Rectangle outlineR;

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
            manTransparency = 255;
            bishopTransparency = 255;
            copTransparency = 255;
            glassesTransparency = 255;
            girlTransparency = 255;
            outlineTransparency = 255;


            manR = new Rectangle(50, 50, 100, 100);
            bishopR = new Rectangle(200, 50, 100, 100);
            copR = new Rectangle(350, 50, 100, 100);
            glassesR = new Rectangle(500, 50, 100, 100);
            girlR = new Rectangle(650, 50, 100, 100);

            outlineR = new Rectangle(50, 50, 100, 100);

            oldPad = GamePad.GetState(PlayerIndex.One);


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
            man = Content.Load<Texture2D>("bearded man");
            bishop = Content.Load<Texture2D>("bishop");
            cop = Content.Load<Texture2D>("cop");
            glasses = Content.Load<Texture2D>("glasses");
            girl = Content.Load<Texture2D>("girl");

            

            outline = Content.Load<Texture2D>("outline");
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

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            // Checking to see if a certain avatar was clicked
            if (pad.Buttons.Start == ButtonState.Pressed && !(oldPad.Buttons.Start == ButtonState.Pressed))
            {
                // Changing Transparency of Each image
                manTransparency = 0;
                copTransparency = 0;
                glassesTransparency = 0;
                girlTransparency = 0;
                bishopTransparency = 0;
                outlineTransparency = 0;

                // Changing size and location of each image to center based on cursor location
                switch (outlineR.X)
                {
                    case 50:
                        manTransparency = 255;
                        manR.Width = 250;
                        manR.X = GraphicsDevice.Viewport.Width / 2 - 100;
                        manR.Height = 250;
                        manR.Y = GraphicsDevice.Viewport.Height / 2 - 100;
                        break;
                    case 200:
                        bishopTransparency = 255;
                        bishopR.Width = 250;
                        bishopR.X = GraphicsDevice.Viewport.Width / 2 - 100;
                        bishopR.Height = 250;
                        bishopR.Y = GraphicsDevice.Viewport.Height / 2 - 100;
                        break;
                    case 350:
                        copTransparency = 255;
                        copR.Width = 250;
                        copR.X = GraphicsDevice.Viewport.Width / 2 - 100;
                        copR.Height = 250;
                        copR.Y = GraphicsDevice.Viewport.Height / 2 - 100;
                        break;
                    case 500:
                        glassesTransparency = 255;
                        glassesR.Width = 250;
                        glassesR.X = GraphicsDevice.Viewport.Width / 2 - 100;
                        glassesR.Height = 250;
                        glassesR.Y = GraphicsDevice.Viewport.Height / 2 - 100;
                        break;
                    case 650:
                        girlTransparency = 255;
                        girlR.Width = 250;
                        girlR.X = GraphicsDevice.Viewport.Width / 2 - 100;
                        girlR.Height = 250;
                        girlR.Y = GraphicsDevice.Viewport.Height / 2 - 100;
                        break;
                }
            }

            if (pad.Buttons.Back == ButtonState.Pressed && !(oldPad.Buttons.Back == ButtonState.Pressed))
            {
                // Changing Transparency
                manTransparency = 255;
                bishopTransparency = 255;
                copTransparency = 255;
                glassesTransparency = 255;
                girlTransparency = 255;
                outlineTransparency = 255;

                // Resetting Positions and Sizes
                manR.X = 50;
                manR.Width = 100;
                manR.Y = 50;
                manR.Height = 100;

                bishopR.X = 200;
                bishopR.Width = 100;
                bishopR.Y = 50;
                bishopR.Height = 100;

                copR.X = 350;
                copR.Width = 100;
                copR.Y = 50;
                copR.Height = 100;

                glassesR.X = 500;
                glassesR.Width = 100;
                glassesR.Y = 50;
                glassesR.Height = 100;

                girlR.X = 650;
                girlR.Width = 100;
                girlR.Y = 50;
                girlR.Height = 100;
            }

            // To Move the Cursor
            if (outlineR.X == 800)
            {
                outlineR.X = 50;
            }

            if (outlineR.X == -100)
            {
                outlineR.X = 650;
            }

            if (pad.DPad.Left == ButtonState.Pressed && !(oldPad.DPad.Left == ButtonState.Pressed))
            {
                outlineR.X -= 150;
            }

            if (pad.DPad.Right == ButtonState.Pressed && !(oldPad.DPad.Right == ButtonState.Pressed))
            {
                outlineR.X += 150;
            }


            oldPad = pad;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GamePadState pad = GamePad.GetState(PlayerIndex.One);

            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(outline, outlineR, Color.Black * outlineTransparency);

            spriteBatch.Draw(man, manR, Color.White * manTransparency);
            spriteBatch.Draw(bishop, bishopR, Color.White * bishopTransparency);
            spriteBatch.Draw(cop, copR, Color.White * copTransparency);
            spriteBatch.Draw(glasses, glassesR, Color.White * glassesTransparency);
            spriteBatch.Draw(girl, girlR, Color.White * girlTransparency);

            spriteBatch.End();


            oldPad = pad;
            base.Draw(gameTime);
        }
    }
}
