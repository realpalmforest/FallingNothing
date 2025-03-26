using FallingNothing.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FallingNothing
{
    public class World
    {
        public float Scale = 1f;

        public Particle[,] Map;
        public int Width { get => Map.GetLength(0); }
        public int Height { get => Map.GetLength(1); }

        private Texture2D mapTexture;

        public World(int width, int height)
        {
            Map = new Particle[width, height];
        }

        public void UpdateMap()
        {
            Parallel.For(0, Width, x =>
            {
                for (int y = Height - 1; y >= 0; y--)
                {
                    UpdateParticleAt(x, y);
                }
            });


            //for (int y = Height - 1; y >= 0; y--)
            //{
            //    // Randomise the X update direction to prevent bias
            //    //if (Utils.NextBool(2)) // Update X left to right
            //    //{
            //    //    for (int x = 0; x < Width; x++)
            //    //        UpdateParticleAt(x, y);
            //    //}
            //    //else // Update X right to left
            //    //{
            //    //    for (int x = Width - 1; x >= 0; x--)
            //    //        UpdateParticleAt(x, y);
            //    //}  
            //}
        }

        public void RenderMap()
        {
            mapTexture ??= new Texture2D(Game1.Instance.GraphicsDevice, Width, Height);
            Color[] data = new Color[Width * Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    data[y * Width + x] = Map[x, y].Color;
                }
            }

            mapTexture.SetData(data);
        }

        public void DrawMap()
        {
            Debug.Assert(mapTexture is not null);
            Game1.Instance.SpriteBatch.Draw(mapTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }


        private void UpdateParticleAt(int x, int y)
        {
            switch (Map[x, y].Material)
            {
                case Material.Air:
                    return;
                case Material.Sand:
                    ParticleFallUpdate(x, y);
                    return;
            }
        }

        public void SetParticleAt(int x, int y, Particle particle)
        {
            if (IsPositionInBounds(x, y))
                Map[x, y] = particle;
        }

        private bool IsPositionInBounds(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;

        private void MoveParticle(int x, int y, int newX, int newY)
        {
            if (x == newX && y == newY)
                return;

            SetParticleAt(newX, newY, Map[x, y]);
            Map[x, y] = new Particle(Material.Air);
        }


        private void ParticleFallUpdate(int x, int y)
        {
            if (y >= Height - 1)
                return;

            bool belowAvailable = Map[x, y + 1].Material == Material.Air;
            bool belowLeftAvailable = x > 0 && Map[x - 1, y + 1].Material == Material.Air;
            bool belowRightAvailable = x < Width - 1 && Map[x + 1, y + 1].Material == Material.Air;

            int xOffset = 0; int yOffset = 0;

            if (belowAvailable)
                yOffset = 1;
            else if (belowLeftAvailable && belowRightAvailable)
                xOffset += Utils.NextBool(2) ? -1 : 1;
            else if (belowLeftAvailable)
            {
                yOffset = 1;
                xOffset = -1;
            }
            else if (belowRightAvailable)
            {
                yOffset = 1;
                xOffset = 1;
            }

            if (xOffset != 0 || yOffset != 0)
                MoveParticle(x, y, x + xOffset, y + yOffset);
        }





        /// </summary>
        /// <param name="startX">The starting x coordinate</param>
        /// <param name="startY">The starting y coordinate</param>
        /// <param name="endX">The ending x coordinate</param>
        /// <param name="endY">The ending y coordinate</param>
        /// <returns>A list of points forming a line from the start to the end position.</returns>
        public static List<(int, int)> GetLine(int startX, int startY, int endX, int endY)
        {
            List<(int, int)> line = new List<(int, int)>();

            int changeX = Math.Abs(endX - startX);
            int changeY = Math.Abs(endY - startY);
            int dirX = startX < endX ? 1 : -1;
            int dirY = startY < endY ? 1 : -1;
            int err = changeX - changeY;

            while (true)
            {
                // Include the current point
                line.Add((startX, startY));

                // Check if the end point has been reached
                if (startX == endX && startY == endY)
                    break;

                // Increase the X and Y in the appropriate directions
                int e2 = 2 * err;
                if (e2 > -changeY)
                {
                    err -= changeY;
                    startX += dirX;
                }

                if (e2 < changeX)
                {
                    err += changeX;
                    startY += dirY;
                }
            }

            return line;
        }
    }
}