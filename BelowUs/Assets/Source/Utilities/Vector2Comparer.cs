using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BelowUs.Assets.Source.Utilities
{
    /// <summary>
    /// Compares two Vector2 objects' x and y components.
    /// </summary>
    public class Vector2Comparer : IEqualityComparer<Vector2>
    {
        /// <summary>
        /// Compares two Vector2 objects' x and y components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(Vector2 x, Vector2 y)
        {
            return x.x == y.x && x.y == y.y;
        }

        /// <summary>
        /// Returns the hash code computed from the x and y components of the given Vector 2 object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(Vector2 obj)
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ obj.x.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.y.GetHashCode();
                return hashCode;
            }
        }
    }
}
