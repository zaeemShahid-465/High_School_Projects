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

namespace YouveBeenTargeted
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState oldMouse;

        int width, height;

        Texture2D tank;
        Rectangle tankR;

        Vector2 origin;
        Vector2 center;
        Vector2 mousePos;

        float rotation;

        Texture2D bullet;
        Rectangle bulletR;

        int dx, dy;

        int speed;

        int frames;

        double hyp = 0;

        double numUpdates = 0;

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

            oldMouse = Mouse.GetState();

            bulletR = new Rectangle(999, 999, 16, 16);

            dx = 0;
            dy = 0;

            speed = 5;

            rotation = 0;

            frames = 0;

            center = new Vector2(tankR.Width / 2, tankR.Height / 2);

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
            tank = Content.Load<Texture2D>("Tank");

            bullet = Content.Load<Texture2D>("White Square");

            tankR = new Rectangle(width / 2, height / 2, tank.Width / 2, tank.Height / 2);
            origin = new Vector2(tank.Width / 2, tank.Height / 2);
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
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            bulletR.X += dx;
            bulletR.Y += dy;



            Vector2 mousePos = new Vector2(mouse.X, mouse.Y);
            Vector2 distance = new Vector2(mouse.X - tankR.X, mouse.Y - tankR.Y);
            Vector2 normalDis = distance - center;

            rotation = (float)Math.Atan2(normalDis.Y, normalDis.X);



            if (mouse.LeftButton == ButtonState.Pressed && !(oldMouse.LeftButton == ButtonState.Pressed))
            {
                bulletR.X = tankR.X;
                bulletR.Y = tankR.Y;

                hyp = Math.Sqrt(Math.Pow(normalDis.X, 2) + Math.Pow(normalDis.Y, 2));

                numUpdates = hyp / speed;

                dx = (int)(normalDis.X / numUpdates);
                dy = (int)(normalDis.Y / numUpdates);
            }

            else if (numUpdates != 0)
            {
                frames++;
                if (frames > numUpdates)
                {
                    dx = 0;
                    dy = 0;
                    frames = 0;
                    numUpdates = 0;
                }
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
            spriteBatch.Draw(tank, tankR, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
            spriteBatch.Draw(bullet, bulletR, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
