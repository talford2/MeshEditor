using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CubeGenerator : MonoBehaviour {
    public MeshFilter MeshFilter;

    public bool FrontCap = true;

    public bool BackCap = true;

    public bool RelativeToWorld = false;

    public float UVScale = 1f;

    public Vector3 Offset = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        var mesh = new Mesh();

        var verts = new Vector3[]
        {
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f)
        };

        mesh.vertices = new Vector3[] {
            // Front
            verts[0], verts[1], verts[2], verts[3], // 0, 1, 2, 3
            // Top
            verts[0], verts[4], verts[5], verts[1], // 4, 5, 6, 7
            // Back
            verts[4], verts[5], verts[6], verts[7], // 8, 9, 10, 11
        };

        var tris = new int[]
        {
            // front
            0, 3, 2, 2, 1, 0,
            // top
            4, 7, 6, 6, 5, 4,
            // back
            8, 9, 10, 10, 11, 8
        };

        mesh.triangles = tris;

        mesh.normals = new Vector3[] {
            Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
            Vector3.up, Vector3.up, Vector3.up, Vector3.up,
            Vector3.back, Vector3.back, Vector3.back, Vector3.back
        };

        MeshFilter.sharedMesh = mesh;

        var offset = Offset;

        if (RelativeToWorld)
        {
            offset += transform.position;
        }

        var x0 = (offset.x - 0.5f * transform.localScale.x) * UVScale;
        var y0 = (offset.y + 0.5f * transform.localScale.y) * UVScale;
        var z0 = (offset.z + 0.5f * transform.localScale.z) * UVScale;

        var x1 = (offset.x + 0.5f * transform.localScale.x) * UVScale;
        var y1 = (offset.y - 0.5f * transform.localScale.y) * UVScale;
        var z1 = (offset.z - 0.5f * transform.localScale.z) * UVScale;

        MeshFilter.sharedMesh.uv = new Vector2[]
        {
            // front
            new Vector2(x0, y0),
            new Vector2(x1, y0),
            new Vector2(x1, y1),
            new Vector2(x0, y1),
            // top
            new Vector2(z0, x0),
            new Vector2(z1, x0),
            new Vector2(z1, x1),
            new Vector2(z0, x1),
            // back
            new Vector2(x0, y0),
            new Vector2(x1, y0),
            new Vector2(x1, y1),
            new Vector2(x0, y1),
        };
    }
}
