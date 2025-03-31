using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class AbsorbFood : MonoBehaviour
{
    public float sizeIncrease = 1.1f;
    public Transform centerPnt;

    public AudioClip absorptionSound;
    private AudioSource audioSource;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (absorptionSound != null)
            {
                audioSource.PlayOneShot(absorptionSound);
            }

            transform.localScale *= sizeIncrease;
            GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

            other.gameObject.transform.position = centerPnt.position;
            other.gameObject.transform.parent = centerPnt;
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
                1 / blobDivider.x,
                1 / blobDivider.y,
                1 / blobDivider.z);
        }
    }

}