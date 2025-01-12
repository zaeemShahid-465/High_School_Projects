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

namespace RomanPig
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D pig1Texture;
        Rectangle pig1Rect;

        Texture2D pig2Texture;
        Rectangle pig2Rect;

        String s1;
        String translatedS1;

        String s2;
        String translatedS2;

        String s3;
        String translatedS3;

        String vowels;
        String uppercaseVowels;

        SpriteFont font;

        int timer;
        int cycle;

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
            pig1Rect = new Rectangle(50, 270, 250, 250);

            pig2Rect = new Rectangle(350, 290, 300, 150);

            vowels = "aeiou";
            uppercaseVowels = "AEIOU";

            s1 = "Hello";
            translatedS1 = translate(s1);

            s2 = "My name is Piggy";
            translatedS2 = translate(s2);

            s3 = "I like making Video Games";
            translatedS3 = translate(s3);

            timer = 0;
            cycle = 0;

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
            pig1Texture = Content.Load<Texture2D>("pig1");
            pig2Texture = Content.Load<Texture2D>("pig2");

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


            if (timer < 300)
            {
                cycle = 1;
            }
            else if (timer < 600)
            {
                cycle = 2;
            }
            else if (timer < 900)
            {
                cycle = 3;
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

            spriteBatch.Draw(pig1Texture, pig1Rect, Color.White);
            spriteBatch.Draw(pig2Texture, pig2Rect, Color.White);

            if (cycle == 1)
            {
                spriteBatch.DrawString(font, s1, new Vector2(100, 250), Color.Black);
                spriteBatch.DrawString(font, translatedS1, new Vector2(400, 250), Color.Black);
            }
            else if (cycle == 2)
            {
                spriteBatch.DrawString(font, s2, new Vector2(100, 250), Color.Black);
                spriteBatch.DrawString(font, translatedS2, new Vector2(400, 250), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(font, s3, new Vector2(100, 250), Color.Black);
                spriteBatch.DrawString(font, translatedS3, new Vector2(400, 250), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public String translate(String s)
        {
            String tempS;

            tempS = s;

            tempS.ToLower();

            String[] tempArray = s.Split(' ');

            String finalS = "";

            // Iterating through each element in the tempArray
            for (int i = 0; i < tempArray.Length; i++)
            {
                // Checking if the first character is a vowel
                if (vowels.Contains(tempArray[i].Substring(0, 1)) || uppercaseVowels.Contains(tempArray[i].Substring(0, 1)))
                {
                    // adding "way" to the end of the string
                    tempArray[i] += "way";
                }
                else // First Character is a Consonant
                {
                    // Checking word length to avoid index Out of Range exception
                    if (tempArray[i].Length > 2)
                    {
                        // Checking if the second character is a vowel
                        if (vowels.Contains(tempArray[i].Substring(1, 2)) || uppercaseVowels.Contains(tempArray[i].Substring(1, 2)))
                        {
                            // Moving the first character to the end of the word
                            tempArray[i] += tempArray[i].Substring(0, 2);
                            tempArray[i] += "ay";

                            tempArray[i] = tempArray[i].Remove(0, 2);
                        }
                        // second letter is a consonant
                        else
                        {
                            tempArray[i] += tempArray[i].Substring(0, 1);
                            tempArray[i] += "ay";
                            tempArray[i] = tempArray[i].Remove(0, 1);
                        }
                    }
                    else
                    {
                        // Checking if second character is a vowel
                        if (vowels.Contains(tempArray[i].Substring(1)) || uppercaseVowels.Contains(tempArray[i].Substring(1)))
                        {
                            tempArray[i] += tempArray[i].Substring(0, 1);
                            tempArray[i] += "ay";
                            tempArray[i] = tempArray[i].Remove(0, 1); 
                        }
                        else
                        {
                            tempArray[i] += tempArray[i].Substring(0, 2);
                            tempArray[i] += "ay";
                            tempArray[i] = tempArray[i].Remove(0, 2); 
                        }
                    }
                    
                }
            }


            for (int i = 0; i < tempArray.Length; i++)
            {
                finalS += tempArray[i] + " ";
            }

            return finalS;

            


                
        }
    }
}
