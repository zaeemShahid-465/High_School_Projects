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

namespace FollowMe
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Boolean isMovingTowardsMouse;

        MouseState oldMouse;

        Texture2D temp;

        Texture2D person;
        Rectangle personR;



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
            isMovingTowardsMouse = false;

            oldMouse = Mouse.GetState();

            personR = new Rectangle(GraphicsDevice.Viewport.Width - 200, 100, 73, 270);

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
            person = Content.Load<Texture2D>("Standing Man");
            temp = Content.Load<Texture2D>("Walking Man");
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
            MouseState mouse = Mouse.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here



            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed)
            {
                person = temp;
                temp = person;

                isMovingTowardsMouse = true;

                if (isMovingTowardsMouse)
                {
                    personR.X += (mouse.X - personR.X) / 5;
                    personR.Y += (mouse.Y - personR.Y) / 5;
                }
            }

            else
            {
                isMovingTowardsMouse = false;
                person = Content.Load<Texture2D>("Standing Man");
            }



            oldMouse = mouse;

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

            spriteBatch.Draw(person, personR, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
