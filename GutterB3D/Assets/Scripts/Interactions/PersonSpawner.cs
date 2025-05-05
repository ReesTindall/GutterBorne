using UnityEngine;
using System.Collections.Generic;

public class PersonSpawner : MonoBehaviour
{
    public GameObject personPrefab;
    public Vector2 xRange = new Vector2(-5f, 5f);
    public float spawnZ = 20f;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float spawnInterval = 2f;    // Time between spawns in seconds

    public List<Material> skinMaterials;

    private float spawnTimer;

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

        // Randomize material
        // Randomize material
        if (skinMaterials.Count > 0)
        {
            SkinnedMeshRenderer skinnedRenderer = person.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedRenderer != null)
            {
                // Duplicate current materials array
                Material[] newMats = skinnedRenderer.materials;

                // Replace the first material slot (usually the body/skin) with a random one
                newMats[0] = new Material(skinMaterials[Random.Range(0, skinMaterials.Count)]);

                // Assign the new material array back
                skinnedRenderer.materials = newMats;
            }
        }
    }
}
