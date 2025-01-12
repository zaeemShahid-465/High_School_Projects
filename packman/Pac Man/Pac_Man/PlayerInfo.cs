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
    class PlayerInfo
    {
        private int score, highscore, screenWidth, screenHeight;
        Texture2D heartsT;
        public int lives;
        

        public PlayerInfo(Texture2D T, int w, int h)
        {
            heartsT = T;
            screenWidth = w;
            screenHeight = h;
            lives = 3;
        }

        public void ScoreReset()
        {
            score = 0;
            lives = 3;
        }

        public void AddScore(int num)
        {
            score += num;
            if (score > highscore)
            {
                highscore = score;
            }
        }

        public void AddLives(int num)
        {
            lives += num;
        }

        public void SetScore(int num)
        {
            score = num;
        }

        public Boolean IsAlive()
        {
            if (lives > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayInfo(SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(heartsT, new Rectangle(10 + 50 * i, screenHeight - 60, 35, 35), Color.White);
            }
            spriteBatch.DrawString(font, "HIGH SCORE\n" + highscore, new Vector2(screenWidth / 2 + 100, screenHeight - 65), Color.White);
            spriteBatch.DrawString(font, "CURRENT SCORE\n" + score, new Vector2(screenWidth / 2 - 200, screenHeight - 65), Color.White);
        }
    }

}
