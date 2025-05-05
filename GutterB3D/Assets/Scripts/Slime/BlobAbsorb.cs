using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlobAbsorb : MonoBehaviour
{
    public float sizeIncrease = 1.1f;
    public Transform centerPnt;
    public AudioClip absorptionSound;
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;
    
    public List<GameObject> myFood = new List<GameObject>();

    [System.Serializable]
    public class TrashPrefabMapping
    {
        public string tag;
        public GameObject prefab;
    }

    public List<TrashPrefabMapping> trashPrefabs;

    [SerializeField] TrashCounter trashCounter;

    private bool hasCollided = false; // Prevent multiple collisions with the same object

    void Start()
    {
        // Try to get an AudioSource; if none exists, add one.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void FixedUpdate()
    {
        centerPnt.Rotate(new Vector3(0, 30, 0) * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        // Skip if this object has already been absorbed by TrashAbsorb
        if (other.gameObject.GetComponent<TrashAbsorb>() != null) return;

        // Check if the object matches any tag in trashPrefabs
        GameObject prefabToSpawn = null;
        foreach (var mapping in trashPrefabs)
        {
            if (mapping.tag == tag)
            {
                prefabToSpawn = mapping.prefab;
                break; // No need to continue if a match is found
            }
        }

        // Handle food and trash absorption
        if (prefabToSpawn != null) // Food objects with matching tags in trashPrefabs
        {
            AbsorbFood(other, prefabToSpawn);
        }
        else if (tag == "Food") // Fallback for generic food objects
        {
            AbsorbGenericFood(other);
        }
        else // Handle trash objects specifically
        {
            HandleTrash(other);
        }
    }

    void AbsorbFood(Collider other, GameObject prefabToSpawn)
    {
        // Grow and speed up the blob
        transform.localScale *= sizeIncrease;
        GetComponent<BlobMovementWorldSpace>().moveSpeed += 0.1f;

        // Instantiate new version of the object inside slime
        GameObject newTrash = Instantiate(prefabToSpawn, centerPnt);
        FoodPosition(newTrash.transform);
        newTrash.GetComponent<PickupUpDown>().SinWaveMove = true;

        // Add the new object to the list
        myFood.Add(newTrash);

        // Resize the new food object
        FoodSizer();

        // Mark the original object as absorbed and destroy it
        MoveFood foodMovement = other.GetComponent<MoveFood>();
        if (foodMovement != null) foodMovement.isAbsorbed = true;
        Destroy(other.gameObject);
    }

    void AbsorbGenericFood(Collider other)
    {
        // Play absorption sound and destroy the object
        if (absorptionSound != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.PlayOneShot(absorptionSound);
        }
        Destroy(other.gameObject);
    }

    void HandleTrash(Collider other)
    {
        if (trashCounter != null && !hasCollided)
        {
            hasCollided = true;

            if (other.CompareTag("Banana"))
            {
                trashCounter.setBanana();
            }
            else if (other.CompareTag("Gum"))
            {
                trashCounter.setGum();
            }
            else if (other.CompareTag("Cup"))
            {
                trashCounter.setCup();
            }
            else if (other.CompareTag("Paper"))
            {
                trashCounter.setPaper();
            }

            // Play absorption sound
            if (absorptionSound != null)
            {
                audioSource.outputAudioMixerGroup = mixerGroup;
                audioSource.PlayOneShot(absorptionSound);
            }

            // Destroy the object after handling
            Destroy(other.gameObject);
        }
    }

    void FoodSizer()
    {
        float slimeSize = transform.localScale.x;
        float foodSizeFactor = 0.03f;

        Vector3 targetScale = Vector3.one * (slimeSize * foodSizeFactor);

        for (int i = 0; i < myFood.Count; i++)
        {
            if (myFood[i] != null)
            {
                SetGlobalScale(myFood[i].transform, targetScale);
            }
        }
    }

    void SetGlobalScale(Transform obj, Vector3 targetScale)
    {
        obj.localScale = Vector3.one;
        Vector3 parentScale = obj.parent != null ? obj.parent.lossyScale : Vector3.one;
        obj.localScale = new Vector3(
            targetScale.x / parentScale.x,
            targetScale.y / parentScale.y,
            targetScale.z / parentScale.z
        );
    }

    void FoodPosition(Transform food)
    {
        float radius = Mathf.Min(this.transform.localScale.x * 0.05f, 0.3f);
        Vector3 offset = Random.insideUnitSphere * radius;

        food.localPosition = offset;
    }

    // Reset hasCollided when the object exits the trigger (optional if you want the player to collide again)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Banana") || other.CompareTag("Gum") || other.CompareTag("Cup") || other.CompareTag("Paper"))
        {
            hasCollided = false;
        }
    }
}
