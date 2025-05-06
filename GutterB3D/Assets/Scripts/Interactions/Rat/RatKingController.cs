using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatKingController : MonoBehaviour
{
    [SerializeField] GameObject grateObject; 
    [Header("Ring spin")]
    public float spinSpeed = 40f;

    [Header("Wave settings")]
    public float timeBetweenWaves = 5f;
    public int ratsPerWave = 3;

    public int RatsInRing   => ring.Count;
    public int ActiveRats   => activeRats;
    public int TotalWaves   => totalWaves;

    public int CurrentWave => currentWave;

    List<RatController> ring = new();
    int activeRats;
    int totalWaves;

    int currentWave = 0; 
  
    void Awake()
    {
        foreach (Transform child in transform)
        {
            RatController rc = child.GetComponent<RatController>();
            if (!rc) continue;

            rc.enabled = false;
            rc.king    = this;

            RatHealth h = rc.GetComponent<RatHealth>();
            if (h) h.invulnerable = true;

            ring.Add(rc);
        }

        ratsPerWave = Mathf.Max(1, ratsPerWave);
        totalWaves  = Mathf.CeilToInt((float)ring.Count / ratsPerWave);
    }

    void Start() => StartCoroutine(WaveLoop());

    void Update() => transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);

    IEnumerator WaveLoop()
    {
        while (ring.Count > 0)
        {
            LaunchWave();
            while (activeRats > 0) yield return null;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        if (grateObject) grateObject.SetActive(true);
    }

    void LaunchWave()
    {
        currentWave++;   
        int launchCount = Mathf.Min(ratsPerWave, ring.Count);

        for (int i = 0; i < launchCount; i++)
        {
            int idx = Random.Range(0, ring.Count);
            RatController rc = ring[idx];
            ring.RemoveAt(idx);

            rc.transform.parent = null;
            rc.GetComponent<RatHealth>().invulnerable = false;
            rc.enabled = true;
            Rigidbody rbr = rc.GetComponent<Rigidbody>();    
            if (rbr) { rbr.angularVelocity = Vector3.zero; } 

            activeRats++;
        }
    }

    public void NotifyRatDead() => activeRats--;
}