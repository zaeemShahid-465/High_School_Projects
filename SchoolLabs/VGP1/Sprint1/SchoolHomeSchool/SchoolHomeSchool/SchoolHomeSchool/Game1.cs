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

namespace SchoolHomeSchool
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int timer;
        int outlineTimer;

        // Classroom Picture Texture and Rectangle
        Texture2D classroomTexture;
        Rectangle classroomRect;

        // Home picture and Rectangle
        Texture2D homeTexture;
        Rectangle homeRect;

        // School Picture and Rectangle
        Texture2D schoolTexture;
        Rectangle schoolRect;


        // Outline picture and Rectangles
        Texture2D outlineTexture;
        Rectangle p1;
        Rectangle p2;
        Rectangle p3;
        Rectangle tempP;

        Color c1;
        Color c2;
        Color c3;
        Color tempC;


        // Text font and strings
        SpriteFont font;
        String classroomText;
        String homeText;
        String schoolText;
        
        // Text positions
        Vector2 classroomTextPos;
        Vector2 homeTextPos;
        Vector2 schoolTextPos;

        // Rectangle Copies
        Rectangle classroomCopy;
        Rectangle homeCopy;
        Rectangle schoolCopy;

        // Text Position Copies
        Vector2 classroomTextPosCopy;
        Vector2 homeTextPosCopy;
        Vector2 schoolTextPosCopy;


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

            // Pictures
            classroomRect = new Rectangle(5, 40, 237, 200);
            classroomCopy = new Rectangle(5, 40, 237, 200);

            homeRect = new Rectangle(255, 230, 267, 200);
            homeCopy = new Rectangle(255, 230, 267, 200);

            schoolRect = new Rectangle(525, 40, 267, 200);
            schoolCopy = new Rectangle(525, 40, 267, 200);

            // Outlines
            p1 = new Rectangle(0, 35, 250, 210);
            p2 = new Rectangle(249, 225, 280, 210);
            p3 = new Rectangle(518, 35, 280, 210);

            c1 = Color.Red;
            c2 = Color.Blue;
            c3 = Color.Yellow;

            
            // Text
            classroomText = "Classroom\nSlide";
            classroomTextPos = new Vector2(100, 250);
            classroomTextPosCopy = classroomTextPos;

            homeText = "Home\nSlide";
            homeTextPos = new Vector2(400, 430);
            homeTextPosCopy = homeTextPos;

            schoolText = "School\nSlide";
            schoolTextPos = new Vector2(600, 250);
            schoolTextPosCopy = schoolTextPos;

            timer = 0;
            outlineTimer = 0;

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

            // Picture Texture Loading
            classroomTexture = Content.Load<Texture2D>("IMG_1097");
            homeTexture = Content.Load<Texture2D>("home");
            schoolTexture = Content.Load<Texture2D>("School Shoot 2 (1)");

            // Outline Loading
            outlineTexture = Content.Load<Texture2D>("outline");

            // Font Loading
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

            int seconds = timer / 60;

            if (timer == 1260)
                timer = 0;

            // Updating Picture and Text location every 7 seconds
            if (timer == 420)
            {
                // Updating Pics
                classroomRect = homeCopy;
                homeRect = schoolCopy;
                schoolRect = classroomCopy;

                // Updating Text Position
                classroomTextPos = homeTextPosCopy;
                homeTextPos = schoolTextPosCopy;
                schoolTextPos = classroomTextPosCopy;
            }
            else if (timer == 840)
            {
                // Updating pic location
                classroomRect = schoolCopy;
                homeRect = classroomCopy;
                schoolRect = homeCopy;

                // Updating Text location
                classroomTextPos = schoolTextPosCopy;
                homeTextPos = classroomTextPosCopy;
                schoolTextPos = homeTextPosCopy;
            }
            else if (timer == 1260)
            {
                // updating pic location
                classroomRect = classroomCopy;
                homeRect = homeCopy;
                schoolRect = schoolCopy;

                // updating text location
                classroomTextPos = classroomTextPosCopy;
                homeTextPos = homeTextPosCopy;
                schoolTextPos = schoolTextPosCopy;
            }
            else if (timer == 0)
            {
                // resetting everything
                classroomRect = classroomCopy;
                homeRect = homeCopy;
                schoolRect = schoolCopy;

                classroomTextPos = classroomTextPosCopy;
                homeTextPos = homeTextPosCopy;
                schoolTextPos = schoolTextPosCopy;
            }

            // Updating Outline Colors
            outlineTimer++;

            if (outlineTimer == 240)
            {
                tempP = p1;
                p1 = p3;
                p3 = p2;
                p2 = tempP;

                tempC = c1;
                c1 = c2;
                c2 = c3;
                c3 = tempC;
            }
            else if (outlineTimer == 480)
            {
                tempP = p2;
                p2 = p1;
                p1 = p3;
                p3 = tempP;

                tempC = c2;
                c2 = c3;
                c3 = c1;
                c1 = tempC;
            }
            else if (outlineTimer == 720)
            {
                tempP = p3;
                p3 = p2;
                p2 = p1;
                p1 = tempP;

                tempC = c3;
                c3 = c2;
                c2 = c1;
                c1 = tempC;
            }

            if (outlineTimer == 720)
                outlineTimer = 0;


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

            // Drawing Outlines
            spriteBatch.Draw(outlineTexture, p1, Color.Red);
            spriteBatch.Draw(outlineTexture, p2, Color.Blue);
            spriteBatch.Draw(outlineTexture, p3, Color.Yellow);

            // Drawing Pictures
            spriteBatch.Draw(classroomTexture, classroomRect, Color.White);
            spriteBatch.Draw(homeTexture, homeRect, Color.White);
            spriteBatch.Draw(schoolTexture, schoolRect, Color.White);

            // Drawing Text
            spriteBatch.DrawString(font, classroomText, classroomTextPos, c1);
            spriteBatch.DrawString(font, homeText, homeTextPos, c2);
            spriteBatch.DrawString(font, schoolText, schoolTextPos, c3);




            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
