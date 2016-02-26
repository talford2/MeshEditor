using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Wall : MonoBehaviour
{
	public MeshFilter MeshFilter;

	public float WallThickness = 0.1f;

	public float WallLength = 2f;

	public float WallHeight = 2f;

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

		mesh.vertices = new Vector3[] {
			new Vector3(0,0,0),
			new Vector3(WallLength,0,0),
			new Vector3(WallLength, 0, WallThickness),
			new Vector3(0, 0, WallThickness),
			new Vector3(0,WallHeight,0),
			new Vector3(WallLength, WallHeight,0),
			new Vector3(WallLength, WallHeight, WallThickness),
			new Vector3(0,WallHeight, WallThickness)
		};

		var tris = new List<int>() {
			0,4,5,0,5,1,
			4,7,6,4,6,5,
			3,6,7,3,2,6
		};

		if (FrontCap)
		{
			tris.AddRange(new List<int> {
				3,7,4,
				0,3,4
			});
		}

		if (BackCap)
		{
			tris.AddRange(new List<int> {
				1,5,6,
				1,6,2
			});
		}

		mesh.triangles = tris.ToArray();
		
		mesh.normals = new Vector3[] {
			Vector3.back,
			Vector3.back,

			Vector3.forward,
			Vector3.forward,

			Vector3.up,
			Vector3.up,

			Vector3.up,
			Vector3.up
		};

		MeshFilter.sharedMesh = mesh;

        var offset = Offset;

        if (RelativeToWorld)
        {
            offset += transform.position; 
        }

        var x0 = offset.x * UVScale;
        var y0 = offset.y * UVScale;
        var z0 = offset.z * UVScale;

        var x1 = offset.x * UVScale + transform.localScale.x * UVScale;
        var y1 = offset.y * UVScale + transform.localScale.y * UVScale;
        var z1 = offset.z * UVScale + transform.localScale.z * UVScale;

        MeshFilter.sharedMesh.uv = new Vector2[]
        {
            new Vector2(x0, y0),
            new Vector2(x1, y0),
            new Vector2(x1, y1),
            new Vector2(x1, y0),

            new Vector2(x0, z1),
            new Vector2(x1, z1),
            new Vector2(x1, z0),
            new Vector2(x0, z0)
        };
    }
}
