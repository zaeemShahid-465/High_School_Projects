using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pac_Man
{
    public class Ghost
    {
        private const int GhostAmount = 4;     // Number of ghosts
        private const int GhostSize = 60;      // Ghost dimensions
        private const int Speed = 3;           // Speed of ghost movement
        private const int BoxWidth = 240;      // Box width
        private const int BoxHeight = 120;     // Box height
        private const int ExitDistance = 100;  // Distance ghosts move upward to exit
        private Vector2 BoxPosition;           // Top-left corner of the box

        private Texture2D[] GhostTextures;     // Ghost textures
        private Vector2[] GhostPositions;      // Ghost positions
        private Vector2[] GhostDirections;     // Ghost directions
        private Vector2[] PreviousDirections;  // Avoid immediate back-and-forth
        private bool[] OutOfBox;               // Tracks if each ghost is out of the box

        private bool allGhostsExited = false;  // Flag to block the box after exit
        private int nextGhostToExit = 0;       // Index of the next ghost to exit
        private TimeSpan exitDelayTimer;       // Timer for delaying ghost exits
        private TimeSpan exitDelay = TimeSpan.FromSeconds(1);

        private int chasingGhostIndex = -1;    // Index of the ghost currently chasing Pac-Man
        private TimeSpan chaseTimer;           // Timer for chase intervals
        private TimeSpan chaseDuration = TimeSpan.FromSeconds(5);

        private Random random;                 // Random direction generator
        private int screenWidth, screenHeight;

        Rectangle[] recs;

        public Ghost(Texture2D[] textures, int screenWidth, int screenHeight)
        {
            recs = new Rectangle[4];
            GhostTextures = textures;
            GhostPositions = new Vector2[GhostAmount];
            GhostDirections = new Vector2[GhostAmount];
            PreviousDirections = new Vector2[GhostAmount];
            OutOfBox = new bool[GhostAmount];
            random = new Random();
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            BoxPosition = new Vector2((screenWidth - BoxWidth) / 2, (screenHeight - BoxHeight) / 2);
            Vector2 startPosition = BoxPosition + new Vector2(BoxWidth / 2 - GhostSize / 2, BoxHeight - GhostSize - 50);

            for (int i = 0; i < GhostAmount; i++)
            {
                GhostPositions[i] = startPosition;
                GhostDirections[i] = new Vector2(0, -1);
                PreviousDirections[i] = Vector2.Zero;
                OutOfBox[i] = false;
            }

            exitDelayTimer = TimeSpan.Zero;
            chaseTimer = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime, Rectangle[] tileRects, Vector2 pacManPosition)
        {
            for (int i = 0; i < GhostAmount; i++)
            {
                recs[i] = new Rectangle((int)GhostPositions[i].X, (int)GhostPositions[i].Y, GhostSize, GhostSize);
            }

            exitDelayTimer += gameTime.ElapsedGameTime;
            chaseTimer += gameTime.ElapsedGameTime;

            if (!allGhostsExited)
            {
                HandleGhostExit();
            }
            else
            {
                HandleChasedown(gameTime, tileRects, pacManPosition);
            }

            for (int i = 0; i < GhostAmount; i++)
            {
                if (i != chasingGhostIndex) // Ghosts not in chasedown mode
                {
                    HandleRandomMovement(i, tileRects);
                }

                // Move the ghost
                Vector2 nextPosition = GhostPositions[i] + GhostDirections[i] * Speed;

                if (Collides(nextPosition, tileRects))
                {
                    PickNewValidDirection(i, tileRects);
                }
                else
                {
                    GhostPositions[i] = nextPosition;
                }
            }
        }

        public Rectangle[] getRecs()
        {
            return recs;
        }

        private void HandleGhostExit()
        {
            if (nextGhostToExit < GhostAmount && exitDelayTimer >= exitDelay)
            {
                GhostDirections[nextGhostToExit] = new Vector2(0, -1);
                exitDelayTimer = TimeSpan.Zero;
                nextGhostToExit++;
            }

            for (int i = 0; i < GhostAmount; i++)
            {
                if (!OutOfBox[i] && GhostPositions[i].Y <= BoxPosition.Y - ExitDistance)
                {
                    OutOfBox[i] = true;
                    GhostDirections[i] = GetRandomHorizontalDirection();
                }
            }

            allGhostsExited = Array.TrueForAll(OutOfBox, exited => exited);
        }

        private void HandleChasedown(GameTime gameTime, Rectangle[] tileRects, Vector2 pacManPosition)
        {
            if (chaseTimer >= chaseDuration)
            {
                chaseTimer = TimeSpan.Zero;
                chasingGhostIndex = (chasingGhostIndex + 1) % GhostAmount;
            }

            if (chasingGhostIndex >= 0)
            {
                GhostDirections[chasingGhostIndex] = GetDirectionToTarget(GhostPositions[chasingGhostIndex], pacManPosition, tileRects);
            }
        }

        private void HandleRandomMovement(int ghostIndex, Rectangle[] tileRects)
        {
            // If ghost collides, pick a new direction
            Vector2 nextPosition = GhostPositions[ghostIndex] + GhostDirections[ghostIndex] * Speed;

            if (Collides(nextPosition, tileRects))
            {
                PickNewValidDirection(ghostIndex, tileRects);
            }
        }

        private void PickNewValidDirection(int ghostIndex, Rectangle[] tileRects)
        {
            Vector2[] possibleDirections = { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };

            for (int i = 0; i < possibleDirections.Length; i++)
            {
                Vector2 newDirection = possibleDirections[random.Next(possibleDirections.Length)];
                Vector2 testPosition = GhostPositions[ghostIndex] + newDirection * Speed;

                if (!Collides(testPosition, tileRects) && newDirection != -PreviousDirections[ghostIndex])
                {
                    GhostDirections[ghostIndex] = newDirection;
                    PreviousDirections[ghostIndex] = newDirection;
                    return;
                }
            }

            // Default to the last known valid direction
            GhostDirections[ghostIndex] = -PreviousDirections[ghostIndex];
        }

        private Vector2 GetRandomHorizontalDirection()
        {
            return random.Next(2) == 0 ? new Vector2(-1, 0) : new Vector2(1, 0);
        }

        private Vector2 GetDirectionToTarget(Vector2 currentPosition, Vector2 targetPosition, Rectangle[] tileRects)
        {
            Vector2[] directions = { new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0) };
            Vector2 bestDirection = Vector2.Zero;
            float shortestDistance = float.MaxValue;

            foreach (var direction in directions)
            {
                Vector2 testPosition = currentPosition + direction * Speed;
                if (!Collides(testPosition, tileRects))
                {
                    float distance = Vector2.Distance(testPosition, targetPosition);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        bestDirection = direction;
                    }
                }
            }

            return bestDirection == Vector2.Zero ? GetRandomHorizontalDirection() : bestDirection;
        }

        private bool Collides(Vector2 position, Rectangle[] tileRects)
        {
            Rectangle ghostRect = new Rectangle((int)position.X, (int)position.Y, GhostSize, GhostSize);
            foreach (var tile in tileRects)
            {
                if (ghostRect.Intersects(tile)) return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GhostAmount; i++)
            {
                spriteBatch.Draw(GhostTextures[i], new Rectangle((int)GhostPositions[i].X, (int)GhostPositions[i].Y, GhostSize, GhostSize), Color.White);
            }
        }
    }
}



