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

namespace Borg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Boolean isBorgActive;

        int frames1;

        Texture2D borg;
        Rectangle borgR;

        int[] tubePosX = new int[4];
        int[] tubePosY = new int[4];

        Random rand;

        int frames;
        int framesBorgIsActive;

        int randTime;

        SpriteFont font;

        int charge;
        int power;

        int timer;
        int timer2;
        int fireCooldown;
        int framesSinceLastFire;

        KeyboardState oldKB;

        Texture2D turret;
        Rectangle turretR;

        Texture2D tube1, tube2, tube3, tube4;
        Rectangle tube1R, tube2R, tube3R, tube4R;

        Texture2D outline;
        Rectangle outlineR;

        Texture2D torpedo;
        Rectangle torpedoR;

        int torpedoXInc;
        int torpedoYInc;

        Color tubeC;

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
            isBorgActive = false;

            rand = new Random();

            randTime = rand.Next(60, 500);

            framesBorgIsActive = rand.Next(60, 180);

            frames1 = 0;

            borgR = new Rectangle(999, 999, 100, 50);

            oldKB = Keyboard.GetState();

            frames = 0;

            // Turret Charge
            charge = 100;

            // Turret Power
            power = 0;

            // Timer to control cursor color
            timer = 0;

            // timer to add to charge
            timer2 = 0;

            // Controls Torpedo Cool Down
            fireCooldown = 120;
            framesSinceLastFire = fireCooldown;

            // Torpedo
            torpedoR = new Rectangle(9999, 9999, 63, 63);

            // Turret
            turretR = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 130, GraphicsDevice.Viewport.Height / 2 - 100, 260, 180);

            // Outline Curosor
            outlineR = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 35, 110, 70, 100);

            // Launch Tubes
            tube1R = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 35, 110, 70, 100);
            tube2R = new Rectangle(445, 210, 100, 70);
            tube3R = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 35, 280, 70, 100);
            tube4R = new Rectangle(255, 210, 100, 70);

            // Initializing Color of Tube
            tubeC = Color.Green;

            torpedoXInc = 1;
            torpedoYInc = 1;

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

            // Borg image loading
            borg = Content.Load<Texture2D>("borg-removebg-preview");

            // Turret Image Loading
            turret = Content.Load<Texture2D>("turret-removebg-preview");

            // Launch tube image loading
            tube1 = Content.Load<Texture2D>("outline");
            tube2 = Content.Load<Texture2D>("outline");
            tube3 = Content.Load<Texture2D>("outline");
            tube4 = Content.Load<Texture2D>("outline");

            // outline loading
            outline = Content.Load<Texture2D>("outline");

            // Torpedo loading
            torpedo = Content.Load<Texture2D>("torpedo-removebg-preview");

            // Font loading
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

            framesSinceLastFire++;

            torpedoR.X += torpedoXInc;
            torpedoR.Y += torpedoYInc;

            timer++;

            // Changing which launch tube is selected
            if (kb.IsKeyDown(Keys.Right) && !oldKB.IsKeyDown(Keys.Right))     
                outlineR = tube2R; 

            else if (kb.IsKeyDown(Keys.Left) && !oldKB.IsKeyDown(Keys.Left))
                outlineR = tube4R;

            else if (kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Up))
                outlineR = tube1R;

            else if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down))
                outlineR = tube3R;

            // Firing launch tube
            if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space))
            {
                charge -= power;

                // Changes color to red
                if (timer < 180)
                    tubeC = Color.Red;

                // Fires Torpedo
                if (framesSinceLastFire >= fireCooldown)
                {
                    fire(torpedoR);
                    framesSinceLastFire = 0;
                }

                
            }

            // Changing tube Color back to green
            if (timer / 60 > 2)
            {
                timer = 0;
                tubeC = Color.Green;
            }

            // Selecting power from numpad
            if (power < charge)
            {
                if (kb.IsKeyDown(Keys.NumPad0) && !oldKB.IsKeyDown(Keys.NumPad0))
                    power = 0;
                else if (kb.IsKeyDown(Keys.NumPad1) && !oldKB.IsKeyDown(Keys.NumPad1))
                    power = 1;
                else if (kb.IsKeyDown(Keys.NumPad2) && !oldKB.IsKeyDown(Keys.NumPad2))
                    power = 2;
                else if (kb.IsKeyDown(Keys.NumPad3) && !oldKB.IsKeyDown(Keys.NumPad3))
                    power = 3;
                else if (kb.IsKeyDown(Keys.NumPad4) && !oldKB.IsKeyDown(Keys.NumPad4))
                    power = 4;
                else if (kb.IsKeyDown(Keys.NumPad5) && !oldKB.IsKeyDown(Keys.NumPad5))
                    power = 5;
                else if (kb.IsKeyDown(Keys.NumPad6) && !oldKB.IsKeyDown(Keys.NumPad6))
                    power = 6;
                else if (kb.IsKeyDown(Keys.NumPad7) && !oldKB.IsKeyDown(Keys.NumPad7))
                    power = 7;
                else if (kb.IsKeyDown(Keys.NumPad8) && !oldKB.IsKeyDown(Keys.NumPad8))
                    power = 8;
                else if (kb.IsKeyDown(Keys.NumPad9) && !oldKB.IsKeyDown(Keys.NumPad9))
                    power = 9;
            }

            // Adding to charge every second
            timer2++;
            if (timer2 > 60)
            {
                timer2 = 0;
                charge += 3;
            }
            if (charge > 100)
                charge = 100;

            // Changing torpedo size based on energy chosen by user
            switch (power)
            {
                case 1:
                    torpedoR.Width = 75;
                    torpedoR.Height = 75;
                    break;
                case 2:
                    torpedoR.Width = 85;
                    torpedoR.Height = 85;
                    break;
                case 3:
                    torpedoR.Width = 95;
                    torpedoR.Height = 95;
                    break;
                case 4:
                    torpedoR.Width = 105;
                    torpedoR.Height = 105;
                    break;
                case 5:
                    torpedoR.Width = 115;
                    torpedoR.Height = 115;
                    break;
                case 6:
                    torpedoR.Width = 125;
                    torpedoR.Height = 125;
                    break;
                case 7:
                    torpedoR.Width = 135;
                    torpedoR.Height = 135;
                    break;
                case 8:
                    torpedoR.Width = 145;
                    torpedoR.Height = 145;
                    break;
                case 9:
                    torpedoR.Width = 155;
                    torpedoR.Height = 155;
                    break;
                default:
                    torpedoR.Width = 65;
                    torpedoR.Height = 65;
                    break;
            }


            // Randomly spawning in Borgs

            frames++;

            if (frames >= randTime && !isBorgActive)
            {
                frames = 0;
                spawnBorg();
            }

            if (isBorgActive)
            {
                frames1++;

                if (frames1 >= framesBorgIsActive)
                {
                    isBorgActive = false;
                    frames1 = 0;
                }
            }


            oldKB = kb;
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

            spriteBatch.Draw(torpedo, torpedoR, Color.White);
            spriteBatch.Draw(turret, turretR, Color.White);
            spriteBatch.Draw(tube1, tube1R, Color.Black);
            spriteBatch.Draw(tube2, tube2R, Color.Black);
            spriteBatch.Draw(tube3, tube3R, Color.Black);
            spriteBatch.Draw(tube4, tube4R, Color.Black);
            spriteBatch.Draw(outline, outlineR, tubeC);
            spriteBatch.DrawString(font, "" + charge, new Vector2(369, 220), Color.White);
            spriteBatch.DrawString(font, "" + power, new Vector2(GraphicsDevice.Viewport.Width - 50, GraphicsDevice.Viewport.Height - 50), Color.Black);

            if (isBorgActive)
                spriteBatch.Draw(borg, borgR, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void fire(Rectangle torpedo)
        {
            if (outlineR.Equals(tube1R))
            {
                torpedoR.X = tube1R.X;
                torpedoR.Y = tube1R.Y;
                torpedoXInc = 0;
                torpedoYInc = -3;
            }

            else if (outlineR.Equals(tube2R))
            {
                torpedoR.X = tube2R.X;
                torpedoR.Y = tube2R.Y;
                torpedoXInc = 3;
                torpedoYInc = 0;
            }

            else if (outlineR.Equals(tube3R))
            {
                torpedoR.X = tube3R.X;
                torpedoR.Y = tube3R.Y;
                torpedoXInc = 0;
                torpedoYInc = 3;
            }

            else if (outlineR.Equals(tube4R))
            {
                torpedoR.X = tube4R.X;
                torpedoR.Y = tube4R.Y;
                torpedoXInc = -3;
                torpedoYInc = 0;
            }
        }

        public void spawnBorg()
        {
            Rectangle[] launchTubes = { tube1R, tube2R, tube3R, tube4R };
            int randTube = rand.Next(0, launchTubes.Length);

            int randYPos = 0;
            int randXPos = 0;

            switch (randTube)
            {
                case 0:
                    randXPos = rand.Next(tube1R.X - 50, tube1R.X + 40);
                    randYPos = rand.Next(tube1R.Y - 100, tube1R.Y + 40);
                    break;
                case 1:
                    randXPos = rand.Next(tube2R.X + 40, tube2R.X + 100);
                    randYPos = rand.Next(tube2R.Y - 50, tube2R.Y + 50);
                    break;
                case 2:
                    randXPos = rand.Next(tube3R.X - 50, tube3R.X + 50);
                    randYPos = rand.Next(tube3R.Y + 40, tube3R.Y + 100);
                    break;
                case 3:
                    randXPos = rand.Next(tube4R.X - 100, tube4R.X - 40);
                    randYPos = rand.Next(tube4R.Y - 50, tube4R.Y +  50);
                    break;
            }

            isBorgActive = true;
            borgR.X = randXPos;
            borgR.Y = randYPos;
        }
    }
}
