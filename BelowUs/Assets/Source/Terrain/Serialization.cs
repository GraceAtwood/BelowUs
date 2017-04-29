using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// A static class which provides methods for saving and loading chunks to and from the disk.
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// The save folder name.
        /// </summary>
        private static string saveFolderName = "worlddata";

        /// <summary>
        /// Gets the save location based on the given world name.
        /// </summary>
        /// <param name="worldName"></param>
        /// <returns></returns>
        private static string SaveLocation(string worldName)
        {
            string saveLocation = String.Format(@"{0}/{1}/", saveFolderName, worldName);

            if (!Directory.Exists(saveLocation))
            {
                Directory.CreateDirectory(saveLocation);
            }

            return saveLocation;
        }

        /// <summary>
        /// Gets the filename for a chunk location.
        /// </summary>
        /// <param name="chunkLocation"></param>
        /// <returns></returns>
        private static string FileName(WorldPosition chunkLocation)
        {
            return chunkLocation.X + "," + chunkLocation.Y + "," + chunkLocation.Z + ".bin";
        }

        /// <summary>
        /// Saves the given chunk to disk.
        /// </summary>
        /// <param name="chunk"></param>
        public static void SaveChunk(Chunk chunk)
        {
            ChunkSave save = new ChunkSave(chunk);

            if (!save.Blocks.Any())
                return;

            string saveFile = SaveLocation(chunk.World.WorldName);
            saveFile += FileName(chunk.Position);

            using (var stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, save);
            }
        }

        /// <summary>
        /// Attempts to load a chunk from disk and returns false if it doesn't exit.
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public static bool TryLoad(Chunk chunk)
        {
            string saveFile = SaveLocation(chunk.World.WorldName);
            saveFile += FileName(chunk.Position);

            if (!File.Exists(saveFile))
                return false;

            using (var stream = new FileStream(saveFile, FileMode.Open))
            {
                var formatter = new BinaryFormatter();

                ChunkSave save = (ChunkSave)formatter.Deserialize(stream);
                foreach (var block in save.Blocks)
                {
                    chunk.Blocks[block.Key.X, block.Key.Y, block.Key.Z] = block.Value;
                }
            }

            return true;
        }

    }
}
