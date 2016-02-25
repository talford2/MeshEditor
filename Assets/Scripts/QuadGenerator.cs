using System.Collections.Generic;
using UnityEngine;

public class QuadGenerator : MonoBehaviour
{
	public MeshFilter MeshFilter;
	
	void Awake()
	{
		var mesh = new Mesh();
		var vert = new List<Vector3> {
			new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(1, 0, 1),
			new Vector3(0, 0, 1)
		};
		mesh.vertices = vert.ToArray();
		
		var uvs = new List<Vector2> {
			new Vector2(0,0),
			 new Vector2(transform.localScale.x,0),
			 new Vector2(transform.localScale.x,transform.localScale.z),
			 new Vector2(0,transform.localScale.z)
		};
		mesh.uv = uvs.ToArray();

		var tris = new List<int> { 2, 1, 0, 3, 2, 0 };
		mesh.triangles = tris.ToArray();

		var norms = new List<Vector3> { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
		mesh.normals = norms.ToArray();

		MeshFilter.mesh = mesh;
	}

	void Update()
	{
		MeshFilter.mesh.uv = new List<Vector2> {
			new Vector2(0,0),
			 new Vector2(transform.localScale.x,0),
			 new Vector2(transform.localScale.x,transform.localScale.z),
			 new Vector2(0,transform.localScale.z)
		}.ToArray();
	}
}
