using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// The base class from which all blocks should inherit.
    /// </summary>
    [Serializable]
    public class Block
    {
        private const float tileSize = 0.25f;

        /// <summary>
        /// Creates a new block.
        /// </summary>
        public Block()
        {

        }

        /// <summary>
        /// Builds the block data into the given mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        public virtual MeshData AddBlockDataToMesh(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            if (!chunk.GetBlock(x, y + 1, z).IsSolid(Directions.Down))
            {
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y - 1, z).IsSolid(Directions.Up))
            {
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z + 1).IsSolid(Directions.South))
            {
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z - 1).IsSolid(Directions.North))
            {
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x + 1, y, z).IsSolid(Directions.West))
            {
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x - 1, y, z).IsSolid(Directions.East))
            {
                meshData = FaceDataWest(chunk, x, y, z, meshData);
            }

            return meshData;
        }

        /// <summary>
        /// Adds the face data for the up direction to the mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Directions.Up));
            return meshData;
        }

        /// <summary>
        /// Adds the face data for the down direction to the mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        protected virtual MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Directions.Down));
            return meshData;
        }

        /// <summary>
        /// Adds the face data for the north direction to the mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Directions.North));
            return meshData;
        }

        /// <summary>
        /// Adds the face data for the east direction to the mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Directions.East));
            return meshData;
        }

        /// <summary>
        /// Adds the face data for the south direction to the mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Directions.South));
            return meshData;
        }

        /// <summary>
        /// Adds the face data for the west direction to the mesh data.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="meshData"></param>
        /// <returns></returns>
        protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Directions.West));
            return meshData;
        }

        /// <summary>
        /// Returns the texture position of this block.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual Tile TexturePosition(Directions direction)
        {
            return new Tile(0, 0);
        }

        /// <summary>
        /// Builds the face UV position for a direction of this block.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual Vector2[] FaceUVs(Directions direction)
        {
            Tile tilePos = TexturePosition(direction);
            return new Vector2[4]
            {
                new Vector2(tileSize * tilePos.X + tileSize, tileSize * tilePos.Y),
                new Vector2(tileSize * tilePos.X + tileSize, tileSize * tilePos.Y + tileSize),
                new Vector2(tileSize * tilePos.X, tileSize * tilePos.Y + tileSize),
                new Vector2(tileSize * tilePos.X, tileSize * tilePos.Y)
            };
        }

        /// <summary>
        /// Indicates if a face of this block is solid.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual bool IsSolid(Directions direction)
        {
            //I could've just returned true, but I thought it was more verbose to write out what was actually happening here.
            switch (direction)
            {
                case Directions.North:
                    return true;
                case Directions.East:
                    return true;
                case Directions.South:
                    return true;
                case Directions.West:
                    return true;
                case Directions.Up:
                    return true;
                case Directions.Down:
                    return true;
            }

            return false;
        }
    }

}

