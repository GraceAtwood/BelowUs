using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// Describes a single world with all of its chunks and methods for interacting with the terrain.
    /// </summary>
    public class World : MonoBehaviour
    {

        #region Properties

        /// <summary>
        /// A dictionary containing all the chunks, indexed by their positions.
        /// </summary>
        public Dictionary<WorldPosition, Chunk> Chunks = new Dictionary<WorldPosition, Chunk>();

        /// <summary>
        /// The prefab from which all chunks will be made.
        /// </summary>
        public GameObject ChunkPrefab;

        /// <summary>
        /// The name of this world.
        /// </summary>
        public string WorldName = "world";

        #endregion

        #region Unity Methods

        private void Start()
        {
        }

        private void Update()
        {
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a chunk at the given position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void CreateChunk(int x, int y, int z)
        {
            WorldPosition worldPos = new WorldPosition(x, y, z);

            //Instantiate the chunk at the coordinates using the chunk prefab
            GameObject newChunkObject = Instantiate(ChunkPrefab, new Vector3(x, y, z), Quaternion.Euler(Vector3.zero)) as GameObject;

            Chunk newChunk = newChunkObject.GetComponent<Chunk>();

            newChunk.Position = worldPos;
            newChunk.World = this;

            //Add it to the chunks dictionary with the position as the key
            Chunks.Add(worldPos, newChunk);

            //Set all the blocks to air.
            for (int xi = 0; xi < Chunk.ChunkSize; xi++)
            {
                for (int yi = 0; yi < Chunk.ChunkSize; yi++)
                {
                    for (int zi = 0; zi < Chunk.ChunkSize; zi++)
                    {
                        newChunk.SetBlock(xi, yi, zi, new Blocks.Air());
                    }
                }
            }
        }

        /// <summary>
        /// Removes a chunk from the game and saves it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void DestroyChunk(int x, int y, int z)
        {
            Chunk chunk = null;
            if (Chunks.TryGetValue(new WorldPosition(x, y, z), out chunk))
            {
                Serialization.SaveChunk(chunk);
                Destroy(chunk.gameObject);
                Chunks.Remove(new WorldPosition(x, y, z));
            }
        }

        /// <summary>
        /// Returns the chunk at the given position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Chunk GetChunk(int x, int y, int z)
        {
            WorldPosition pos = new WorldPosition();
            float multiple = Chunk.ChunkSize;
            pos.X = Mathf.FloorToInt(x / multiple) * Chunk.ChunkSize;
            pos.Y = Mathf.FloorToInt(y / multiple) * Chunk.ChunkSize;
            pos.Z = Mathf.FloorToInt(z / multiple) * Chunk.ChunkSize;

            Chunk chunk = null;

            Chunks.TryGetValue(pos, out chunk);

            return chunk;
        }

        /// <summary>
        /// Returns the block at the given position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Block GetBlock(int x, int y, int z)
        {
            Chunk chunk = GetChunk(x, y, z);

            if (chunk == null)
                return new Blocks.Air();

            return chunk.GetBlock(x - chunk.Position.X, y - chunk.Position.Y, z - chunk.Position.Z);
        }

        /// <summary>
        /// Finds the chunk that owns the block at the given position and sets it to the desired block.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="block"></param>
        public void SetBlock(int x, int y, int z, Block block)
        {
            Chunk chunk = GetChunk(x, y, z);

            if (chunk != null)
            {
                chunk.SetBlock(x - chunk.Position.X, y - chunk.Position.Y, z - chunk.Position.Z, block);
                chunk.RequiresUpdate = true;

                UpdateIfEqual(x - chunk.Position.X, 0, new WorldPosition(x - 1, y, z));
                UpdateIfEqual(x - chunk.Position.X, Chunk.ChunkSize - 1, new WorldPosition(x + 1, y, z));
                UpdateIfEqual(y - chunk.Position.Y, 0, new WorldPosition(x, y - 1, z));
                UpdateIfEqual(y - chunk.Position.Y, Chunk.ChunkSize - 1, new WorldPosition(x, y + 1, z));
                UpdateIfEqual(z - chunk.Position.Z, 0, new WorldPosition(x, y, z - 1));
                UpdateIfEqual(z - chunk.Position.Z, Chunk.ChunkSize - 1, new WorldPosition(x, y, z + 1));
            }
        }

        private void UpdateIfEqual(int value1, int value2, WorldPosition pos)
        {
            if (value1 == value2)
            {
                Chunk chunk = GetChunk(pos.X, pos.Y, pos.Z);
                if (chunk != null)
                {
                    chunk.RequiresUpdate = true;
                }
            }
        }

        #endregion


    }
}
