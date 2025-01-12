using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MaxBorg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        int maxPropTimer;
        SpriteBatch spriteBatch;
        Texture2D explosion;
        Rectangle explosionR;
        SoundEffect torpedoSound;
        SoundEffect phaserSound;
        Texture2D chargeBar, propBar, explodeBar;
        Rectangle chargeR, propR, explodeR;
        Boolean phaserActive;
        int phaserTimer;
        Texture2D phaser;
        Rectangle phaserR;
        Boolean phaserFired;
        Color phaserColor;
        GamePadState oldPad;
        int phaserPower;
        Boolean leftXF;
        Boolean leftYF;
        Boolean leftTrigF;
        Boolean rightTrigF;
        int startingTorp;
        Boolean torpedoFired;
        MouseState oldMouse;
        int torpedoDistanceTraveled;
        int distanceTorpedoTraveling;
        double mousetube1X;
        double mousetube1Y;
        double mousetube2X;
        double mousetube2Y;
        double mousetube3X;
        double mousetube3Y;
        double mousetube4X;
        double mousetube4Y;
        double mouseTube1Distance;
        double mouseTube2Distance;
        double mouseTube3Distance;
        double mouseTube4Distance;
        // Borg's Bomb 
        Texture2D bomb;
        Rectangle bombR;
        int bombXInc;
        int bombYInc;
        // Borg
        Boolean isBorgActive;
        Texture2D borg;
        Rectangle borgR;
        int frames1;
        Texture2D temp;
        int randTube;
        Random rand;
        int frames;
        int framesBorgIsActive;
        int randTime;
        SpriteFont font;
        int charge;
        int power;
        int propulsionEnergy;
        int timer;
        int timer2;
        int fireCooldown;
        int framesSinceLastFire;
        KeyboardState oldKB;
        // Turret
        Texture2D turret;
        Rectangle turretR;
        // Tubes/Outline
        Texture2D tube1, tube2, tube3, tube4;
        Rectangle tube1R, tube2R, tube3R, tube4R;
        Texture2D outline;
        Rectangle outlineR;
        // Torpedo
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

        protected override void Initialize()
        {
            chargeR = new Rectangle(50, 25, 200, 30);
            propR = new Rectangle(50, 75, 200, 30);
            explodeR = new Rectangle(50, 120, 200, 30);
            oldPad = GamePad.GetState(PlayerIndex.One);
            phaserTimer = 0;
            maxPropTimer = 0;
            explosionR = new Rectangle(999, 999, 200, 200);
            phaserColor = Color.Green;
            phaserFired = false;
            leftXF = false;
            leftYF = false;
            leftTrigF = false;
            rightTrigF = false;
            phaserActive = false;
            phaserR = new Rectangle(999, 999, 400, 50);
            oldMouse = Mouse.GetState();
            torpedoFired = false;
            distanceTorpedoTraveling = 0;
            propulsionEnergy = 0;
            bombR = new Rectangle(999, 999, 100, 100);
            bombXInc = 0;
            bombYInc = 0;
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

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            // Borg's Bomb Image
            bomb = Content.Load<Texture2D>("bomb-removebg-preview");
            // power bars
            explodeBar = Content.Load<Texture2D>("outline");
            chargeBar = Content.Load<Texture2D>("outline");
            propBar = Content.Load<Texture2D>("outline");
            phaser = outline;
            torpedoSound = Content.Load<SoundEffect>("tng_torpedo_clean");
            phaserSound = Content.Load<SoundEffect>("tos_phaser_ricochet");
            explosion = Content.Load<Texture2D>("images-removebg-preview");
            temp = Content.Load<Texture2D>("torpedo-removebg-preview");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            GamePadState pad = GamePad.GetState(PlayerIndex.One);
            pad.ThumbSticks.Left.Normalize();

            // Selecting tubes with controller dpad
            if (pad.DPad.Up == ButtonState.Pressed && !(oldPad.DPad.Up == ButtonState.Pressed)) outlineR = tube1R;
            if (pad.DPad.Left == ButtonState.Pressed && !(oldPad.DPad.Left == ButtonState.Pressed)) outlineR = tube4R;
            if (pad.DPad.Down == ButtonState.Pressed && !(oldPad.DPad.Down == ButtonState.Pressed)) outlineR = tube3R;
            if (pad.DPad.Right == ButtonState.Pressed && !(oldPad.DPad.Right == ButtonState.Pressed)) outlineR = tube2R;

            // Incrementing/Decrementing propulsion energy with horizontal left stick movement
            if (propulsionEnergy > 0 && propulsionEnergy < 9)
            {
                if (pad.ThumbSticks.Left.X >= 0.5 && !leftXF)
                {
                    propulsionEnergy++;
                    leftXF = true;
                }

                if (pad.ThumbSticks.Left.X <= -0.5 && !leftXF)
                {
                    propulsionEnergy--;
                    leftXF = true;
                }
            }

            else if (propulsionEnergy == 0)
            {
                if (pad.ThumbSticks.Left.X >= 0.5 && !leftXF)
                {
                    propulsionEnergy++;
                    leftXF = true;
                }
            }

            else if (propulsionEnergy == 9)
            {
                if (pad.ThumbSticks.Left.X <= -0.5 && !leftXF)
                {
                    propulsionEnergy--;
                    leftXF = true;
                }
            }

            if (pad.ThumbSticks.Left.X == 0) leftXF = false;

            // inc/dec explosive power using vertical left stick movement
            if (power > 0 && power < 9)
            {
                if (pad.ThumbSticks.Left.Y >= 0.5 && !leftYF)
                {
                    power++;
                    leftYF = true;
                }

                if (pad.ThumbSticks.Left.Y <= -0.5 && !leftYF)
                {
                    power--;
                    leftYF = true;
                }
            }

            else if (power == 0)
            {
                if (pad.ThumbSticks.Left.Y >= 0.5 && !leftYF)
                {
                    power++;
                    leftYF = true;
                }
            }

            else if (power == 9)
            {
                if (pad.ThumbSticks.Left.Y <= -0.5 && !leftYF)
                {
                    power--;
                    leftYF = true;
                }
            }

            if (pad.ThumbSticks.Left.Y == 0) leftYF = false;

            // Firing torpedo with left trigger

            if (pad.Triggers.Left >= 0.5 && !leftTrigF)
            {
                // Changing Color to Red
                if (timer < 180) tubeC = Color.Red;

                // Firing Torpedo
                if (framesSinceLastFire >= fireCooldown)
                {
                    fire(torpedoR);
                    torpedoFired = true;
                    framesSinceLastFire = 0;
                    charge -= power;
                }
            }

            if (pad.Triggers.Left == 0) leftTrigF = false;

            // Selecting phaser power through AB/XY Buttons
            if (pad.Buttons.A == ButtonState.Pressed && !(oldPad.Buttons.A == ButtonState.Pressed)) phaserPower = 25;
            if (pad.Buttons.B == ButtonState.Pressed && !(oldPad.Buttons.B == ButtonState.Pressed)) phaserPower = 50;
            if (pad.Buttons.X == ButtonState.Pressed && !(oldPad.Buttons.X == ButtonState.Pressed)) phaserPower = 75;
            if (pad.Buttons.Y == ButtonState.Pressed && !(oldPad.Buttons.Y == ButtonState.Pressed)) phaserPower = 100;

            // Changing phaser color based on phaser power

            switch (phaserPower)
            {
                case 25:
                    phaserColor = Color.Green;
                    break;
                case 50:
                    phaserColor = Color.Red;
                    break;
                case 75:
                    phaserColor = Color.Blue;
                    break;
                case 100:
                    phaserColor = Color.Yellow;
                    break;
            }

            // Firing phaser
            if (phaserFired)
            {
                phaserTimer++;
                phaserActive = true;
            }

            if (phaserTimer > 120)
            {
                phaserTimer = 0;
                phaserR.X = 999;
                phaserR.Y = 999;
                phaserFired = false;
                phaserActive = false;
            }

            if (!phaserActive)
            {
                if (pad.Triggers.Right >= 0.5 && !rightTrigF)
                {
                    firePhaser();
                    rightTrigF = true;
                    phaserFired = true;
                }
            }

            if (pad.Triggers.Right == 0) rightTrigF = false;

            // updating power bars
            chargeR.Width = charge;
            propR.Width = propulsionEnergy * 20;
            explodeR.Width = power * 20;

            // Bomb increment
            bombR.X += bombXInc;
            bombR.Y += bombYInc;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            torpedoDistanceTraveled = propulsionEnergy * 100;

            framesSinceLastFire++;

            torpedoR.X += torpedoXInc;
            torpedoR.Y += torpedoYInc;

            if (Math.Abs(torpedoR.X - startingTorp) >= propulsionEnergy * 30)
            {
                torpedoR.X = 999;
                torpedoR.Y = 999;
                distanceTorpedoTraveling = 0;
                torpedo = Content.Load<Texture2D>("torpedo-removebg-preview");
                torpedoFired = false;
            }

            if (Math.Abs(torpedoR.Y - startingTorp) >= propulsionEnergy * 80)
            {
                torpedoR.X = 999;
                torpedo = Content.Load<Texture2D>("torpedo-removebg-preview");
                torpedoR.Y = 999;
                distanceTorpedoTraveling = 0;
                torpedoFired = false;
            }

            timer++;

            // Finding mouse position from tubes
            mousetube1X = Math.Abs(mouse.X - tube1R.X);
            mousetube1Y = Math.Abs(mouse.Y - tube1R.Y);
            mouseTube1Distance = Math.Sqrt((Math.Pow(mousetube1X, 2) + Math.Pow(mousetube1Y, 2)));

            mousetube2X = Math.Abs(mouse.X - tube2R.X);
            mousetube2Y = Math.Abs(mouse.Y - tube2R.Y);
            mouseTube2Distance = Math.Sqrt((Math.Pow(mousetube2X, 2) + Math.Pow(mousetube2Y, 2)));

            mousetube3X = Math.Abs(mouse.X - tube3R.X);
            mousetube3Y = Math.Abs(mouse.Y - tube3R.Y);
            mouseTube3Distance = Math.Sqrt((Math.Pow(mousetube3X, 2) + Math.Pow(mousetube3Y, 2)));

            mousetube4X = Math.Abs(mouse.X - tube4R.X);
            mousetube4Y = Math.Abs(mouse.Y - tube4R.Y);
            mouseTube4Distance = Math.Sqrt((Math.Pow(mousetube4X, 2) + Math.Pow(mousetube4Y, 2)));

            double min = Math.Min(Math.Min(mouseTube1Distance, mouseTube2Distance), Math.Min(mouseTube3Distance, mouseTube4Distance));

            // Changing which launch tube is selected based on arrow keys
            if (kb.IsKeyDown(Keys.Right) && !oldKB.IsKeyDown(Keys.Right)) outlineR = tube2R;
            else if (kb.IsKeyDown(Keys.Left) && !oldKB.IsKeyDown(Keys.Left)) outlineR = tube4R;
            else if (kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Up)) outlineR = tube1R;
            else if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down)) outlineR = tube3R;

            // Selecting Tube with mouse
            if (oldMouse.X != mouse.X)
            {
                if (min == mouseTube1Distance) outlineR = tube1R;
                else if (min == mouseTube2Distance) outlineR = tube2R;
                else if (min == mouseTube3Distance) outlineR = tube3R;
                else if (min == mouseTube4Distance) outlineR = tube4R;
            }


            // Firing launch tube based on mouse click
            if (mouse.LeftButton == ButtonState.Pressed && !(oldMouse.LeftButton == ButtonState.Pressed))
            {
                // Changing Color to Red
                if (timer < 180) tubeC = Color.Red;

                // Firing Torpedo
                if (framesSinceLastFire >= fireCooldown)
                {
                    fire(torpedoR);
                    torpedoFired = true;
                    framesSinceLastFire = 0;
                    charge -= power;
                }
            }

            // Firing launch tube
            if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space))
            {
                if (framesSinceLastFire >= fireCooldown) charge -= power;

                // Changes color to red
                if (timer < 180) tubeC = Color.Red;

                // Fires Torpedo
                if (framesSinceLastFire >= fireCooldown)
                {
                    fire(torpedoR);
                    torpedoFired = true;
                    framesSinceLastFire = 0;
                }
            }

            if (torpedoFired)
            {
                distanceTorpedoTraveling++;
                maxPropTimer++;
            }

            if (maxPropTimer >= 80 && propulsionEnergy == 9)
            {
                temp = torpedo;
                torpedo = explosion;
                maxPropTimer = 0;
            }

            if (torpedoR.X == 999)
            {
                torpedo = temp;
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
                if (kb.IsKeyDown(Keys.NumPad0) && !oldKB.IsKeyDown(Keys.NumPad0)) power = 0;
                else if (kb.IsKeyDown(Keys.NumPad1) && !oldKB.IsKeyDown(Keys.NumPad1)) power = 1;
                else if (kb.IsKeyDown(Keys.NumPad2) && !oldKB.IsKeyDown(Keys.NumPad2)) power = 2;
                else if (kb.IsKeyDown(Keys.NumPad3) && !oldKB.IsKeyDown(Keys.NumPad3)) power = 3;
                else if (kb.IsKeyDown(Keys.NumPad4) && !oldKB.IsKeyDown(Keys.NumPad4)) power = 4;
                else if (kb.IsKeyDown(Keys.NumPad5) && !oldKB.IsKeyDown(Keys.NumPad5)) power = 5;
                else if (kb.IsKeyDown(Keys.NumPad6) && !oldKB.IsKeyDown(Keys.NumPad6)) power = 6;
                else if (kb.IsKeyDown(Keys.NumPad7) && !oldKB.IsKeyDown(Keys.NumPad7)) power = 7;
                else if (kb.IsKeyDown(Keys.NumPad8) && !oldKB.IsKeyDown(Keys.NumPad8)) power = 8;
                else if (kb.IsKeyDown(Keys.NumPad9) && !oldKB.IsKeyDown(Keys.NumPad9)) power = 9;
            }

            // Selecting Propulsion Energy from number keys above keyboard
            if (propulsionEnergy < charge)
            {
                if (kb.IsKeyDown(Keys.D0) && !oldKB.IsKeyDown(Keys.D0)) propulsionEnergy = 0;
                else if (kb.IsKeyDown(Keys.D1) && !oldKB.IsKeyDown(Keys.D1)) propulsionEnergy = 1;
                else if (kb.IsKeyDown(Keys.D2) && !oldKB.IsKeyDown(Keys.D2)) propulsionEnergy = 2;
                else if (kb.IsKeyDown(Keys.D3) && !oldKB.IsKeyDown(Keys.D3)) propulsionEnergy = 3;
                else if (kb.IsKeyDown(Keys.D4) && !oldKB.IsKeyDown(Keys.D4)) propulsionEnergy = 4;
                else if (kb.IsKeyDown(Keys.D5) && !oldKB.IsKeyDown(Keys.D5)) propulsionEnergy = 5;
                else if (kb.IsKeyDown(Keys.D6) && !oldKB.IsKeyDown(Keys.D6)) propulsionEnergy = 6;
                else if (kb.IsKeyDown(Keys.D7) && !oldKB.IsKeyDown(Keys.D7)) propulsionEnergy = 7;
                else if (kb.IsKeyDown(Keys.D8) && !oldKB.IsKeyDown(Keys.D8)) propulsionEnergy = 8;
                else if (kb.IsKeyDown(Keys.D9) && !oldKB.IsKeyDown(Keys.D9)) propulsionEnergy = 9;
            }

            // Adding to charge every second
            timer2++;
            if (timer2 > 60)
            {
                timer2 = 0;
                charge += 3;
            }
            if (charge > 100) charge = 100;

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
            // Also borg firing
            if (isBorgActive)
            {
                frames1++;
                if (frames1 >= framesBorgIsActive)
                {
                    isBorgActive = false;
                    frames1 = 0;
                }
                fireBorgShot();
            }

            oldMouse = mouse;
            oldKB = kb;
            oldPad = pad;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            spriteBatch.Draw(torpedo, torpedoR, Color.White);
            spriteBatch.Draw(turret, turretR, Color.White);
            spriteBatch.Draw(tube1, tube1R, Color.Black);
            spriteBatch.Draw(tube2, tube2R, Color.Black);
            spriteBatch.Draw(tube3, tube3R, Color.Black);
            spriteBatch.Draw(tube4, tube4R, Color.Black);
            spriteBatch.Draw(outline, outlineR, tubeC);
            spriteBatch.DrawString(font, "" + charge, new Vector2(369, 220), Color.White);
            spriteBatch.Draw(chargeBar, chargeR, Color.Yellow);
            spriteBatch.Draw(propBar, propR, Color.Blue);
            spriteBatch.Draw(explodeBar, explodeR, Color.Red);
            spriteBatch.DrawString(font, "Explosive Power: " + power, new Vector2(GraphicsDevice.Viewport.Width - 380, GraphicsDevice.Viewport.Height - 50), Color.Red);
            spriteBatch.DrawString(font, "Propulsion Energy: " + propulsionEnergy, new Vector2(GraphicsDevice.Viewport.Width - 420, GraphicsDevice.Viewport.Height - 90), Color.Blue);
            spriteBatch.DrawString(font, "Phaser Power: " + phaserPower, new Vector2(GraphicsDevice.Viewport.Width - 340, GraphicsDevice.Viewport.Height - 130), phaserColor);

            spriteBatch.Draw(phaser, phaserR, phaserColor);
            if (isBorgActive)
            {
                spriteBatch.Draw(borg, borgR, Color.White);
                spriteBatch.Draw(bomb, bombR, Color.White);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // Custom Methods
        public void fire(Rectangle torpedo)
        {
            torpedoSound.Play();
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

            startingTorp = torpedoR.X;
        }
        public void spawnBorg()
        {
            Rectangle[] launchTubes = { tube1R, tube2R, tube3R, tube4R };
            randTube = rand.Next(0, launchTubes.Length);

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
                    randYPos = rand.Next(tube4R.Y - 50, tube4R.Y + 50);
                    break;
            }

            isBorgActive = true;
            borgR.X = randXPos;
            borgR.Y = randYPos;
        }
        public void fireBorgShot()
        {
            bombR.X = borgR.X;
            bombR.Y = borgR.Y;
            int centerX = GraphicsDevice.Viewport.Width / 2;
            int centerY = GraphicsDevice.Viewport.Height / 2;
            // Changing X increment
            if (bombR.X > centerX) bombXInc = -2;
            else if (bombR.X < centerX) bombXInc = 2;
            else bombXInc = 0;

            // Changing Y increment
            if (bombR.Y < centerY) bombYInc = -2;
            else if (bombR.Y > centerY) bombYInc = 2;
            else bombYInc = 0;
        }
        public void firePhaser()
        {
            phaserSound.Play();
            phaserR.X = 50;
            phaserR.Y = 200;

            int[] tubes = { 1, 2, 3, 4 };
            int chosen = 0;

            // Finding which tube is selected when phaser is fired
            if (outlineR == tube1R) chosen = 1;
            else if (outlineR == tube2R) chosen = 2;
            else if (outlineR == tube3R) chosen = 3;
            else if (outlineR == tube4R) chosen = 4;

            // Changing phaser properties based on chosen tube
            switch (chosen)
            {
                case 1:
                    phaserR.X = outlineR.X + 25;
                    phaserR.Y = outlineR.Y - 200;
                    phaserR.Width = 25;
                    phaserR.Height = 200;
                    break;
                case 2:
                    phaserR.X = outlineR.X + 100;
                    phaserR.Y = outlineR.Y + 25;
                    phaserR.Width = 500;
                    phaserR.Height = 25;
                    break;
                case 3:
                    phaserR.X = outlineR.X + 25;
                    phaserR.Y = outlineR.Y + 100;
                    phaserR.Width = 25;
                    phaserR.Height = 400;
                    break;
                case 4:
                    phaserR.X = outlineR.X - 300;
                    phaserR.Y = outlineR.Y + 25;
                    phaserR.Width = 300;
                    phaserR.Height = 25;
                    break;
            }

        }
    }
}
