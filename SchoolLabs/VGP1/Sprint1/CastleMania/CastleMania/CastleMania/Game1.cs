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

namespace CastleMania
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int timer;
        int timer2;
        int timer3;
        int timer4;
        int timer5;
        int eclipseTimer;

        // BG Rect and Texture
        Texture2D bg;
        Rectangle bgRect;

        // Security Guard Rect and Texture
        Texture2D guard1;
        Rectangle guard1Rect;

        // Archer, arrow, and target Textures and rectangles
        Texture2D archer;
        Rectangle archerRect;

        Texture2D arrow;
        Rectangle arrowRect;

        Texture2D target;
        Rectangle targetRect;

        // Dragon Rectangle and Texture
        Texture2D dragon;
        Rectangle dragonRect;

        // Ghost Rectangle and Texture
        Texture2D ghost;
        Rectangle ghostRect;

        // Princess Texture and Rectangle
        Texture2D princess;
        Rectangle princessRect;

        // Herald Texture and Rectangle
        Texture2D herald;
        Rectangle heraldRect;

        // Font
        SpriteFont font;
        String heraldDialogue;


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
            timer2 = 0;
            timer3 = 0;
            timer4 = 0;
            timer5 = 0;
            eclipseTimer = 0;

            // BG Rectangle
            bgRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // Guard 1 Rectangle
            guard1Rect = new Rectangle(0, 370, 100, 100);

            // Archer, arrow, target rectangles
            archerRect = new Rectangle(400, 370, 150, 90);
            arrowRect = new Rectangle(480, 370, 75, 75);
            targetRect = new Rectangle(620, 370, 130, 110);

            // Dragon Rectangle
            dragonRect = new Rectangle(GraphicsDevice.Viewport.Width, 50, 200, 150);

            // Ghost Rectangle
            ghostRect = new Rectangle(0, 200, 100, 100);

            // Princess Rectangle
            princessRect = new Rectangle(200, 350, 100, 100);

            // Herald Rectangle
            heraldRect = new Rectangle(250, 250, 100, 100);

            // Herald Text
            heraldDialogue = "The solar eclipse is about to occur!!";

     

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

            // bg Texture Loading
            bg = Content.Load<Texture2D>("castlebg");

            // guard 1 texture loading
            guard1 = Content.Load<Texture2D>("guard1-removebg-preview");

            // archer, arrow, target texture loading
            archer = Content.Load<Texture2D>("archer-removebg-preview");
            target = Content.Load<Texture2D>("target-removebg-preview");
            arrow = Content.Load<Texture2D>("arrow-removebg-preview");

            // Dragon loading
            dragon = Content.Load<Texture2D>("dragon-removebg-preview");

            // Ghost Loading
            ghost = Content.Load<Texture2D>("ghost-removebg-preview");

            // Princess Texture Loading
            princess = Content.Load<Texture2D>("princess-removebg-preview");

            // Herald Texture Loading
            herald = Content.Load<Texture2D>("herald-removebg-preview");

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            timer++;
            timer2++;
            timer3++;
            timer4++;
            timer5++;
            eclipseTimer++;

            // Guard 1 Movement
            if (timer < 300)
            {
                if (timer2 % 2 == 0)
                    guard1Rect.X += 1;
            }

            else if (timer < 600)
            {
                if (timer2 % 2 == 0)
                    guard1Rect.X -= 1;
            }



            // Archer shooting arrows
            if (timer3 < 60)
            {
                arrowRect.X += 3;
            }

            else
            {
                arrowRect.X = 480;
            }

            if (timer3 > 120)
                timer3 = 0;

            // Dragon Flying 
            if (timer4 < 1000)
            {
                dragonRect.X -= 2;
            }
            
            if (timer4 > 1000)
            {
                dragonRect.X = GraphicsDevice.Viewport.Width;
                timer4 = 0;
            }

            // Ghost appearing in and out of screen
            if (timer < 600)
            {

                if (ghostRect.X < GraphicsDevice.Viewport.Width + 50)
                    ghostRect.X += 2;
                else
                    ghostRect.X = -50;
            }

            // Princess Walking

            if (timer < 180)
            {
                if (timer2 % 2 == 0)
                    princessRect.Y -= 1;
            }
            else if (timer < 360)
            {
                if (timer2 % 2 == 0)
                    princessRect.Y += 1;
            }

            if (timer > 600)
                timer = 0;

            // Herald walking
            
            if (timer5 < 190)
            {
                if (timer2 % 2 == 0)
                    heraldRect.X += 2;
            }
            else if (timer5 > 190)
            {
                if (timer2 % 2 == 0)
                    heraldRect.X -= 2;
            }
            

            

            if (timer5 > 380)
                timer5 = 0;




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


            // Solar Eclipse


            if ((eclipseTimer / 60) >= 23)
            {
                spriteBatch.Draw(bg, bgRect, Color.White);
                spriteBatch.Draw(guard1, guard1Rect, Color.White);
                spriteBatch.Draw(archer, archerRect, Color.White);
                spriteBatch.Draw(target, targetRect, Color.White);
                spriteBatch.Draw(arrow, arrowRect, Color.White);
                spriteBatch.Draw(dragon, dragonRect, Color.White);
                spriteBatch.Draw(ghost, ghostRect, Color.White);
                spriteBatch.Draw(princess, princessRect, Color.White);
                spriteBatch.Draw(herald, heraldRect, Color.White);
            }

            else if ((eclipseTimer / 60) >= 22)
            {
                spriteBatch.Draw(bg, bgRect, Color.LightGray);
                spriteBatch.Draw(guard1, guard1Rect, Color.LightGray);
                spriteBatch.Draw(archer, archerRect, Color.LightGray);
                spriteBatch.Draw(target, targetRect, Color.LightGray);
                spriteBatch.Draw(arrow, arrowRect, Color.LightGray);
                spriteBatch.Draw(dragon, dragonRect, Color.LightGray);
                spriteBatch.Draw(ghost, ghostRect, Color.LightGray);
                spriteBatch.Draw(princess, princessRect, Color.LightGray);
                spriteBatch.Draw(herald, heraldRect, Color.LightGray);
            }

            else if((eclipseTimer / 60) >= 21)
            {
                spriteBatch.Draw(bg, bgRect, Color.Gray);
                spriteBatch.Draw(guard1, guard1Rect, Color.Gray);
                spriteBatch.Draw(archer, archerRect, Color.Gray);
                spriteBatch.Draw(target, targetRect, Color.Gray);
                spriteBatch.Draw(arrow, arrowRect, Color.Gray);
                spriteBatch.Draw(dragon, dragonRect, Color.Gray);
                spriteBatch.Draw(ghost, ghostRect, Color.Gray);
                spriteBatch.Draw(princess, princessRect, Color.Gray);
                spriteBatch.Draw(herald, heraldRect, Color.Gray);
            }

            else if ((eclipseTimer / 60) >= 20)
            {
                spriteBatch.Draw(bg, bgRect, Color.DarkGray);
                spriteBatch.Draw(guard1, guard1Rect, Color.DarkGray);
                spriteBatch.Draw(archer, archerRect, Color.DarkGray);
                spriteBatch.Draw(target, targetRect, Color.DarkGray);
                spriteBatch.Draw(arrow, arrowRect, Color.DarkGray);
                spriteBatch.Draw(dragon, dragonRect, Color.DarkGray);
                spriteBatch.Draw(ghost, ghostRect, Color.DarkGray);
                spriteBatch.Draw(princess, princessRect, Color.DarkGray);
                spriteBatch.Draw(herald, heraldRect, Color.DarkGray);
            }

            else if ((eclipseTimer / 60) >= 15)
            {
                spriteBatch.Draw(bg, bgRect, Color.Black);
                spriteBatch.Draw(guard1, guard1Rect, Color.Black);
                spriteBatch.Draw(archer, archerRect, Color.Black);
                spriteBatch.Draw(target, targetRect, Color.Black);
                spriteBatch.Draw(arrow, arrowRect, Color.Black);
                spriteBatch.Draw(dragon, dragonRect, Color.Black);
                spriteBatch.Draw(ghost, ghostRect, Color.Black);
                spriteBatch.Draw(princess, princessRect, Color.Black);
                spriteBatch.Draw(herald, heraldRect, Color.Black);
            }

            else if ((eclipseTimer / 60) >= 14)
            {
                spriteBatch.Draw(bg, bgRect, Color.DarkGray);
                spriteBatch.Draw(guard1, guard1Rect, Color.DarkGray);
                spriteBatch.Draw(archer, archerRect, Color.DarkGray);
                spriteBatch.Draw(target, targetRect, Color.DarkGray);
                spriteBatch.Draw(arrow, arrowRect, Color.DarkGray);
                spriteBatch.Draw(dragon, dragonRect, Color.DarkGray);
                spriteBatch.Draw(ghost, ghostRect, Color.DarkGray);
                spriteBatch.Draw(princess, princessRect, Color.DarkGray);
                spriteBatch.Draw(herald, heraldRect, Color.DarkGray);
            }
                
            else if ((eclipseTimer / 60) >= 13)
            {
                spriteBatch.Draw(bg, bgRect, Color.Gray);
                spriteBatch.Draw(guard1, guard1Rect, Color.Gray);
                spriteBatch.Draw(archer, archerRect, Color.Gray);
                spriteBatch.Draw(target, targetRect, Color.Gray);
                spriteBatch.Draw(arrow, arrowRect, Color.Gray);
                spriteBatch.Draw(dragon, dragonRect, Color.Gray);
                spriteBatch.Draw(ghost, ghostRect, Color.Gray);
                spriteBatch.Draw(princess, princessRect, Color.Gray);
                spriteBatch.Draw(herald, heraldRect, Color.Gray);
            }
                
            else if ((eclipseTimer / 60) >= 12)
            {
                spriteBatch.Draw(bg, bgRect, Color.LightGray);
                spriteBatch.Draw(guard1, guard1Rect, Color.LightGray);
                spriteBatch.Draw(archer, archerRect, Color.LightGray);
                spriteBatch.Draw(target, targetRect, Color.LightGray);
                spriteBatch.Draw(arrow, arrowRect, Color.LightGray);
                spriteBatch.Draw(dragon, dragonRect, Color.LightGray);
                spriteBatch.Draw(ghost, ghostRect, Color.LightGray);
                spriteBatch.Draw(princess, princessRect, Color.LightGray);
                spriteBatch.Draw(herald, heraldRect, Color.LightGray);
            }
                
            else
            {
                spriteBatch.Draw(bg, bgRect, Color.White);
                spriteBatch.Draw(guard1, guard1Rect, Color.White);
                spriteBatch.Draw(archer, archerRect, Color.White);
                spriteBatch.Draw(target, targetRect, Color.White);
                spriteBatch.Draw(arrow, arrowRect, Color.White);
                spriteBatch.Draw(dragon, dragonRect, Color.White);
                spriteBatch.Draw(ghost, ghostRect, Color.White);
                spriteBatch.Draw(princess, princessRect, Color.White);
                spriteBatch.Draw(herald, heraldRect, Color.White);
            }

            if ((eclipseTimer / 60) >= 7 && (eclipseTimer) / 60 <= 24)
            {
                spriteBatch.DrawString(font, heraldDialogue, new Vector2(heraldRect.X + 20, heraldRect.Y - 20), Color.Black);

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
