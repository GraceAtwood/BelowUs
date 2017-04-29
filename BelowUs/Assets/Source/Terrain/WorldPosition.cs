using Assets.Source.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// A world position is simply a Vector3 rounded to the nearest whole numbers.  Used to represent block/chunk positions.
    /// </summary>
    [Serializable]
    public struct WorldPosition
    {
        /// <summary>
        /// The x axis component of this world position.
        /// </summary>
        public int X;

        /// <summary>
        /// The y axis component of this world position.
        /// </summary>
        public int Y;

        /// <summary>
        /// The z axis component of this world position.
        /// </summary>
        public int Z;

        /// <summary>
        /// Creates a new world position from the given x, y, and z components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public WorldPosition(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Determines if the given obj equals this instance.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is WorldPosition))
                return false;

            WorldPosition pos = (WorldPosition)obj;

            return pos.X == X && pos.Y == Y && pos.Z == Z;
        }

        /// <summary>
        /// Returns the hash code of this instance computed from all three components.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns "{X},{Y},{Z}"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{0},{1},{2}".FormatS(X, Y, Z);
        }
    }
}
