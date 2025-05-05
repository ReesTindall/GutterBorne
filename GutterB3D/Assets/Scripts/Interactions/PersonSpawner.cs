using UnityEngine;
using System.Collections.Generic;

public class PersonSpawner : MonoBehaviour
{
    public GameObject personPrefab;
    public Vector2 xRange = new Vector2(-5f, 5f);
    public float spawnZ = 20f;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float spawnInterval = 2f;

    public List<Material> skinMaterials;

    private float spawnTimer;

    private Queue<Material> recentlyUsed = new Queue<Material>();
    private int cooldownCount = 3; // Number of spawns to wait before a material can be reused

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
            Random.Range(xRange.x, xRange.y),
            0f,
            spawnZ
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
        }

        // Randomize material (avoiding recently used ones)
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

            // Add to cooldown queue
            recentlyUsed.Enqueue(chosenMat);
            if (recentlyUsed.Count > cooldownCount)
            {
                recentlyUsed.Dequeue();
            }
        }
    }

    Material GetRandomMaterial()
    {
        // Try to avoid recently used materials
        List<Material> available = new List<Material>(skinMaterials);
        foreach (Material m in recentlyUsed)
        {
            available.Remove(m);
        }

        // If all materials are in cooldown, allow all again
        if (available.Count == 0)
        {
            available.AddRange(skinMaterials);
            recentlyUsed.Clear();
        }

        return available[Random.Range(0, available.Count)];
    }
}
