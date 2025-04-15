using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class ObstacleSFX : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;
    public AudioClip sfx1; //"eeek"
    public AudioClip sfx2; //"aaahh"

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
        //send output to global audio mixer
            audioSource.outputAudioMixerGroup = mixerGroup;

             if(other.gameObject.tag == "Obstacle") {
                if(sfx1 != null) {
                    audioSource.PlayOneShot(sfx1);
                }
             }

             if(other.gameObject.tag == "Enemy") {
               if(sfx2 != null) {
                    audioSource.PlayOneShot(sfx2);
               }
             }
       }
}