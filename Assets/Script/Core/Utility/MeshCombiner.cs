using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshCombiner
{
    public static GameObject CombineMeshes(GameObject target, Material mat)
    {
        GameObject combinedTarget = CombineMeshes(target);
        combinedTarget.GetComponent<Renderer>().material = mat;

        return combinedTarget;
    }
    public static GameObject CombineMeshes(GameObject target)
    {
        List<MeshFilter> filterList = new List<MeshFilter>();

        Transform[] childs = GetAllChild(target.transform);
        for (int i = 0; i < childs.Length; i++)
        {
            MeshFilter mesh = childs[i].GetComponent<MeshFilter>();

            if (mesh && mesh.sharedMesh)
                filterList.Add(mesh);
        }
        
        List<Tuple<Mesh, Matrix4x4>> meshDatas = new List<Tuple<Mesh, Matrix4x4>>();
        for (int i = 0; i < filterList.Count; i++)
        {
            foreach (var mesh in GetSubMeshes(filterList[i].sharedMesh))
                meshDatas.Add(Tuple.Create(mesh, filterList[i].transform.localToWorldMatrix));
        }

        GameObject combinedTarget = new GameObject(target.name + "_Combined");
        int vertexCount = 0;
        CombineInstance[] combines = new CombineInstance[meshDatas.Count];
        for (int i = 0; i < meshDatas.Count; i++)
        {
            Mesh mesh = meshDatas[i].Item1;
            combines[i].mesh = mesh;
            combines[i].transform = meshDatas[i].Item2;
            vertexCount += mesh.vertexCount;
        }

        MeshFilter filter = combinedTarget.AddComponent<MeshFilter>();
        combinedTarget.AddComponent<MeshRenderer>();

        filter.mesh = new Mesh();
        filter.mesh.indexFormat = vertexCount > ushort.MaxValue ? IndexFormat.UInt32 : IndexFormat.UInt32;
        filter.mesh.CombineMeshes(combines);

        return combinedTarget;
    }

    private static Transform[] GetAllChild(Transform target)
    {
        List<Transform> childs = new List<Transform>();

        childs.Add(target);
        for (int i = 0; i < target.childCount; i++)
            childs.AddRange(GetAllChild(target.GetChild(i)));

        return childs.ToArray();
    }
    private static Mesh[] GetSubMeshes(Mesh mesh)
    {
        if (mesh == null) return null;

        int subMeshCount = mesh.subMeshCount;
        Mesh[] subMeshes = new Mesh[subMeshCount];

        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        Vector3[] normals = mesh.normals;

        for (int i = 0; i < subMeshCount; i++)
        {
            List<Vector3> newVertices = new List<Vector3>();
            List<Vector2> newUVs = new List<Vector2>();
            List<Vector3> newNormals = new List<Vector3>();
            List<int> newTriangles = new List<int>();

            int[] triangles = mesh.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3)
            {
                int idx = triangles[j];
                int idx2 = triangles[j + 1];
                int idx3 = triangles[j + 2];

                newVertices.Add(vertices[idx]);
                newVertices.Add(vertices[idx2]);
                newVertices.Add(vertices[idx3]);

                newUVs.Add(uvs[idx]);
                newUVs.Add(uvs[idx2]);
                newUVs.Add(uvs[idx3]);

                newNormals.Add(normals[idx]);
                newNormals.Add(normals[idx2]);
                newNormals.Add(normals[idx3]);

                newTriangles.Add(newTriangles.Count);
                newTriangles.Add(newTriangles.Count);
                newTriangles.Add(newTriangles.Count);
            }

            subMeshes[i] = new Mesh();
            subMeshes[i].indexFormat = newVertices.Count > ushort.MaxValue ? IndexFormat.UInt32 : IndexFormat.UInt16;

            subMeshes[i].vertices = newVertices.ToArray();
            subMeshes[i].uv = newUVs.ToArray();
            subMeshes[i].normals = newNormals.ToArray();
            subMeshes[i].triangles = newTriangles.ToArray();
        }

        return subMeshes;
    }
}
