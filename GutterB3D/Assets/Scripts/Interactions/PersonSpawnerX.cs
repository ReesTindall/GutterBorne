using UnityEngine;
using System.Collections.Generic;

public class PersonSpawnerX : MonoBehaviour
{
    public GameObject personPrefab;

    public Vector2 zRange = new Vector2(-5f, 5f); // Instead of xRange
    public float spawnX = -20f;                  // Fixed X position

    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float spawnInterval = 2f;

    public bool walkNegativeX = true; // Toggle this in the Inspector

    public List<Material> skinMaterials;

    private float spawnTimer;
    private Queue<Material> recentlyUsed = new Queue<Material>();
    private int cooldownCount = 3;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnPerson();
            spawnTimer = 0f;
        }
    }

    void SpawnPerson()
    {
        Vector3 spawnPos = new Vector3(
            spawnX,
            0f,
            Random.Range(zRange.x, zRange.y)
        );

        GameObject person = Instantiate(personPrefab, spawnPos, Quaternion.identity);
        person.SetActive(true);

        // Randomize speed
        PersonWalker walker = person.GetComponent<PersonWalker>();
        if (walker != null)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            walker.walkSpeed = -speed;
            walker.animSpeed = speed;

            if (walkNegativeX)
            {
                walker.walkDirection = Vector3.right;
                person.transform.rotation = Quaternion.Euler(0f, 90f, 0f); // facing +X
            }
            else
            {
                walker.walkDirection = Vector3.left;
                person.transform.rotation = Quaternion.Euler(0f, -90f, 0f); // facing -X
            }
        }

        // Randomize material
        if (skinMaterials.Count > 0)
        {
            Material chosenMat = GetRandomMaterial();

            SkinnedMeshRenderer skinnedRenderer = person.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedRenderer != null)
            {
                Material[] newMats = skinnedRenderer.materials;
                newMats[0] = new Material(chosenMat);
                skinnedRenderer.materials = newMats;
            }

            recentlyUsed.Enqueue(chosenMat);
            if (recentlyUsed.Count > cooldownCount)
            {
                recentlyUsed.Dequeue();
            }
        }
    }

    Material GetRandomMaterial()
    {
        List<Material> available = new List<Material>(skinMaterials);
        foreach (Material m in recentlyUsed)
        {
            available.Remove(m);
        }

        if (available.Count == 0)
        {
            available.AddRange(skinMaterials);
            recentlyUsed.Clear();
        }

        return available[Random.Range(0, available.Count)];
    }
}
