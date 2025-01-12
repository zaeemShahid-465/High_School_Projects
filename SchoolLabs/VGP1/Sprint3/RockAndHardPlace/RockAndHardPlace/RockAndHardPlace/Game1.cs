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

namespace RockAndHardPlace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int width, height;

        Texture2D hard, rock;
        Rectangle hardR, rockR;

        Color rockC;

        int speed;

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

            speed = 10;

            rockC = Color.White;

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
            hard = Content.Load<Texture2D>("Hard Place");
            int hWidth = hard.Width / 2;
            int hHeight = hard.Height / 2;

            rock = Content.Load<Texture2D>("Rock");

            hardR = new Rectangle(width / 2 - (hWidth / 2), height / 2 - (hHeight / 2), hard.Width / 2, hard.Height / 2);
            rockR = new Rectangle(width / 2 - (hWidth / 2) + 150, height / 2 - (hHeight / 2), rock.Width, rock.Height);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (isOverlapping(rockR, hardR))
                rockC = Color.Red;
            else
                rockC = Color.White;

            if (kb.IsKeyDown(Keys.Up))
                rockR.Y -= speed;
            if (kb.IsKeyDown(Keys.Down))
                rockR.Y += speed;
            if (kb.IsKeyDown(Keys.Left))
                rockR.X -= speed;
            if (kb.IsKeyDown(Keys.Right))
                rockR.X += speed;


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
            spriteBatch.Draw(hard, hardR, Color.White);
            spriteBatch.Draw(rock, rockR, rockC);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Boolean isOverlapping(Rectangle r1, Rectangle r2)
        {
            if (r1.X + r1.Width >= r2.X && r1.X <= r2.X + r2.Width)
            {
                if (r1.Y + r1.Height >= r2.Y && r1.Y <= r2.Height + r2.Y)
                    return true;
            }

            return false;
        }
    }
}
