using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TubeTrail : MonoBehaviour
{
    [Header("Trail Settings")]
    public float trailLength = 5f;           // How long the trail is (world units)
    public float ringSpacing = 0.1f;         // Distance between rings
    public float trailRadius = 0.1f;         // Radius of the tube
    public int ringResolution = 8;           // Number of points around each ring (more = smoother)

    public Transform trailAnchor;

    private Mesh mesh;
    private List<Vector3> ringCenters = new List<Vector3>(); // World positions of each ring
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Slime Tube Trail";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // void FixedUpdate()
    // {
    //     // Add a new ring center
    //     ringCenters.Insert(0, transform.position);

    //     // Limit total rings based on length and spacing
    //     float maxRings = trailLength / ringSpacing;
    //     if (ringCenters.Count > maxRings)
    //     {
    //         ringCenters.RemoveAt(ringCenters.Count - 1);
    //     }
    //     RebuildMesh();
    // }
void FixedUpdate()
{
    // Make sure anchor is assigned
    if (trailAnchor == null) return;

    Vector3 direction = -trailAnchor.forward;

    Vector3 lastRing = (ringCenters.Count > 0) ? ringCenters[0] : trailAnchor.position;

    Vector3 newRingPos = lastRing + direction * ringSpacing;

    ringCenters.Insert(0, newRingPos);

    float maxRings = trailLength / ringSpacing;
    if (ringCenters.Count > maxRings)
    {
        ringCenters.RemoveAt(ringCenters.Count - 1);
    }

    RebuildMesh();
}


    void RebuildMesh()
    {
        mesh.Clear();
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();

        int ringCount = ringCenters.Count;
        if (ringCount < 2) return;

        for (int i = 0; i < ringCount; i++)
        {
            Vector3 center = ringCenters[i];
            Vector3 forward = (i == ringCount - 1) ? (ringCenters[i - 1] - center).normalized : (ringCenters[i] - ringCenters[i + 1]).normalized;

            // Get an orientation for the ring using forward
            Quaternion rotation = Quaternion.LookRotation(forward);
            for (int j = 0; j < ringResolution; j++)
            {
                float angle = ((float)j / ringResolution) * Mathf.PI * 2f;
                Vector3 localPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * trailRadius;
                vertices.Add(center + rotation * localPos);
                uvs.Add(new Vector2((float)j / ringResolution, (float)i / ringCount));
            }
        }

        // Build triangles between rings
        for (int i = 0; i < ringCount - 1; i++)
        {
            int baseIndex = i * ringResolution;
            int nextBaseIndex = (i + 1) * ringResolution;

            for (int j = 0; j < ringResolution; j++)
            {
                int current = baseIndex + j;
                int next = baseIndex + (j + 1) % ringResolution;
                int currentNext = nextBaseIndex + j;
                int nextNext = nextBaseIndex + (j + 1) % ringResolution;

                // Two triangles per quad
                triangles.Add(current);
                triangles.Add(next);
                triangles.Add(currentNext);

                triangles.Add(currentNext);
                triangles.Add(next);
                triangles.Add(nextNext);
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();
    }
}
