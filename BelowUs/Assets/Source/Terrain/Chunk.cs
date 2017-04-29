using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// Defines a single chunk, which encapsulates a number of blocks.
    /// <para />
    /// Chunks should be tied to game objects and are used to enable batch draw calls for groups of blocks.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {

        #region Properties/Fields

        /// <summary>
        /// All blocks included in this chunk.
        /// </summary>
        public Block[,,] Blocks = new Block[ChunkSize, ChunkSize, ChunkSize];

        /// <summary>
        /// This is the size on all three axis of a chunk.
        /// </summary>
        public static int ChunkSize = 16;

        /// <summary>
        /// Instructs this chunk to redraw itself in the next update.
        /// </summary>
        public bool RequiresUpdate = true;

        /// <summary>
        /// The world that owns this chunk.
        /// </summary>
        public World World;

        /// <summary>
        /// The world position of this chunk.
        /// </summary>
        public WorldPosition Position;

        /// <summary>
        /// The mesh filter used for this chunk.
        /// </summary>
        private MeshFilter filter;

        /// <summary>
        /// The mesh collider for this chunk.
        /// </summary>
        private MeshCollider coll;

        #endregion

        #region Unity Methods

        private void Start()
        {
            filter = gameObject.GetComponent<MeshFilter>();
            coll = gameObject.GetComponent<MeshCollider>();
        }

        private void Update()
        {
            if (RequiresUpdate)
            {
                RequiresUpdate = false;
                UpdateChunk();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a block in this chunk or defers selection to the world if the block is determined to be outside the bounds of this chunk.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Block GetBlock(int x, int y, int z)
        {
            if (InRange(x) && InRange(y) && InRange(z))
                return Blocks[x, y, z];
            return World.GetBlock(Position.X + x, Position.Y + y, Position.Z + z);
        }

        /// <summary>
        /// Returns a boolean indicating if the given index is within the range of this chunk.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool InRange(int index)
        {
            if (index < 0 || index >= ChunkSize)
                return false;

            return true;
        }

        /// <summary>
        /// Sets the block at the given position to the given block.  If the position is outside this chunk, the operation is sent to the world object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="block"></param>
        public void SetBlock(int x, int y, int z, Block block)
        {
            if (InRange(x) && InRange(y) && InRange(z))
            {
                Blocks[x, y, z] = block;
            }
            else
            {
                World.SetBlock(Position.X + x, Position.Y + y, Position.Z + z, block);
            }
        }

        /// <summary>
        /// Updates the chunk based on its contents.
        /// </summary>
        private void UpdateChunk()
        {
            MeshData meshData = new MeshData();

            for (int x = 0; x < ChunkSize; x++)
            {
                for (int y = 0; y < ChunkSize; y++)
                {
                    for (int z = 0; z < ChunkSize; z++)
                    {
                        meshData = Blocks[x, y, z].AddBlockDataToMesh(this, x, y, z, meshData);
                    }
                }
            }

            RenderMesh(meshData);
        }

        /// <summary>
        /// Sends the calculated mesh information to the mesh and collision components.
        /// </summary>
        /// <param name="meshData"></param>
        private void RenderMesh(MeshData meshData)
        {
            filter.mesh.Clear();
            filter.mesh.vertices = meshData.Vertices.ToArray();
            filter.mesh.triangles = meshData.Triangles.ToArray();

            filter.mesh.uv = meshData.UV.ToArray();
            filter.mesh.RecalculateNormals();

            coll.sharedMesh = null;
            Mesh mesh = new Mesh()
            {
                vertices = meshData.ColVertices.ToArray(),
                triangles = meshData.ColTriangles.ToArray()
            };
            mesh.RecalculateNormals();

            coll.sharedMesh = mesh;
        }

        #endregion

    }
}
