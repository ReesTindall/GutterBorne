using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [Header("What & Where")]
    public GameObject ratPrefab;          // drag your Rat prefab
    public Transform [] spawnPoints;      // 1+ empty GOs in the scene

    [Header("How Many / How Often")]
    public int   startCount     = 5;      // rats spawned at Start
    public int   maxAlive       = 15;     // don’t exceed this in scene
    public float spawnInterval  = 8f;     // seconds between spawns

    int alive;

    void Start()
    {
        for (int i = 0; i < startCount; i++) SpawnRat();
        InvokeRepeating(nameof(SpawnTick), spawnInterval, spawnInterval);
    }

    void SpawnTick()
    {
        if (alive >= maxAlive) return;
        SpawnRat();
    }

    void SpawnRat()
    {
        if (!ratPrefab || spawnPoints.Length == 0) return;

        Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject rat = Instantiate(ratPrefab, p.position, p.rotation);

        alive++;
        rat.GetComponent<RatHealth>()?.OnDestroyCall(() => alive--); // helper below
    }
}