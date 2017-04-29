using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain.Blocks
{
    /// <summary>
    /// A grass block.
    /// </summary>
    [Serializable]
    public class Grass : Block
    {
        /// <summary>
        /// Creates a new grass block.
        /// </summary>
        public Grass()
            : base()
        {

        }

        /// <summary>
        /// Returns the texture positions for grass.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override Tile TexturePosition(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    return new Tile(2, 0);
                case Directions.Down:
                    return new Tile(1, 0);
            }

            return new Tile(3, 0);
        }
    }
}
