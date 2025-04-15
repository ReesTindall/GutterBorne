using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DivineSlime : MonoBehaviour
{
    [SerializeField] DivineSlimeCounter divineCounter;
    public AudioClip absorptionSound;
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    private bool hasCollided = false;

    void Start()
    {
        // Try to get an AudioSource; if none exists, add one.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DivineSlime") && !hasCollided)
        {
            hasCollided = true;
            divineCounter.setSlime();

            if (absorptionSound != null)
            {
                audioSource.outputAudioMixerGroup = mixerGroup;
                audioSource.PlayOneShot(absorptionSound);
            }
            Destroy(other.gameObject);
        }
    } 

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("DivineSlime")) {
            hasCollided = false;
        }
    }
}
