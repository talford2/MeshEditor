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
	}
}
