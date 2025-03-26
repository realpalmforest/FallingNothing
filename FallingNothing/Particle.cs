using Microsoft.Xna.Framework;

namespace FallingNothing
{
    public enum Material
    {
        Air, Sand
    }

    public struct Particle
    {
        public Color Color;
        public Material Material;

        public Particle()
        {
            this = new Particle(Material.Air);
        }

        public Particle(Material material)
        {
            Material = material;

            switch (material)
            {
                case Material.Air:
                    Color = Color.Transparent;
                    break;
                case Material.Sand:
                    Color = Color.Goldenrod;
                    break;
            }
        }
    }
}