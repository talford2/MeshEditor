using UnityEngine;

[ExecuteInEditMode]
public class QuadGenerator : MonoBehaviour
{
	public MeshFilter MeshFilter;

	public bool RelativeToWorld = false;

	public float UVScale = 1f;

	public Vector2 Offset = Vector2.zero;

	void Awake()
	{
		var mesh = new Mesh();

		mesh.vertices = new Vector3[] {
			new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(1, 0, 1),
			new Vector3(0, 0, 1)
		};
		mesh.triangles = new int[] { 2, 1, 0, 3, 2, 0 };
		mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };

		MeshFilter.sharedMesh = mesh;
		
		SetUVs();
	}

	void Update()
	{
		SetUVs();
	}

	private void SetUVs()
	{
		var offset = Offset;

		if (RelativeToWorld)
		{
			offset = new Vector2(Offset.x + transform.position.x, Offset.y + transform.position.z);
		}
		
		MeshFilter.sharedMesh.uv = new Vector2[] {
			new Vector2(offset.x * UVScale, offset.y * UVScale),
			new Vector2(offset.x * UVScale + transform.localScale.x * UVScale, offset.y * UVScale),
			new Vector2(offset.x * UVScale + transform.localScale.x * UVScale, offset.y  * UVScale + transform.localScale.z * UVScale),
			new Vector2(offset.x * UVScale, offset.y * UVScale + transform.localScale.z * UVScale)
		};
	}
}
