using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain.Blocks
{
    /// <summary>
    /// An air block.  It's transparent on all sides and has no mesh rendering data.
    /// </summary>
    [Serializable]
    public class Air : Block
    {
        /// <summary>
        /// Creates a new air block.
        /// </summary>
        public Air() 
            : base()
        {

        }

        /// <summary>
        /// Returns the mesh data, unchanged.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        public override MeshData AddBlockDataToMesh(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            return meshData;
        }

        /// <summary>
        /// Returns false for all positions.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool IsSolid(Directions direction)
        {
            return false;
        }
    }
}
