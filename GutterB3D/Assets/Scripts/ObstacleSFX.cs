using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleSFX : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sfx1; //"bleh" "eeek"
    public AudioClip sfx2; 

    void Start()
    {
        // Try to get an AudioSource; if none exists, add one.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other){

             if(other.gameObject.tag == "StationaryObstacle") {
                if(sfx1 != null) {
                    audioSource.PlayOneShot(sfx1);
                }
             }

            //  if(other.gameObject.tag == "Human") {
            //    if(sfx2 != null) {
            //         audioSource.PlayOneShot(sfx2);
            //    }
            //  }
       }
}