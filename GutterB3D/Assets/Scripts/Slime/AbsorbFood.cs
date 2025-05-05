using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class AbsorbFood : MonoBehaviour
{
    public float sizeIncrease = 1.1f;
    public Transform centerPnt;
    public AudioClip absorptionSound;
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    // public List<GameObject> myFood = new List<GameObject>();
    // int myFoodIndex = 0;
    
    // [System.Serializable]
    // public class TrashPrefabMapping
    // {
    //     public string tag;
    //     public GameObject prefab;
    // }

    // public List<TrashPrefabMapping> trashPrefabs;

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
    string incomingTag = other.tag;
    if (incomingTag == "Food") // fallback case
    {
        transform.localScale *= sizeIncrease;
        if (absorptionSound != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.PlayOneShot(absorptionSound);
        }
        Debug.Log("HEREEEEEE");
        Destroy(other.gameObject);
    }
}

// public class AbsorbFood : MonoBehaviour
// {
//     public float sizeIncrease = 1.1f;
//     public Transform centerPnt;
//     public AudioClip absorptionSound;
//     private AudioSource audioSource;
//     public AudioMixerGroup mixerGroup;

//     public List<GameObject> myFood = new List<GameObject>();
//     // int myFoodIndex = 0;
    
//     [System.Serializable]
//     public class TrashPrefabMapping
//     {
//         public string tag;
//         public GameObject prefab;
//     }

//     public List<TrashPrefabMapping> trashPrefabs;

//         void Start()
//         {
//             // Try to get an AudioSource; if none exists, add one.
//             audioSource = GetComponent<AudioSource>();
//             if (audioSource == null)
//             {
//                 audioSource = gameObject.AddComponent<AudioSource>();
//             }
//         }

//     void FixedUpdate(){
//         centerPnt.Rotate (new Vector3 (0, 30, 0) * Time.fixedDeltaTime);
//     }
// void OnTriggerEnter(Collider other)
// {
//     string incomingTag = other.tag;
//     GameObject prefabToSpawn = null;

//     // 1) Find the matching prefab (manual loop)
//     for (int i = 0; i < trashPrefabs.Count; i++)
//     {
//         if (trashPrefabs[i].tag == incomingTag)
//         {
//             prefabToSpawn = trashPrefabs[i].prefab;
//             Debug.Log("Matched tag: " + incomingTag);
//             break;
//         }
//     }

//     if (prefabToSpawn != null)
//     {
//         // 2) Grow & speed up
//         transform.localScale *= sizeIncrease;
//         GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

//         // 3) Instantiate inside slime
//         GameObject newTrash = Instantiate(prefabToSpawn, centerPnt);
//         FoodPosition(newTrash.transform);
//         newTrash.GetComponent<PickupUpDown>().SinWaveMove = true;

//         // 4) EXTRA MANUAL CHECK: only add if its tag is in trashPrefabs
//         bool tagAllowed = false;
//         for (int i = 0; i < trashPrefabs.Count; i++)
//         {
//             if (trashPrefabs[i].tag == newTrash.tag)
//             {
//                 tagAllowed = true;
//                 break;
//             }
//         }

//         if (tagAllowed)
//         {
//             Debug.Log("Adding to myFood list: " + newTrash.name);
//             myFood.Add(newTrash);
//             FoodSizer();
//         }
//         else
//         {
//             Debug.LogWarning($"Refusing to add '{newTrash.tag}' to myFood: tag not in trashPrefabs");
//             Destroy(newTrash);
//         }

//         // 5) Mark the original as absorbed
//         MoveFood foodMovement = other.GetComponent<MoveFood>();
//         if (foodMovement != null) foodMovement.isAbsorbed = true;

//         return;
//     }
//     else if (incomingTag == "Food") // fallback case
//     {
//         if (absorptionSound != null)
//         {
//             audioSource.outputAudioMixerGroup = mixerGroup;
//             audioSource.PlayOneShot(absorptionSound);
//         }
//         Debug.Log("HEREEEEEE");
//         Destroy(other.gameObject);
//     }
// }

// void OnTriggerEnter(Collider other)
// {
//     string tag = other.tag;

//     // find a matching prefab
//     var mapping = trashPrefabs.FirstOrDefault(m => m.tag == tag);

