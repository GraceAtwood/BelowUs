using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Terrain
{
    /// <summary>
    /// Contains generic mesh information.  Primarily used for drawing a chunk, but it could be used elsewhere.
    /// </summary>
    public class MeshData
    {

        /// <summary>
        /// These are all the points of the mesh triangles.
        /// </summary>
        public List<Vector3> Vertices = new List<Vector3>();

        /// <summary>
        /// These are all the triangles.  Three entries from the vertices is a single triangle.
        /// </summary>
        public List<int> Triangles = new List<int>();

        /// <summary>
        /// 2D selector triangles for the texture mesh.
        /// </summary>
        public List<Vector2> UV = new List<Vector2>();

        /// <summary>
        /// Same as the other verts, except this is for the mesh collider.
        /// </summary>
        public List<Vector3> ColVertices = new List<Vector3>();

        /// <summary>
        /// The mesh collider triangles.
        /// </summary>
        public List<int> ColTriangles = new List<int>();

        /// <summary>
        /// Instructs the mesh data to use the render data to build a collider as well.
        /// </summary>
        public bool UseRenderDataForCollider { get; private set; }

        /// <summary>
        /// Creates a new mesh data object.
        /// </summary>
        /// <param name="useRenderDataForCollider"></param>
        public MeshData(bool useRenderDataForCollider = true)
        {
            this.UseRenderDataForCollider = useRenderDataForCollider;
        }

        /// <summary>
        /// Given the last 3 added vertices, creates a quad.
        /// </summary>
        public void AddQuadTriangles()
        {
            //Here's one triangle of a rectangle.
            Triangles.Add(Vertices.Count - 4);
            Triangles.Add(Vertices.Count - 3);
            Triangles.Add(Vertices.Count - 2);

            //And here's the other one.  Notice that they share two verts.
            Triangles.Add(Vertices.Count - 4);
            Triangles.Add(Vertices.Count - 2);
            Triangles.Add(Vertices.Count - 1);

            if (UseRenderDataForCollider)
            {
                ColTriangles.Add(ColVertices.Count - 4);
                ColTriangles.Add(ColVertices.Count - 3);
                ColTriangles.Add(ColVertices.Count - 2);

                ColTriangles.Add(ColVertices.Count - 4);
                ColTriangles.Add(ColVertices.Count - 2);
                ColTriangles.Add(ColVertices.Count - 1);
            }
        }

        /// <summary>
        /// Adds a vertex.
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(Vector3 vertex)
        {
            Vertices.Add(vertex);

            if (UseRenderDataForCollider)
            {
                ColVertices.Add(vertex);
            }
        }

        /// <summary>
        /// Adds a triangle.
        /// </summary>
        /// <param name="tri"></param>
        public void AddTriangle(int tri)
        {
            Triangles.Add(tri);

            if (UseRenderDataForCollider)
            {
                ColTriangles.Add(tri - (Vertices.Count - ColVertices.Count));
            }
        }
    }
}
