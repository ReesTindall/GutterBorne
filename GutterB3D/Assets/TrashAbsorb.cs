using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TrashAbsorb : MonoBehaviour
{
    [SerializeField] TrashCounter trashCounter;
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
        if(trashCounter != null) {
            if (other.CompareTag("Banana") && !hasCollided)
            {
                hasCollided = true;
                trashCounter.setBanana();

                if (absorptionSound != null)
                {
                    audioSource.outputAudioMixerGroup = mixerGroup;
                    audioSource.PlayOneShot(absorptionSound);
                    Debug.Log("playing sfx");
                }
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Gum") && !hasCollided)
            {
                hasCollided = true;
                trashCounter.setGum();

                if (absorptionSound != null)
                {
                    audioSource.outputAudioMixerGroup = mixerGroup;
                    audioSource.PlayOneShot(absorptionSound);
                    Debug.Log("playing sfx");
                }
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Cup") && !hasCollided)
            {
                hasCollided = true;
                trashCounter.setCup();

                if (absorptionSound != null)
                {
                    audioSource.outputAudioMixerGroup = mixerGroup;
                    audioSource.PlayOneShot(absorptionSound);
                    Debug.Log("playing sfx");
                }
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Paper") && !hasCollided)
            {
                hasCollided = true;
                trashCounter.setPaper();

                if (absorptionSound != null)
                {
                    audioSource.outputAudioMixerGroup = mixerGroup;
                    audioSource.PlayOneShot(absorptionSound);
                    Debug.Log("playing sfx");
                }
                    Destroy(other.gameObject);
            }
        } else {
            Debug.Log("Slime player is missing reference to Trash Counter on TrashAbsorb.cs!");
        }
        
    } 

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Banana") || other.CompareTag("Gum") || other.CompareTag("Cup") || other.CompareTag("Paper")) {
            hasCollided = false;
        }
    }
}
