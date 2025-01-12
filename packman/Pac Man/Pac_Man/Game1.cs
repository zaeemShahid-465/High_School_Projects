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
using System.Collections;

namespace Pac_Man
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Screen Width/Height
        int screenW, screenH;

        // Previous KeyboardState
        KeyboardState oldKb;

        // Player Object
        Player player;

        // Ghosts
        Texture2D[] ghostTextures;
        Rectangle[] ghostRecs;
        Ghost ghosts;

        // Coins
        ArrayList coins = new ArrayList();

        // TileMap
        Texture2D tileText;
        Rectangle[] tileRects;

        // UI
        PlayerInfo playerInfo;

        // Game State Manager
        int currentState = 0;

        // Fonts
        SpriteFont font;
        SpriteFont biggerFont;

        // timer and flag for handling the game when the player gets hit
        int timer;
        bool hit;

        // timer and flag for handling the countdown before the game starts
        int readyTimer;
        bool ready;

        // Game Won Flag
        bool won;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 675;
            graphics.ApplyChanges();
            this.Window.AllowUserResizing = true;
            IsMouseVisible = true;
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

            ready = true;
            readyTimer = 360;

            oldKb = Keyboard.GetState();

            timer = 0;
            hit = false;

            currentState = 0;

            won = false;

            // Player object
            player = new Player(Content, screenW, screenH);

            // Tilemap Rectangles
            tileRects = new Rectangle[27];
            tileRects[0] = new Rectangle(10, 10, 980, 10);
            tileRects[1] = new Rectangle(10, 580, 980, 10);
            tileRects[2] = new Rectangle(10, 10, 10, 580);
            tileRects[3] = new Rectangle(980, 10, 10, 580);
            tileRects[4] = new Rectangle(360, 125, 280, 10);
            tileRects[5] = new Rectangle(360, 465, 280, 10);
            tileRects[6] = new Rectangle(360, 355, 280, 10);
            tileRects[7] = new Rectangle(260, 20, 10, 115);
            tileRects[8] = new Rectangle(730, 20, 10, 115);
            tileRects[9] = new Rectangle(260, 465, 10, 115);
            tileRects[10] = new Rectangle(730, 465, 10, 115);
            tileRects[11] = new Rectangle(880, 355, 10, 125);
            tileRects[12] = new Rectangle(880, 120, 10, 125);
            tileRects[13] = new Rectangle(110, 355, 10, 125);
            tileRects[14] = new Rectangle(110, 120, 10, 125);
            tileRects[15] = new Rectangle(360, 240, 10, 125);
            tileRects[16] = new Rectangle(630, 240, 10, 125);
            tileRects[17] = new Rectangle(570, 240, 60, 10);
            tileRects[18] = new Rectangle(370, 240, 60, 10);
            tileRects[19] = new Rectangle(210, 240, 60, 10);
            tileRects[20] = new Rectangle(730, 240, 60, 10);
            tileRects[21] = new Rectangle(210, 350, 60, 10);
            tileRects[22] = new Rectangle(730, 350, 60, 10);
            tileRects[23] = new Rectangle(120, 120, 60, 10);
            tileRects[24] = new Rectangle(820, 120, 60, 10);
            tileRects[25] = new Rectangle(120, 470, 60, 10);
            tileRects[26] = new Rectangle(820, 470, 60, 10);

            // Fill up Coins Array
            createCoinArray();

