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

    List<RatController> ring = new List<RatController>();
    bool attacking;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            RatController rc = child.GetComponent<RatController>();
            if (rc) { ring.Add(rc); rc.enabled = false; }    // keep them dormant
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
            yield return new WaitForSeconds(timeBetweenWaves);
            LaunchWave();
        }
    }

    void LaunchWave()
    {
        int launchCount = Mathf.Min(ratsPerWave, ring.Count);

        for (int i = 0; i < launchCount; i++)
        {
            /* pick a random rat from the ring */
            int idx = Random.Range(0, ring.Count);
            RatController rc = ring[idx];
            ring.RemoveAt(idx);

            /* detach from the ring so it stops spinning */
            rc.transform.parent = null;

            /* enable its AI */
            rc.enabled = true;

            /* optional: give it a short forward impulse so it shoots out */
            rc.transform.position += rc.transform.forward * 0.5f;
        }
    }
}