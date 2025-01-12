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

namespace BeamMeUp_
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SoundEffect alert, autoDestruct, phaser3, replicator, phaserRicochet, redAlert;

        Texture2D bg;
        Rectangle bgR;

        KeyboardState oldKb;

        SpriteFont font;

        String options;

        int countdown;

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
            bgR = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            oldKb = Keyboard.GetState();

            options = "Sound Effect Menu:\nAlert - Press 1\nAuto Destruct Sequence - Press 2\nPhaser - Press 3\nReplicator - Press 4\nPhaser Ricochet - Press 5\nRed Alert - Press 6";

            countdown = 0;
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
            alert = Content.Load<SoundEffect>("alert10");
            autoDestruct = Content.Load<SoundEffect>("autodestructsequencearmed_ep");
            phaser3 = Content.Load<SoundEffect>("tng_phaser3_clean");
            replicator = Content.Load<SoundEffect>("tng_replicator");
            phaserRicochet = Content.Load<SoundEffect>("tos_phaser_ricochet");
            redAlert = Content.Load<SoundEffect>("tos_red_alert_3");

            bg = Content.Load<Texture2D>("trek1");

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

            if (countdown == 0)
            {
                if (kb.IsKeyDown(Keys.D1) && !oldKb.IsKeyDown(Keys.D1))
                {
                    alert.Play();
                    countdown = 120;
                }
                if (kb.IsKeyDown(Keys.D2) && !oldKb.IsKeyDown(Keys.D2))
                {
                    autoDestruct.Play();
                    countdown = 180;
                }
                if (kb.IsKeyDown(Keys.D3) && !oldKb.IsKeyDown(Keys.D3))
                {
                    phaser3.Play();
                    countdown = 120;
                }
                if (kb.IsKeyDown(Keys.D4) && !oldKb.IsKeyDown(Keys.D4))
                {
                    replicator.Play();
                    countdown = 300;
                }
                if (kb.IsKeyDown(Keys.D5) && !oldKb.IsKeyDown(Keys.D5))
                {
                    phaserRicochet.Play();
                    countdown = 600;
                }
                if (kb.IsKeyDown(Keys.D6) && !oldKb.IsKeyDown(Keys.D6))
                {
                    redAlert.Play();
                    countdown = 1200;
                }
            }
            else
                countdown--;


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

            spriteBatch.Draw(bg, bgR, Color.White);
            spriteBatch.DrawString(font, options, new Vector2(50, 100), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
