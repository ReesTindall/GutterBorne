using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatKingController : MonoBehaviour
{
    [Header("Ring spin")]
    public float spinSpeed = 40f;           // deg/sec

    [Header("Wave settings")]
    public float timeBetweenWaves = 5f;
    public int   ratsPerWave     = 3;

    int   activeRats; 

    List<RatController> ring = new List<RatController>();
    bool attacking;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            RatController rc = child.GetComponent<RatController>();
            if (rc)
            {
                rc.enabled = false;               // dormant in ring
                rc.king    = this;                // NEW (see RatController)
                rc.GetComponent<RatHealth>().invulnerable = true;
                ring.Add(rc);
            }
        }
    }

    void Start() => StartCoroutine(WaveLoop());

    void Update()
    {
        /* spin entire ring */
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator WaveLoop()
    {
        while (ring.Count > 0)
        {
            LaunchWave();                         // ← launch immediately
            while (activeRats > 0)                // wait until they all die
                yield return null;

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void LaunchWave()
    {
        int launchCount = Mathf.Min(ratsPerWave, ring.Count);

        for (int i = 0; i < launchCount; i++)
        {
            int idx = Random.Range(0, ring.Count);
            RatController rc = ring[idx];
            ring.RemoveAt(idx);

            rc.transform.parent = null;
            rc.GetComponent<RatHealth>().invulnerable = false;
            rc.enabled = true;
            activeRats++;                         // track alive attackers
        }
    }
     public void NotifyRatDead() => activeRats--;
}