using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class MeshExtensions
    {
        public static Mesh Clone(this Mesh original, bool withSubMeshes = true)
        {
            var clone = new Mesh
            {
                name = "Clone",
                vertices = original.vertices,
                normals = original.normals,
                uv = original.uv,
                colors = original.colors,
                tangents = original.tangents
            };
            if (withSubMeshes)
            {
                clone.subMeshCount = original.subMeshCount;
                for (int i = 0; i < original.subMeshCount; i++) clone.SetTriangles(original.GetTriangles(i), i);
            }
            else
            {
                clone.triangles = original.triangles;
            }

            return clone;
        }

        public static void SetVertexColor(this Mesh mesh, Color color)
        {
            var colors = new Color[mesh.vertices.Length];
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                colors[i] = color;
            }

            mesh.colors = colors;
        }
    }
}