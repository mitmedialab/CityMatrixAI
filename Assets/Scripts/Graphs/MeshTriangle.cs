// Builds a Mesh containing a single triangle with uvs.
// Create arrays of vertices, uvs and triangles, and copy them into the mesh.

using UnityEngine;

public class MeshTriangle : MonoBehaviour
{
    public Material material;
    public GameObject pt0;
    public GameObject pt1;
    public GameObject pt2;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        // make changes to the Mesh by creating arrays which contain the new values
        // clockwise!
        Vector3 vtx0 = transform.InverseTransformPoint(pt0.transform.position);
        Vector3 vtx1 = transform.InverseTransformPoint(pt1.transform.position);
        Vector3 vtx2 = transform.InverseTransformPoint(pt2.transform.position);
        mesh.vertices = new Vector3[] { vtx0, vtx1, vtx2 };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.RecalculateNormals();
        meshRenderer.material = material;
    }

    public void Update()
    {
        // have to create a new vertices list and assign to mesh
        Vector3 vtx0 = transform.InverseTransformPoint(pt0.transform.position);
        Vector3 vtx1 = transform.InverseTransformPoint(pt1.transform.position);
        Vector3 vtx2 = transform.InverseTransformPoint(pt2.transform.position);
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = new Vector3[] { vtx0, vtx1, vtx2 };
        mesh.RecalculateNormals();
    }

}