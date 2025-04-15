using System.Collections.Generic;
using UnityEngine;

public class WallScroller : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject groundPrefab;
    public float scrollSpeed = 5f;

    public float wallLength = 20f;     // Offset for wall pieces
    public float groundLength = 80f;   // Offset for ground pieces (set this to match your ground prefab's length)
    public float spawnThresholdZ = 10f; // When to spawn the next object

    private List<GameObject> activeWalls = new List<GameObject>();
    private List<GameObject> activeGrounds = new List<GameObject>();

    void Start()
    {
        // Start with one wall and one ground piece in place.
        activeWalls.Add(Instantiate(wallPrefab, Vector3.zero, Quaternion.identity));
        activeGrounds.Add(Instantiate(groundPrefab, Vector3.zero, Quaternion.identity));
    }

    void Update()
    {
        ScrollObjects(activeWalls, wallPrefab, wallLength);
        ScrollObjects(activeGrounds, groundPrefab, groundLength);
    }

    // Helper method that scrolls, spawns new objects, and removes old ones.
    void ScrollObjects(List<GameObject> objects, GameObject prefab, float prefabLength)
    {
        // Move every object in the list.
        foreach (GameObject obj in objects)
        {
            obj.transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
        }

        // Check the last object to see if we need to spawn a new one.
        GameObject last = objects[objects.Count - 1];
        if (last.transform.position.z <= spawnThresholdZ)
        {
            Vector3 newPos = last.transform.position + new Vector3(0, 0, prefabLength);
            objects.Add(Instantiate(prefab, newPos, Quaternion.identity));
        }

        // Remove the first object if it's way behind.
        if (objects[0].transform.position.z > 100f) // Adjust threshold as needed.
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);
        }
    }
}