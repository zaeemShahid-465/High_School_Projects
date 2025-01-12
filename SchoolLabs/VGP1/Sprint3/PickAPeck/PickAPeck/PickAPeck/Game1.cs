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

namespace PickAPeck
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState oldKb;

        SpriteFont font;

        Texture2D sheet;
        Rectangle[] images;

        Rectangle i1, i2, i3, i4, i5;

        int largeI;

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
            images = new Rectangle[5] { 
                new Rectangle(278, 0, 50, 50), 
                new Rectangle(0, 201, 150, 50), 
                new Rectangle(227, 0, 50, 150), 
                new Rectangle(151, 0, 75, 200), 
                new Rectangle(0, 0, 150, 200) 
            };

            i1 = new Rectangle(50, 0, images[0].Width, images[0].Height);
            i2 = new Rectangle(150, 0, images[1].Width, images[1].Height);
            i3 = new Rectangle(350, 0, images[2].Width, images[2].Height);
            i4 = new Rectangle(450, 0, images[3].Width, images[3].Height);
            i5 = new Rectangle(550, 0, images[4].Width, images[4].Height);

            largeI = 0;

            oldKb = Keyboard.GetState();

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
            sheet = Content.Load<Texture2D>("SpriteSheet");
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


            Keys[] keys = kb.GetPressedKeys();

            if (kb.IsKeyDown(Keys.D1) && !oldKb.IsKeyDown(Keys.D1) && largeI != 1)
            {
                largeI = 1;
                i1 = new Rectangle(50, 0, images[0].Width * 2, images[0].Height * 2);
                i2 = new Rectangle(150, 0, images[1].Width, images[1].Height);
                i3 = new Rectangle(350, 0, images[2].Width, images[2].Height);
                i4 = new Rectangle(450, 0, images[3].Width, images[3].Height);
                i5 = new Rectangle(550, 0, images[4].Width, images[4].Height);
            }

            else if (kb.IsKeyDown(Keys.D2) && !oldKb.IsKeyDown(Keys.D2) && largeI != 2)
            {
                largeI = 2;
                i1 = new Rectangle(50, 0, images[0].Width, images[0].Height);
                i2 = new Rectangle(150, 0, images[1].Width * 2, images[1].Height * 2);
                i3 = new Rectangle(350, 0, images[2].Width, images[2].Height);
                i4 = new Rectangle(450, 0, images[3].Width, images[3].Height);
                i5 = new Rectangle(550, 0, images[4].Width, images[4].Height);
            }

            else if (kb.IsKeyDown(Keys.D3) && !oldKb.IsKeyDown(Keys.D3) && largeI != 3)
            {
                largeI = 3;
                i1 = new Rectangle(50, 0, images[0].Width, images[0].Height);
                i2 = new Rectangle(150, 0, images[1].Width, images[1].Height);
                i3 = new Rectangle(350, 0, images[2].Width * 2, images[2].Height * 2);
                i4 = new Rectangle(450, 0, images[3].Width, images[3].Height);
                i5 = new Rectangle(550, 0, images[4].Width, images[4].Height);
            }

            else if (kb.IsKeyDown(Keys.D4) && !oldKb.IsKeyDown(Keys.D4) && largeI != 4)
            {
                largeI = 4;
                i1 = new Rectangle(50, 0, images[0].Width, images[0].Height);
                i2 = new Rectangle(150, 0, images[1].Width, images[1].Height);
                i3 = new Rectangle(350, 0, images[2].Width, images[2].Height);
                i4 = new Rectangle(450, 0, images[3].Width * 2, images[3].Height * 2);
                i5 = new Rectangle(550, 0, images[4].Width, images[4].Height);
            }

            else if (kb.IsKeyDown(Keys.D5) && !oldKb.IsKeyDown(Keys.D5) && largeI != 5)
            {
                largeI = 5;
                i1 = new Rectangle(50, 0, images[0].Width, images[0].Height);
                i2 = new Rectangle(150, 0, images[1].Width, images[1].Height);
                i3 = new Rectangle(350, 0, images[2].Width, images[2].Height);
                i4 = new Rectangle(450, 0, images[3].Width, images[3].Height);
                i5 = new Rectangle(550, 0, images[4].Width * 2, images[4].Height * 2);
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
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(sheet, i1, images[0], Color.White);
            spriteBatch.Draw(sheet, i2, images[1], Color.White);
            spriteBatch.Draw(sheet, i3, images[2], Color.White);
            spriteBatch.Draw(sheet, i4, images[3], Color.White);
            spriteBatch.Draw(sheet, i5, images[4], Color.White);

            spriteBatch.DrawString(font, "1", new Vector2(70, 250), Color.Black);
            spriteBatch.DrawString(font, "2", new Vector2(220, 250), Color.Black);
            spriteBatch.DrawString(font, "3", new Vector2(370, 250), Color.Black);
            spriteBatch.DrawString(font, "4", new Vector2(480, 250), Color.Black);
            spriteBatch.DrawString(font, "5", new Vector2(600, 250), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
