using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    class Coin
    {
        // PIVS
        private Vector2 pos;
        private Texture2D coinTexture;
        private Rectangle rect;

        // Constructor
        public Coin(Vector2 position, ContentManager Content)
        {
            pos = position;
            rect = new Rectangle((int)pos.X, (int)pos.Y, 10, 10);
            coinTexture = Content.Load<Texture2D>("dot");
        }

        //Getters
        public Rectangle getRectangle()
        {
            return rect;
        }
        public Texture2D getTexture()
        {
            return coinTexture;
        }

        public Boolean coinCollision(Player player)
        {
            if (rect.Intersects(player.getRectangle()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

