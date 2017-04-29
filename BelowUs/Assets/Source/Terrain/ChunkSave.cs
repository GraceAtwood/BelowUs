using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// The class used for saving a chunk to the disk.  This is basically just a DTO of the chunk.
    /// </summary>
    [Serializable]
    public class ChunkSave
    {
        /// <summary>
        /// All the blocks included in this chunk.
        /// </summary>
        public Dictionary<WorldPosition, Block> Blocks = new Dictionary<WorldPosition, Block>();

        /// <summary>
        /// Creates a new chunk save from a given chunk and copies all its blocks.
        /// </summary>
        /// <param name="chunk"></param>
        public ChunkSave(Chunk chunk)
        {
            for (int x = 0; x < Chunk.ChunkSize; x++)
            {
                for (int y = 0; y < Chunk.ChunkSize; y++)
                {
                    for (int z = 0; z < Chunk.ChunkSize; z++)
                    {
                        WorldPosition pos = new WorldPosition(x, y, z);
                        Blocks.Add(pos, chunk.Blocks[x, y, z]);
                    }
                }
            }
        }
    }
}
