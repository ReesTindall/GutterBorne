using System.Collections.Generic;
using UnityEngine;

public class WallScroller : MonoBehaviour
{
    public GameObject wallPrefab;
    public float scrollSpeed = 5f;
    public float wallLength = 20f;     // Length of one wall piece
    public float spawnThresholdZ = 10f; // When to spawn the next wall

    private List<GameObject> activeWalls = new List<GameObject>();

    void Start()
    {
        // Start with one wall in place
        GameObject firstWall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        activeWalls.Add(firstWall);
    }

    void Update()
    {
        // Move all walls forward
        foreach (GameObject wall in activeWalls)
        {
            wall.transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
        }

        // Check if we should spawn a new wall
        GameObject lastWall = activeWalls[activeWalls.Count - 1];

        if (lastWall.transform.position.z <= spawnThresholdZ)
        {
            // Spawn a new wall at the end
            Vector3 newPos = lastWall.transform.position + new Vector3(0, 0, wallLength);
            GameObject newWall = Instantiate(wallPrefab, newPos, Quaternion.identity);
            activeWalls.Add(newWall);
        }

        if (activeWalls[0].transform.position.z > 100f) // Or some distance behind
        {
            Destroy(activeWalls[0]);
            activeWalls.RemoveAt(0);
        }
    }
}
