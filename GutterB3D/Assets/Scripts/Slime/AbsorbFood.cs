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
    
    [System.Serializable]
    public class TrashPrefabMapping
    {
        public string tag;
        public GameObject prefab;
    }

    public List<TrashPrefabMapping> trashPrefabs;

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
    string tag = other.tag;

    GameObject prefabToSpawn = null;
    foreach (var mapping in trashPrefabs)
    {
        if (mapping.tag == tag)
        {
            prefabToSpawn = mapping.prefab;
            break;
        }
    }

    if (prefabToSpawn != null)
    {
        // Play absorption sound
        if (absorptionSound != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.PlayOneShot(absorptionSound);
        }

        // Grow and speed up
        transform.localScale *= sizeIncrease;
        GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

        // Instantiate new version inside slime
        GameObject newTrash = Instantiate(prefabToSpawn, centerPnt);
        FoodPosition(newTrash.transform);
        newTrash.GetComponent<PickupUpDown>().SinWaveMove = true;

        myFood.Add(newTrash);
        myFoodIndex++;
        FoodSizer();

        // Mark original as absorbed (optional, or destroy it)
        MoveFood foodMovement = other.GetComponent<MoveFood>();
        if (foodMovement != null) foodMovement.isAbsorbed = true;

        Destroy(other.gameObject); // Clean up absorbed object
    }
    else if (tag == "Food") // fallback case
    {
        if (absorptionSound != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.PlayOneShot(absorptionSound);
        }

        Destroy(other.gameObject);
    }
}

    // void OnTriggerEnter(Collider other)
    // {
    //     // if (other.CompareTag("Food"))
    //     if (other.CompareTag("Cup") || other.CompareTag("Paper") || other.CompareTag("Gum") || other.CompareTag("Banana"))
    //     {
    //         other.gameObject.transform.position = centerPnt.position;
    //         // FoodPosition(other.gameObject.transform);
    //         other.gameObject.transform.parent = centerPnt;
    //         other.gameObject.GetComponent<PickupUpDown>().SinWaveMove=true;
    //         myFood.Add(other.gameObject);
    //         myFoodIndex ++;
    //         FoodSizer();
    //         //Destroy(other.gameObject);
    //         if (absorptionSound != null)
    //         {
    //             audioSource.outputAudioMixerGroup = mixerGroup;
    //             audioSource.PlayOneShot(absorptionSound);
    //         }

    //         transform.localScale *= sizeIncrease;
    //         GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

    //         MoveFood foodMovement = other.gameObject.GetComponent<MoveFood>();
    //         if (foodMovement != null)
    //         {
    //             foodMovement.isAbsorbed = true;
    //         }

    //     } else if (other.CompareTag("Food")) {
    //         if (absorptionSound != null)
    //         {
    //             audioSource.outputAudioMixerGroup = mixerGroup;
    //             audioSource.PlayOneShot(absorptionSound);
    //         }
    //         Destroy(other.gameObject);
    //     }
    // }

    void FoodSizer(){
        Vector3 blobDivider = this.gameObject.transform.localScale; 
        for (int i = 0; i < myFood.Count; i++){
            myFood[i].transform.localScale = new Vector3(
                0.5f / blobDivider.x,
                0.5f / blobDivider.y,
                0.5f / blobDivider.z);
        }
    }

    // void FoodPosition(Transform food){
    //     float blobBellySize = this.gameObject.transform.localScale.x / 4;
    //     float myNewPos = Random.Range(blobBellySize * (-1), blobBellySize); 
    //     food.position = new Vector3(
    //         centerPnt.position.x + myNewPos,
    //         centerPnt.position.y + myNewPos,
    //         centerPnt.position.z + myNewPos
    //         );
    // }
    void FoodPosition(Transform food){
        float radius = Mathf.Min(this.transform.localScale.x * 0.05f, 0.3f);
        Vector3 offset = Random.insideUnitSphere * radius;

        food.localPosition = offset;
    }


}


