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

    public bool walkPositiveZ = false; // Toggle this in the Inspector

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

            if (walkPositiveZ)
            {
                walker.walkDirection = Vector3.forward;
                person.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // facing +Z
            }
            else
            {
                walker.walkDirection = Vector3.back;
                person.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // facing -Z
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
