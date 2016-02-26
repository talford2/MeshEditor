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
            // Bottom
            verts[3], verts[7], verts[6], verts[2], // 12, 13, 14, 15
            // Left
            verts[0], verts[4], verts[7], verts[3], // 16, 17, 18, 19
            // right
            verts[1], verts[2], verts[6], verts[5], // 20, 21, 22, 23
        };

        var tris = new int[]
        {
            // front
            0, 3, 2, 2, 1, 0,
            // top
            4, 7, 6, 6, 5, 4,
            // back
            8, 9, 10, 10, 11, 8,
            // bottom
            12, 13, 14, 14, 15, 12,
            // left
            16, 17, 18, 18, 19, 16,
            // right
            20, 21, 22, 22, 23, 20
        };

        mesh.triangles = tris;

        mesh.normals = new Vector3[] {
            Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
            Vector3.up, Vector3.up, Vector3.up, Vector3.up,
            Vector3.back, Vector3.back, Vector3.back, Vector3.back,
            Vector3.down, Vector3.down, Vector3.down, Vector3.down,
            Vector3.left, Vector3.left, Vector3.left, Vector3.left,
            Vector3.right, Vector3.right, Vector3.right, Vector3.right
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
            // bottom
            new Vector2(z0, x0),
            new Vector2(z1, x0),
            new Vector2(z1, x1),
            new Vector2(z0, x1),
            // left
            new Vector2(z0, y0),
            new Vector2(z1, y0),
            new Vector2(z1, y1),
            new Vector2(z0, y1),
            // right
            new Vector2(y0, z0),
            new Vector2(y1, z0),
            new Vector2(y1, z1),
            new Vector2(y0, z1),
        };
    }
}
