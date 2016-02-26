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

    public bool FrontFace = true;
    public bool TopFace = true;
    public bool BackFace = true;
    public bool BottomFace = true;
    public bool LeftFace = true;
    public bool RightFace = true;

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

        var vertices = new List<Vector3>();
        var tris = new List<int>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        var vertCount = 0;

        if (FrontFace)
        {
            vertices.AddRange(new Vector3[] { verts[0], verts[1], verts[2], verts[3] });
            tris.AddRange(new int[] { 0 + vertCount, 3 + vertCount, 2 + vertCount, 2 + vertCount, 1 + vertCount, 0 + vertCount });
            normals.AddRange(new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward });
            uvs.AddRange(new Vector2[] { new Vector2(x0, y0), new Vector2(x1, y0), new Vector2(x1, y1), new Vector2(x0, y1) });
            vertCount += 4;
        }
        if (TopFace)
        {
            vertices.AddRange(new Vector3[] { verts[0], verts[4], verts[5], verts[1] });
            tris.AddRange(new int[] { 0 + vertCount, 3 + vertCount, 2 + vertCount, 2 + vertCount, 1 + vertCount, 0 + vertCount });
            normals.AddRange(new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up });
            uvs.AddRange(new Vector2[] { new Vector2(z0, x0), new Vector2(z1, x0), new Vector2(z1, x1), new Vector2(z0, x1) });
            vertCount += 4;
        }
        if (BackFace)
        {
            vertices.AddRange(new Vector3[] { verts[4], verts[5], verts[6], verts[7] });
            tris.AddRange(new int[] { 0 + vertCount, 1 + vertCount, 2 + vertCount, 2 + vertCount, 3 + vertCount, 0 + vertCount });
            normals.AddRange(new Vector3[] { Vector3.back, Vector3.back, Vector3.back, Vector3.back });
            uvs.AddRange(new Vector2[] { new Vector2(x0, y0), new Vector2(x1, y0), new Vector2(x1, y1), new Vector2(x0, y1) });
            vertCount += 4;
        }
        if (BottomFace)
        {
            vertices.AddRange(new Vector3[] { verts[3], verts[7], verts[6], verts[2] });
            tris.AddRange(new int[] { 0 + vertCount, 1 + vertCount, 2+vertCount, 2 + vertCount, 3 + vertCount, 0 + vertCount });
            normals.AddRange(new Vector3[] { Vector3.down, Vector3.down, Vector3.down, Vector3.down });
            uvs.AddRange(new Vector2[] { new Vector2(z0, x0), new Vector2(z1, x0), new Vector2(z1, x1), new Vector2(z0, x1) });
            vertCount += 4;
        }
        if (LeftFace)
        {
            vertices.AddRange(new Vector3[] { verts[0], verts[4], verts[7], verts[3] });
            tris.AddRange(new int[] { 0 + vertCount, 1 + vertCount, 2 + vertCount, 2 + vertCount, 3 + vertCount, 0 + vertCount });
            normals.AddRange(new Vector3[] { Vector3.left, Vector3.left, Vector3.left, Vector3.left });
            uvs.AddRange(new Vector2[] { new Vector2(z0, y0), new Vector2(z1, y0), new Vector2(z1, y1), new Vector2(z0, y1) });
            vertCount += 4;
        }
        if (RightFace)
        {
            vertices.AddRange(new Vector3[] { verts[1], verts[2], verts[6], verts[5] });
            tris.AddRange(new int[] { 0 + vertCount, 1 + vertCount, 2 + vertCount, 2 + vertCount, 3 + vertCount, 0 + vertCount });
            normals.AddRange(new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right });
            uvs.AddRange(new Vector2[] { new Vector2(y0, z0), new Vector2(y1, z0), new Vector2(y1, z1), new Vector2(y0, z1) });
            vertCount += 4;
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.normals = normals.ToArray();
        MeshFilter.sharedMesh = mesh;
        MeshFilter.sharedMesh.uv = uvs.ToArray();
    }
}
