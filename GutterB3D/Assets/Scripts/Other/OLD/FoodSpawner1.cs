using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner1 : MonoBehaviour {

    // Object variables
    // public GameObject foodPrefab;
    public List<GameObject> foodPrefabs;

    public Transform[] spawnPoints;
    private int rangeEnd;

    // Timing variables
    public float spawnIntervalMin = 0.5f;
    public float spawnIntervalMax = 1.2f;
    private float spawnTimer = 0f;
    private float timeToSpawn = 0f;

    void Start(){
        rangeEnd = spawnPoints.Length;
        timeToSpawn = Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void Update(){
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawn){
            SpawnFood();
            spawnTimer = 0f;
            timeToSpawn = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    void SpawnFood(){
        if (foodPrefabs.Count == 0 || spawnPoints.Length == 0) return;

        // int spawnIndex = Random.Range(0, spawnPoints.Length);
        int spawnIndex = Random.Range(0, rangeEnd);
        int foodIndex = Random.Range(0, foodPrefabs.Count);

        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject foodToSpawn = foodPrefabs[foodIndex];

        Instantiate(foodToSpawn, spawnPoint.position, Quaternion.identity);
        Debug.Log("Spawning Food: " + foodToSpawn.name);
    }
    //     void SpawnFood(){
    //     int spawnIndex = Random.Range(0, rangeEnd);
    //     Transform spawnPoint = spawnPoints[spawnIndex];
    //     Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
    //     Debug.Log("Spawning Food");
    // }
}