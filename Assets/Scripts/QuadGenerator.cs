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

		//mesh.vertices = new Vector3[] {
		//	new Vector3(-0.5f, 0, -0.5f),
		//	new Vector3(0.5f, 0, -0.5f),
		//	new Vector3(0.5f, 0, 0.5f),
		//	new Vector3(-0.5f, 0, 0.5f)
		//};

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
            offset += new Vector2(transform.position.x, transform.position.z);
        }

        var yaw = transform.eulerAngles.y;
        var sinYaw = Mathf.Sin(yaw * Mathf.Deg2Rad);
        var cosYaw = Mathf.Cos(yaw * Mathf.Deg2Rad);

        var x0 = offset.x * UVScale;
        var y0 = offset.y * UVScale;
        var x1 = offset.x * UVScale + transform.localScale.x * UVScale;
        var y1 = offset.y * UVScale + transform.localScale.z * UVScale;

        MeshFilter.sharedMesh.uv = new Vector2[] {
            new Vector2(cosYaw*x0 + sinYaw*y0, -sinYaw*x0 + cosYaw*y0), // 0, 0
            new Vector2(cosYaw*x1 + sinYaw*y0, -sinYaw*x1 + cosYaw*y0), // 1, 0
            new Vector2(cosYaw*x1 + sinYaw*y1, -sinYaw*x1 + cosYaw*y1), // 1, 1
            new Vector2(cosYaw*x0 + sinYaw*y1, -sinYaw*x0 + cosYaw*y1)  // 0, 1
        };
    }
}
