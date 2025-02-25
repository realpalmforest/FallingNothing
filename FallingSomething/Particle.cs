using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FallingSomething
{
    public enum Material
    {
        Air, Sand
    }

    public struct Particle
    {
        public Color Color = Color.Transparent;
        public Material Material = Material.Air;

        public Particle(Material material)
        {
            Material = material;

            switch (material)
            {
                case Material.Air:
                    break;
                case Material.Sand:
                    Color = Color.Goldenrod;
                    break;
            }
        }
    }
}