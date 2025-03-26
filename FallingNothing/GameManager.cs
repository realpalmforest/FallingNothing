using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace FallingNothing
{
    public static class GameManager
    {
        public static World World = new World(Game1.Instance.WindowWidth, Game1.Instance.WindowHeight);

        static GameManager()
        {
            Debug.WriteLine("GameManager initialized");
            Game1.Instance.BackgroundColor = Color.Black;
            World.Map[10, 10] = new Particle(Material.Sand);
            // World.Scale = 5f;
        }

        public static void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                List<(int, int)> line = World.GetLine(InputManager.MouseStateOld.X, InputManager.MouseStateOld.Y, InputManager.MouseState.X, InputManager.MouseState.Y);

                foreach ((int x, int y) in line)
                    World.SetParticleAt(x, y, new Particle(Material.Sand));
            }

            World.UpdateMap();
            World.RenderMap();
        }

        public static void Draw()
        {
            Game1.Instance.SpriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);

            World.DrawMap();

            Game1.Instance.SpriteBatch.End();
        }
    }
}