/*            Console.WriteLine(coins.Count);*/

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

            // Tile Texture
            tileText = Content.Load<Texture2D>("(3.10.2)White Square");

            // Load ghosts with different textures and assign starting positions
            Texture2D blinkyTexture = Content.Load<Texture2D>("blinky");
            Texture2D pinkyTexture = Content.Load<Texture2D>("pinky");
            Texture2D inkyTexture = Content.Load<Texture2D>("inky");
            Texture2D clydeTexture = Content.Load<Texture2D>("clyde");

            // Initialize ghost instance
            ghostTextures = new Texture2D[4] { blinkyTexture, pinkyTexture, inkyTexture, clydeTexture };
            ghosts = new Ghost(ghostTextures, screenW, screenH);

            // UI initialization
            Texture2D livesT = this.Content.Load<Texture2D>("pac man life counter");
            playerInfo = new PlayerInfo(livesT, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // Loading fonts
            font = Content.Load<SpriteFont>("SpriteFont1");
            biggerFont = this.Content.Load<SpriteFont>("SpriteFont2");

            // Getting all the ghost rectangles for player collisions
            ghostRecs = ghosts.getRecs();

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

            // Checking if dots are all collected, then changing game state to won
            if (coins.Count <= 0)
            {
                won = true;
            }

            // If the player hits R, the game Resets
            if (currentState == 2)
            {
                if (kb.IsKeyDown(Keys.R) && !oldKb.IsKeyDown(Keys.R))
                {
                    currentState = 0;
                    destroyAllCoins();
                    createCoinArray();
                    player.rec.X = (screenW / 2) - 60;
                    player.rec.Y = ((screenH / 2) - 60) + 110;
                    playerInfo.SetScore(0);
                    ghosts = new Ghost(ghostTextures, screenW, screenH);
                    ghostRecs = ghosts.getRecs();
                    hit = false;
                    timer = 0;
                    ready = true;
                    readyTimer = 360;
                    playerInfo.lives = 3;
/*                    playerInfo.AddLives(3);*/
                }
            }

            // Ready Countdown when the game starts
            if (ready)
            {
                readyTimer--;
                if (readyTimer <= 0)
                    ready = false;
            }

            else if (!won)
            {
                // updating player and ghosts
                if (!hit)
                {
                    player.move(tileRects);
                    player.animate();
                    ghosts.Update(gameTime, tileRects, new Vector2(player.getRectangle().X, player.getRectangle().Y));
                }

                // Updating Coins
                runCoinArray();

                // Checking Collision with Ghosts
                for (int i = 0; i < ghosts.getRecs().Length; i++)
                {
                    if (new Rectangle(player.getRectangle().X + 5, player.getRectangle().Y + 5, 40, 40).Intersects(ghostRecs[i]))
                    {
                        hit = true;
                        handleHit();
                    }
                }

                // Delay for when pac man is hit
                if (hit)
                    timer++;

                // Changing Game State when Lives is at 0
                if (playerInfo.lives <= 0)
                    currentState = 2;
            }


            oldKb = kb;
            base.Update(gameTime);
        }

        public void handleHit()
        {
            // Resetting ghosts and player position, removing a life
            if (timer > 180)
            {
                player.rec.X = (screenW / 2) - 60;
                player.rec.Y = ((screenH / 2) - 60) + 110;
                playerInfo.AddLives(-1);
                ghosts = new Ghost(ghostTextures, screenW, screenH);
                ghostRecs = ghosts.getRecs();
                hit = false;
                timer = 0;
                ready = true;
                readyTimer = 360;
            }
        }

        // Creates the array of coins with positions in map
        public void createCoinArray()
        {
            for (int i = 0; i < 500; i += 25)
            {
                int x = 64;
                coins.Add(new Coin(new Vector2(60, x + i), Content));

            }
            for (int i = 0; i < 500; i += 25)
            {
                int x = 64;
                coins.Add(new Coin(new Vector2(935, x + i), Content));

            }
            for (int i = 0; i < 175; i += 25)
            {
                int x = 60;
                coins.Add(new Coin(new Vector2(x + i, 64), Content));

            }
            for (int i = 0; i < 175; i += 25)
            {
                int x = 60;
                coins.Add(new Coin(new Vector2(x + i, 540), Content));

            }

            for (int i = 0; i < 175; i += 25)
            {
                int x = 760;
                coins.Add(new Coin(new Vector2(x + i, 64), Content));

            }
            for (int i = 0; i < 175; i += 25)
            {
                int x = 760;
                coins.Add(new Coin(new Vector2(x + i, 540), Content));

            }

            for (int i = 0; i < 400; i += 25)
            {
                int x = 305;
                coins.Add(new Coin(new Vector2(x + i, 64), Content));

            }
            for (int i = 0; i < 400; i += 25)
            {
                int x = 305;
                coins.Add(new Coin(new Vector2(x + i, 540), Content));

            }

            for (int i = 0; i < 725; i += 25)
            {
                int x = 150;
                coins.Add(new Coin(new Vector2(x + i, 180), Content));

            }
            for (int i = 0; i < 725; i += 25)
            {
                int x = 150;
                coins.Add(new Coin(new Vector2(x + i, 415), Content));

            }
            for (int i = 0; i < 275; i += 25)
            {
                int x = 90;
                coins.Add(new Coin(new Vector2(x + i, 295), Content));

            }
            for (int i = 0; i < 275; i += 25)
            {
                int x = 650;
                coins.Add(new Coin(new Vector2(x + i, 295), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 95;
                coins.Add(new Coin(new Vector2(210, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 95;
                coins.Add(new Coin(new Vector2(310, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 95;
                coins.Add(new Coin(new Vector2(675, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 95;
                coins.Add(new Coin(new Vector2(775, x + i), Content));

            }
            for (int i = 0; i < 275; i += 25)
            {
                int x = 650;
                coins.Add(new Coin(new Vector2(x + i, 295), Content));

            }
            // --------
            for (int i = 0; i < 75; i += 25)
            {
                int x = 455;
                coins.Add(new Coin(new Vector2(210, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 455;
                coins.Add(new Coin(new Vector2(310, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 455;
                coins.Add(new Coin(new Vector2(675, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 455;
                coins.Add(new Coin(new Vector2(775, x + i), Content));

            }
            //----
            for (int i = 0; i < 75; i += 25)
            {
                int x = 210;
                coins.Add(new Coin(new Vector2(310, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 330;
                coins.Add(new Coin(new Vector2(310, x + i), Content));

            }

            for (int i = 0; i < 75; i += 25)
            {
                int x = 210;
                coins.Add(new Coin(new Vector2(150, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 330;
                coins.Add(new Coin(new Vector2(150, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 210;
                coins.Add(new Coin(new Vector2(675, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 330;
                coins.Add(new Coin(new Vector2(675, x + i), Content));
            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 210;
                coins.Add(new Coin(new Vector2(850, x + i), Content));

            }
            for (int i = 0; i < 75; i += 25)
            {
                int x = 330;
                coins.Add(new Coin(new Vector2(850, x + i), Content));

            }

        }

        // Draws all coins
        public void drawCoins()
        {
            foreach (Coin current in coins)
            {
                spriteBatch.Draw(current.getTexture(), current.getRectangle(), Color.Yellow);
            }
        }

        // destroys coins and adds score
        public void DestroyCoin(int target)
        {
            coins.RemoveAt(target);
        }

        // run the arrays methods
        public void runCoinArray()
        {
            for (int i = 0; i < coins.Count; i++)
            {
                Coin curret = (Coin)coins[i];
                if (curret.coinCollision(player))
                {
                    DestroyCoin(i);
                    int score = (239 - coins.Count) * 10;
                    /*Console.WriteLine($"Coins remaining: {coins.Count}, Score: {score}");*/
                    playerInfo.SetScore(score);

                }
            }
        }

        public void destroyAllCoins()
        {
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                coins.RemoveAt(i);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            int seconds = readyTimer / 60;

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (currentState == 0)
            {
                // Checking if game won
                if (won)
                {
                    spriteBatch.DrawString(biggerFont, "YOU WON!", new Vector2(420, 190), Color.White);
                }

                // Drawing Ready Countdown
                if (ready)
                    spriteBatch.DrawString(biggerFont, "" + seconds, new Vector2(500, 220), Color.White);
                else if (!won)
                    spriteBatch.Draw(this.Content.Load<Texture2D>("0"), new Rectangle(400, 220, 500, 200), Color.Black);

                // Drawing map
                for (int i = 0; i < tileRects.Length; i++)
                {
                    spriteBatch.Draw(tileText, tileRects[i], Color.Blue);
                }

                // Drawing Coins
                drawCoins();

                // Drawing player
                player.draw(spriteBatch);

                // Drawing Ghosts
                ghosts.Draw(spriteBatch);

                // Drawing UI
                playerInfo.DisplayInfo(spriteBatch, font);
            }

            else if (currentState == 2)
            {
                spriteBatch.DrawString(font, "    Game Over!\nPress R to Restart", new Vector2(screenW / 2 - 80, screenH / 2), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}


