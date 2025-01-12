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

namespace Museum
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int timer;

        Texture2D museumTexture;
        Rectangle museumRect;

        Texture2D person1Texture;
        Rectangle person1Rect;

        Texture2D person2Texture;
        Rectangle person2Rect;

        Texture2D paintingTexture;
        Rectangle paintingRect;

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
            timer = 0;

            museumRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            person1Rect = new Rectangle(0, 350, 150, 150);

            person2Rect = new Rectangle(200, 350, 150, 150);

            paintingRect = new Rectangle(125, 350, 100, 100);

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
            museumTexture = Content.Load<Texture2D>("museum");

            person1Texture = Content.Load<Texture2D>("person1handsdown");

            person2Texture = Content.Load<Texture2D>("person2handsdown");

            paintingTexture = Content.Load<Texture2D>("painting");
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

            // Moving the people and painting towards the spot where the painting goes
            if (timer < 425)
            {
                person1Rect.X++;
                person2Rect.X++;
                paintingRect.X++;
            }

            // Raising the painting and people's arms
            else if (timer < 600)
            {
                paintingRect.Y--;
                person1Texture = Content.Load<Texture2D>("person1HandsUp");
                person2Texture = Content.Load<Texture2D>("person2HandsUp");
            }

            // Lowering their arms and keep on moving
            else
            {
                person1Texture = Content.Load<Texture2D>("person1handsdown");
                person2Texture = Content.Load<Texture2D>("person2handsdown");

                person1Rect.X++;
                person2Rect.X++;
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

            // Background
            spriteBatch.Draw(museumTexture, museumRect, Color.White);

            // People
            spriteBatch.Draw(person1Texture, person1Rect, Color.White);
            spriteBatch.Draw(person2Texture, person2Rect, Color.White);
            spriteBatch.Draw(paintingTexture, paintingRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
