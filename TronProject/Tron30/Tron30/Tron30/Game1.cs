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

namespace Tron30
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Random rand;

        // Config
        Rectangle window;
        int screenW;
        int screenH;
        SpriteFont font;
        KeyboardState oldKb;
        
        // Countdown for when game starts
        int startingTimer;
        bool countdownComplete;

        // Bikes movement
        int speed;
        Vector2 p1Direction;
        Vector2 p2Direction;
        enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }

        Directions p1CurrentDirection;
        Directions p2CurrentDirection;

        // Game state management
        enum states
        {
            Menu,
            Play,
            GameOver
        }
        states currentState;

        // Images and Rectangles for bikes
        Texture2D[] bikes;
        Texture2D p1Tex, p2Tex;
        Rectangle p1, p2;

        // Trail Rectangle arrays for Bikes
        List<Rectangle> p1Trail = new List<Rectangle>();
        List<Rectangle> p2Trail = new List<Rectangle>();

        int trailWidth = 10;
        int trailHeight = 10;

        bool initalizedLocation;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
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
            screenW = GraphicsDevice.Viewport.Width;
            screenH = GraphicsDevice.Viewport.Height;
            window = new Rectangle(0, 0, screenW, screenH);

            rand = new Random(Guid.NewGuid().GetHashCode());

            oldKb = Keyboard.GetState();

            currentState = states.Menu;

            p1 = new Rectangle(0, screenH / 2, 76, 42);
            p2 = new Rectangle(window.Right - p1.Width, screenH / 2, 76, 42);


            startingTimer = 240;
            countdownComplete = false;

            initalizedLocation = false;

            speed = 3;


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
            font = this.Content.Load<SpriteFont>("SpriteFont1");

            bikes = new Texture2D[4];

            bikes[0] = this.Content.Load<Texture2D>("BikeU");
            bikes[1] = this.Content.Load<Texture2D>("BikeR");
            bikes[2] = this.Content.Load<Texture2D>("BikeD");
            bikes[3] = this.Content.Load<Texture2D>("BikeL");

            p1Tex = bikes[1];
            p2Tex = bikes[3];
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
            
            // Changing dimensions of Rectangle to match texture dimensions if texture changes
            if (p1Tex == bikes[0] || p1Tex == bikes[2])
            {
                p1.Width = 42;
                p1.Height = 76;
            }
            else if (p1Tex == bikes[1] || p1Tex == bikes[3])
            {
                p1.Width = 76;
                p1.Height = 42;
            }

            if (p2Tex == bikes[0] || p2Tex == bikes[2])
            {
                p2.Width = 42;
                p2.Height = 76;
            }
            else if (p2Tex == bikes[1] || p2Tex == bikes[3])
            {
                p2.Width = 76;
                p2.Height = 42;
            }

            // Main Menu
            if (currentState == states.Menu)
            {
                if (kb.IsKeyDown(Keys.Space))
                    currentState = states.Play;
            }

            // Game State
            if (currentState == states.Play)
            {
                if (startingTimer < -10)
                    countdownComplete = true;
                
                // Initializing random spawn location when game starts
                if (!initalizedLocation)
                {
                    int side = rand.Next(1, 3);
                    // 1 for Horizontal
                    // 2 for vertical
                    if (side == 1)
                    {
                        p1.X = 10;
                        p2.X = (window.Right - p1.Width - 10);
                        p1.Y = rand.Next(10, screenH - bikes[0].Width - 10);
                        p2.Y = p1.Y;
                        p1Tex = bikes[1];
                        p2Tex = bikes[3];
                        p1Direction = new Vector2(speed, 0);
                        p2Direction = new Vector2(-speed, 0);
                        p1CurrentDirection = Directions.Right;
                        p2CurrentDirection = Directions.Left;

                    }
                    else
                    {
                        p1.X = rand.Next(10, screenW - bikes[0].Height - 10);
                        p2.X = p1.X;
                        p1.Y = 10;
                        p2.Y = screenH - 76 - 10;
                        p1Tex = bikes[2];
                        p2Tex = bikes[0];
                        p1Direction = new Vector2(0, speed);
                        p2Direction = new Vector2(0, -speed);
                        p1CurrentDirection = Directions.Down;
                        p2CurrentDirection = Directions.Up;
                    }

                    initalizedLocation = true;
                }

                // Movement of Bikes
                if (countdownComplete)
                {
                    p1.X += (int)p1Direction.X;
                    p1.Y += (int)p1Direction.Y;
                    p2.X += (int)p2Direction.X;
                    p2.Y += (int)p2Direction.Y;

                    // Leaving trail behind bikes
                    switch (p1CurrentDirection)
                    {
                        case Directions.Up:
                            p1Trail.Add(new Rectangle(p1.X + 17, p1.Y - trailHeight + 28, trailWidth, trailHeight));
                            break;
                        case Directions.Right:
                            p1Trail.Add(new Rectangle(p1.X + trailWidth + 3, p1.Y + 15, trailHeight, trailWidth));
                            break;
                        case Directions.Down:
                            p1Trail.Add(new Rectangle(p1.X + 17, p1.Y + trailHeight + 2, trailWidth, trailHeight));
                            break;
                        case Directions.Left:
                            p1Trail.Add(new Rectangle(p1.X + trailWidth + 11, p1.Y + 17, trailHeight, trailWidth));
                            break;
                    }

                    switch (p2CurrentDirection)
                    {
                        case Directions.Up:
                            p2Trail.Add(new Rectangle(p2.X + 17, p2.Y - trailHeight + 28, trailWidth, trailHeight));
                            break;
                        case Directions.Right:
                            p2Trail.Add(new Rectangle(p2.X + trailWidth + 3, p2.Y + 15, trailHeight, trailWidth));
                            break;
                        case Directions.Down:
                            p2Trail.Add(new Rectangle(p2.X + 17, p2.Y + trailHeight + 2, trailWidth, trailHeight));
                            break;
                        case Directions.Left:
                            p2Trail.Add(new Rectangle(p2.X + trailWidth + 11, p2.Y + 17, trailHeight, trailWidth));
                            break;            
                    }

                    // Keyboard input to change direction

                    /// Player 1 Movement
                    if (kb.IsKeyDown(Keys.W) && !oldKb.IsKeyDown(Keys.W) && p1CurrentDirection != Directions.Down)
                    {
                        p1Tex = bikes[0];
                        p1Direction.Y = -speed;
                        p1Direction.X = 0;
                        p1CurrentDirection = Directions.Up;
                    }

                    if (kb.IsKeyDown(Keys.D) && !oldKb.IsKeyDown(Keys.D) && p1CurrentDirection != Directions.Left)
                    {
                        p1Tex = bikes[1];
                        p1Direction.X = speed;
                        p1Direction.Y = 0;
                        p1CurrentDirection = Directions.Right;
                    }

                    if (kb.IsKeyDown(Keys.S) && !oldKb.IsKeyDown(Keys.S) && p1CurrentDirection != Directions.Up)
                    {
                        p1Tex = bikes[2];
                        p1Direction.Y = speed;
                        p1Direction.X = 0;
                        p1CurrentDirection = Directions.Down;
                    }

                    if (kb.IsKeyDown(Keys.A) && !oldKb.IsKeyDown(Keys.A) && p1CurrentDirection != Directions.Right)
                    {
                        p1Tex = bikes[3];
                        p1Direction.X = -speed;
                        p1Direction.Y = 0;
                        p1CurrentDirection = Directions.Left;
                    }

                    /// Player 2 Movement
                    if (kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up) && p2CurrentDirection != Directions.Down)
                    {
                        p2Tex = bikes[0];
                        p2Direction.Y = -speed;
                        p2Direction.X = 0;
                        p2CurrentDirection = Directions.Up;
                    }

                    if (kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right) && p2CurrentDirection != Directions.Left)
                    {
                        p2Tex = bikes[1];
                        p2Direction.X = speed;
                        p2Direction.Y = 0;
                        p2CurrentDirection = Directions.Right;
                    }

                    if (kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down) && p2CurrentDirection != Directions.Up)
                    {
                        p2Tex = bikes[2];
                        p2Direction.Y = speed;
                        p2Direction.X = 0;
                        p2CurrentDirection = Directions.Down;
                    }

                    if (kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left) && p2CurrentDirection != Directions.Right)
                    {
                        p2Tex = bikes[3];
                        p2Direction.X = -speed;
                        p2Direction.Y = 0;
                        p2CurrentDirection = Directions.Left;
                    }
                }

                // Collisions with sides of game window
                if (p1.X + p1.Width >= window.Right || p1.X <= window.Left || p1.Y <= window.Top || p1.Y + p1.Height >= window.Bottom)
                {
                    currentState = states.GameOver;
                }
                else if (p2.X + p2.Height >= window.Right || p2.X <= window.Left || p2.Y <= window.Top || p2.Y + p2.Height >= window.Bottom)
                {
                    currentState = states.GameOver;
                }

                // Collisions with other bike
                if (p1.Intersects(p2))
                    currentState = states.GameOver;

                // Both Bikes collisions with trails
                for (int i = p1Trail.Count - 30; i >= 0; i--)
                {
                    if (p1.Intersects(p1Trail[i]) || p2.Intersects(p1Trail[i]))
                    {
                        currentState = states.GameOver;
                    }
                }

                for (int i = p2Trail.Count - 30; i >= 0; i--)
                {
                    if (p2.Intersects(p2Trail[i]) || p1.Intersects(p2Trail[i]))
                    {
                        currentState = states.GameOver;
                    }
                }

                startingTimer--;
            }

            if (currentState == states.GameOver)
            {
                if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
                {
                    // removing everything from bike trail
                    p1Trail.Clear();
                    p2Trail.Clear();
                    
                    countdownComplete = false;
                    initalizedLocation = false;
                    startingTimer = 240;
                    currentState = states.Play;

                }
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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (currentState == states.Menu)
            {
                spriteBatch.DrawString(font, "Welcome To Tron!", new Vector2(window.Center.X - 100, window.Center.Y - 100), Color.White);
                spriteBatch.DrawString(font, "Press Space To Start", new Vector2(window.Center.X - 120, window.Center.Y), Color.White);
            }

            if (currentState == states.Play || currentState == states.GameOver)
            {
                // Drawing countdown
                if (!(countdownComplete))
                    spriteBatch.DrawString(font, (startingTimer / 60) + "", new Vector2(screenW / 2, screenH / 2), Color.White);
                
                // Drawing Trails
                foreach (Rectangle trail in p1Trail)
                {
                    spriteBatch.Draw(this.Content.Load<Texture2D>("temp"), trail, Color.Blue);
                }

                foreach (Rectangle trail in p2Trail)
                {
                    spriteBatch.Draw(this.Content.Load<Texture2D>("temp"), trail, Color.Red);
                }

                // Drawing bikes
                spriteBatch.Draw(p1Tex, p1, Color.Blue);
                spriteBatch.Draw(p2Tex, p2, Color.Red);

                // Drawing Game Over Screen
                if (currentState == states.GameOver)
                {
                    spriteBatch.DrawString(font, "You Crashed! Game Over", new Vector2(window.Center.X - 120, window.Center.Y), Color.White);
                    spriteBatch.DrawString(font, "Press Space To Restart", new Vector2(window.Center.X - 120, window.Center.Y + 100), Color.White);
                }


            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
