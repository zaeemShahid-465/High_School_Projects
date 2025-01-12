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

namespace Pac_Man
{
    class Player
    {
        Texture2D[] UImgs, DImgs, RImgs, LImgs;
        Texture2D img;
        public Rectangle rec;

        int LframeIndex, RFrameIndex, UFrameIndex, DFrameIndex;
        int dx, dy;
        int timer;

        int pacSize = 60;

        public Vector2 pacPos;

        int speed = 4;

        public Player(ContentManager content, int w, int h)
        {
            pacPos = new Vector2(rec.X, rec.Y);

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
            rec = new Rectangle((w / 2) - RImgs[0].Width, (h / 2) - RImgs[0].Height + 70, pacSize, pacSize);
        }

        public Rectangle getRectangle()
        {
            return rec;
        }

        public void move(Rectangle[] rects)
        {
            KeyboardState kb = Keyboard.GetState();

            rec.X += dx;
            rec.Y += dy;

            int newX = rec.X + dx;
            int newY = rec.Y + dy;

            // stopping potential collisions if player is already moving and the user hasnt pressed a key
            if (dx < 0)
            {
                if (willColide(new Rectangle(newX - pacSize / 3, rec.Y, pacSize, pacSize), rects))
                {
                    dx = 0;
                }
            }

            else if (dx > 0)
            {
                if (willColide(new Rectangle(newX + pacSize / 3, rec.Y, pacSize, pacSize), rects))
                    dx = 0;
            }

            if (dy > 0)
            {
                if (willColide(new Rectangle(rec.X, newY + pacSize / 3, pacSize, pacSize), rects))
                    dy = 0;
            }

            else if (dy < 0)
            {
                if (willColide(new Rectangle(rec.X, newY - pacSize / 3, pacSize, pacSize), rects))
                    dy = 0;
            }


            // Horizontal Movement
            if (kb.IsKeyDown(Keys.D))
            {
                if (!willColide(new Rectangle(newX + pacSize, rec.Y, pacSize, pacSize), rects))
                {
                    dx = speed;
                    dy = 0;
                }
            }

            if (kb.IsKeyDown(Keys.A))
            {
                if (!willColide(new Rectangle(newX - pacSize, rec.Y, pacSize, pacSize), rects))
                {
                    dx = -speed;
                    dy = 0;
                }
            }

            // Vertical Movement
            if (kb.IsKeyDown(Keys.W))
            {
                if (!willColide(new Rectangle(rec.X, newY - pacSize, pacSize, pacSize), rects))
                {
                    dy = -speed;
                    dx = 0;
                }
            }

            if (kb.IsKeyDown(Keys.S))
            {
                if (!willColide(new Rectangle(rec.X, newY + pacSize, pacSize, pacSize), rects))
                {
                    dy = speed;
                    dx = 0;
                }
            }
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

        public bool willColide(Rectangle futureRec, Rectangle[] rects)
        {
            foreach (Rectangle r in rects)
            {
                if (futureRec.Intersects(r))
                    return true;
            }
            return false;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, rec, Color.White);
        }


    }
}
