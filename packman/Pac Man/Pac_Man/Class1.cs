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

namespace PacManProject
{
    class Class1
    {
        Texture2D[] UImgs, DImgs, RImgs, LImgs;
        Texture2D img;
        Rectangle rec;

        int LframeIndex, RFrameIndex, UFrameIndex, DFrameIndex;
        int dx, dy;
        int timer;

        int pacSize = 30;

        public Class1(ContentManager content, int w, int h)
        {
            LframeIndex = 3;

            UImgs = new Texture2D[3];
            RImgs = new Texture2D[3];
            DImgs = new Texture2D[3];
            LImgs = new Texture2D[3];

            // load all pac man images into array
            for (int i = 0; i < 3; i++)
            {
                UImgs[i] = content.Load<Texture2D>(i + "");
                RImgs[i] = content.Load<Texture2D>((i + 3) + "");
                DImgs[i] = content.Load<Texture2D>((i + 6) + "");
                LImgs[i] = content.Load<Texture2D>((i + 9) + "");
            }

            img = RImgs[0];

            // Create player rectangle
            rec = new Rectangle((w / 2) - RImgs[0].Width, (h / 2) - RImgs[0].Height, pacSize, pacSize);
        }

        public void move()
        {
            KeyboardState kb = Keyboard.GetState();

            rec.X += dx;
            rec.Y += dy;

            // Lateral Movement
            if (kb.IsKeyDown(Keys.D))
            {
                dx = 2;
                dy = 0;
            }

            if (kb.IsKeyDown(Keys.A))
            {
                dx = -2;
                dy = 0;
            }

            // Vertical Movement
            if (kb.IsKeyDown(Keys.W))
            {
                dy = -2;
                dx = 0;
            }

            if (kb.IsKeyDown(Keys.S))
            {
                dy = 2;
                dx = 0;
            }

            // Restricting player within map bounds
            if (rec.X >= (1000 - pacSize))
                rec.X = 1000 - pacSize;
            if (rec.X <= 0)
                rec.X = 0;
            if (rec.Y <= 0)
                rec.Y = 0;
            if (rec.Y >= (600 - pacSize))
                rec.Y = 600 - pacSize;
        }

        public void animate()
        {
            timer++;

            // Only changing frame every 5/60th of a second
            if (timer % 5 == 0)
            {
                if (dx > 0) // If Pac Man Moving Right
                {
                    if (RFrameIndex == 2) // Resetting frame index to avoid index Out of Bounds Error
                        RFrameIndex = 0;
                    else
                        RFrameIndex++; // Incrementing which frame to draw
                    img = RImgs[RFrameIndex]; // setting the frame to draw
                }

                else if (dx < 0)
                {
                    if (LframeIndex >= 2)
                        LframeIndex = 0;
                    else
                        LframeIndex++;
                    img = LImgs[LframeIndex];
                }

                if (dy < 0)
                {
                    if (UFrameIndex >= 2)
                        UFrameIndex = 0;
                    else
                        UFrameIndex++;
                    img = UImgs[UFrameIndex];
                }

                else if (dy > 0)
                {
                    if (DFrameIndex >= 2)
                        DFrameIndex = 0;
                    else
                        DFrameIndex++;
                    img = DImgs[DFrameIndex];
                }

            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, rec, Color.White);
        }


    }
}
