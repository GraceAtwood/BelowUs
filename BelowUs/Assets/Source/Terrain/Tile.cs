using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// The x and y position of a texture tile on a texture atlas.
    /// </summary>
    public struct Tile
    {

        /// <summary>
        /// The X component of the tile's position.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The Y component of the tile's position.
        /// </summary>
        public int Y { get; private set; }

        public Tile(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
