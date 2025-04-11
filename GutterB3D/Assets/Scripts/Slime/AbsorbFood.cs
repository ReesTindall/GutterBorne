using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
//using System;

public class AbsorbFood : MonoBehaviour
{
    public float sizeIncrease = 1.1f;
    public Transform centerPnt;
    public AudioClip absorptionSound;
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    public List<GameObject> myFood = new List<GameObject>();
    int myFoodIndex = 0;

    void Start()
    {
        // Try to get an AudioSource; if none exists, add one.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void FixedUpdate(){
        centerPnt.Rotate (new Vector3 (0, 30, 0) * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (absorptionSound != null)
            {
                audioSource.outputAudioMixerGroup = mixerGroup;
                audioSource.PlayOneShot(absorptionSound);
            }

            transform.localScale *= sizeIncrease;
            GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

            MoveFood foodMovement = other.gameObject.GetComponent<MoveFood>();
            if (foodMovement != null)
            {
                foodMovement.isAbsorbed = true;
            }


            other.gameObject.transform.position = centerPnt.position;
            //FoodPosition(other.gameObject.transform);
            other.gameObject.transform.parent = centerPnt;
            other.gameObject.GetComponent<PickupUpDown>().SinWaveMove=true;
            myFood.Add(other.gameObject);
            myFoodIndex ++;
            FoodSizer();
            //Destroy(other.gameObject);
        }
    }

    void FoodSizer(){
        Vector3 blobDivider = this.gameObject.transform.localScale; 
        for (int i = 0; i < myFood.Count; i++){
            myFood[i].transform.localScale = new Vector3(
                0.5f / blobDivider.x,
                0.5f / blobDivider.y,
                0.5f / blobDivider.z);
        }
    }

    void FoodPosition(Transform food){
        float blobBellySize = this.gameObject.transform.localScale.x / 4;
        float myNewPos = Random.Range(blobBellySize * (-1), blobBellySize); 
        food.position = new Vector3(
            centerPnt.position.x + myNewPos,
            centerPnt.position.y + myNewPos,
            centerPnt.position.z + myNewPos
            );
    }

}


