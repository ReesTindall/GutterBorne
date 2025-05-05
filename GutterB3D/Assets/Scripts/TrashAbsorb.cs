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
    if (trashCounter == null || hasCollided)
        return;

    hasCollided = true;

    if (other.CompareTag("Banana")) trashCounter.setBanana();
    else if (other.CompareTag("Gum")) trashCounter.setGum();
    else if (other.CompareTag("Cup")) trashCounter.setCup();
    else if (other.CompareTag("Paper")) trashCounter.setPaper();
    else {
        hasCollided = false;  // not a trash item
        return;
    }

    if (absorptionSound != null)
    {
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.PlayOneShot(absorptionSound);
    }

    Destroy(other.gameObject);

    // schedule the reset _next_ physics step
    StartCoroutine(ResetCollisionNextFrame());
}

private IEnumerator ResetCollisionNextFrame()
{
    // wait for physics to finish this step
    yield return new WaitForFixedUpdate();
    hasCollided = false;
}

    // void OnTriggerEnter(Collider other)
    // {
    //     if(trashCounter != null) {
    //         if (other.CompareTag("Banana") && !hasCollided)
    //         {
    //             hasCollided = true;
    //             trashCounter.setBanana();

    //             if (absorptionSound != null)
    //             {
    //                 audioSource.outputAudioMixerGroup = mixerGroup;
    //                 audioSource.PlayOneShot(absorptionSound);
    //                 Debug.Log("playing sfx");
    //             }
    //             Destroy(other.gameObject);
    //         }

    //         if (other.CompareTag("Gum") && !hasCollided)
    //         {
    //             hasCollided = true;
    //             trashCounter.setGum();

    //             if (absorptionSound != null)
    //             {
    //                 audioSource.outputAudioMixerGroup = mixerGroup;
    //                 audioSource.PlayOneShot(absorptionSound);
    //                 Debug.Log("playing sfx");
    //             }
    //             Destroy(other.gameObject);
    //         }

    //         if (other.CompareTag("Cup") && !hasCollided)
    //         {
    //             hasCollided = true;
    //             trashCounter.setCup();

    //             if (absorptionSound != null)
    //             {
    //                 audioSource.outputAudioMixerGroup = mixerGroup;
    //                 audioSource.PlayOneShot(absorptionSound);
    //                 Debug.Log("playing sfx");
    //             }
    //             Destroy(other.gameObject);
    //         }

    //         if (other.CompareTag("Paper") && !hasCollided)
    //         {
    //             hasCollided = true;
    //             trashCounter.setPaper();

    //             if (absorptionSound != null)
    //             {
    //                 audioSource.outputAudioMixerGroup = mixerGroup;
    //                 audioSource.PlayOneShot(absorptionSound);
    //                 Debug.Log("playing sfx");
    //             }
    //                 Destroy(other.gameObject);
    //         }
    //     } else {
    //         Debug.Log("Slime player is missing reference to Trash Counter on TrashAbsorb.cs!");
    //     }
        
    // } 

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Banana") || other.CompareTag("Gum") || other.CompareTag("Cup") || other.CompareTag("Paper")) {
            hasCollided = false;
        }
    }
}