//     if (mapping != null)
//     {
//         // Grow and speed up
//         transform.localScale *= sizeIncrease;
//         GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

//         // Instantiate new version inside slime
//         GameObject newTrash = Instantiate(mapping.prefab, centerPnt);
//         FoodPosition(newTrash.transform);
//         newTrash.GetComponent<PickupUpDown>().SinWaveMove = true;

//         // *** EXTRA CHECK: only add if its tag is in trashPrefabs ***
//         if (trashPrefabs.Any(m => m.tag == newTrash.tag))
//         {
//             Debug.Log("Adding to trash list: " + newTrash);
//             myFood.Add(newTrash);
//             FoodSizer();
//         }
//         else
//         {
//             Debug.LogWarning($"Refusing to add '{newTrash.tag}' to myFood: tag not in trashPrefabs");
//             Destroy(newTrash);
//         }

//         // Mark original as absorbed (optional, or destroy it)
//         if (other.TryGetComponent<MoveFood>(out var foodMovement))
//             foodMovement.isAbsorbed = true;

//         return;
//     }
//     else if (tag == "Food") // fallback case
//     {
//         if (absorptionSound != null)
//         {
//             audioSource.outputAudioMixerGroup = mixerGroup;
//             audioSource.PlayOneShot(absorptionSound);
//         }
//         Debug.Log("HEREEEEEE");
//         Destroy(other.gameObject);
//     }
// }

    // void OnTriggerEnter(Collider other)
    // {
    //     string tag = other.tag;

    //     GameObject prefabToSpawn = null;
    //     foreach (var mapping in trashPrefabs)
    //     {
    //         if (mapping.tag == tag)
    //         {
    //             prefabToSpawn = mapping.prefab;
    //             Debug.Log("Matched tag: " + tag);
    //             break; // No need to continue if a match is found
    //         }
    //     }


    //     if (prefabToSpawn != null)
    //     {
    //         // Grow and speed up
    //         transform.localScale *= sizeIncrease;
    //         GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

    //         // Instantiate new version inside slime
    //         GameObject newTrash = Instantiate(prefabToSpawn, centerPnt);
    //         FoodPosition(newTrash.transform);
    //         newTrash.GetComponent<PickupUpDown>().SinWaveMove = true;
    //         Debug.Log("Adding to trash list: " + newTrash);
    //         Debug.Log("how many food in my list:" + myFood.Count);
    //         myFood.Add(newTrash);
    //         // myFoodIndex++;
    //         FoodSizer();


    //         // Mark original as absorbed (optional, or destroy it)
    //         MoveFood foodMovement = other.GetComponent<MoveFood>();
    //         if (foodMovement != null) foodMovement.isAbsorbed = true;
    //         return;
    //     }
    //     else if (tag == "Food") // fallback case
    //     {
    //         if (absorptionSound != null)
    //         {
    //             audioSource.outputAudioMixerGroup = mixerGroup;
    //             audioSource.PlayOneShot(absorptionSound);
    //         }
    //         Debug.Log("HEREEEEEE");
    //         Destroy(other.gameObject);
    //     }
    // }
    // void FoodSizer(){
    //     float slimeSize = transform.localScale.x; // assuming uniform scale
    //     float foodSizeFactor = 0.03f; 

    //     Vector3 targetScale = Vector3.one * (slimeSize * foodSizeFactor);

    //     for (int i = 0; i < myFood.Count; i++) {
    //         if (myFood[i] != null) {
    //             SetGlobalScale(myFood[i].transform, targetScale);
    //             Debug.Log("Scaling in FoodSizer:" + myFood[i]);
    //         }
    //     }
    // }



    // void SetGlobalScale(Transform obj, Vector3 targetScale)
    // {
    //     obj.localScale = Vector3.one;
    //     Vector3 parentScale = obj.parent != null ? obj.parent.lossyScale : Vector3.one;
    //     obj.localScale = new Vector3(
    //         targetScale.x / parentScale.x,
    //         targetScale.y / parentScale.y,
    //         targetScale.z / parentScale.z
    //     );
    // }

    // void FoodPosition(Transform food){
    //     float radius = Mathf.Min(this.transform.localScale.x * 0.05f, 0.3f);
    //     Vector3 offset = Random.insideUnitSphere * radius;

    //     food.localPosition = offset;
    // }
}


