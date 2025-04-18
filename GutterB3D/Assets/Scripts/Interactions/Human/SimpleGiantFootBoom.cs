using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGiantFootBoom : MonoBehaviour
{
    private AudioSource audioClip;       // the actual AudioSource
    private float clipStartPitch;        // the original note
    private float clipPitch;             // a modified note
    private float clipVolume;            // hold and modify the volume

    public GameObject groundObject;      // Drag your ground object here in the Inspector
    public float boomInterval = 2f;      // Time between automatic booms

    private float boomTimer;

    void Awake()
    {
        // populate the variables
        audioClip = GetComponent<AudioSource>();
        clipStartPitch = audioClip.pitch;
        clipPitch = audioClip.pitch;
        clipVolume = audioClip.volume;

        boomTimer = boomInterval;
    }

    void Update()
    {
        boomTimer -= Time.deltaTime;

        if (boomTimer <= 0f)
        {
            PlayBoom();
            boomTimer = boomInterval;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Only play sound if colliding with the specified ground object
        if (other.gameObject == groundObject)
        {
            PlayBoom();
        }
    }

    void PlayBoom()
    {
        if (!audioClip.isPlaying)
        {
            audioClip.Play();
        }
    }
}
