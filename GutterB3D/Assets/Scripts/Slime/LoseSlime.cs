using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSlime : MonoBehaviour
{
    [Tooltip("Amount of scale lost when hit by a rat")]
    public float ratDamage = 0.05f;

    [Tooltip("Amount of scale lost when hit by a person")]
    public float personDamage = 0.5f;

    public float minScale = 0.3f;                 
    public AudioClip absorptionSound;

    private AudioSource audioSrc;
    private BlobMovementPhysics mover;
    private Slime_Scale scaleTweener;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        if (audioSrc == null) audioSrc = gameObject.AddComponent<AudioSource>();

        mover = GetComponent<BlobMovementPhysics>();
        scaleTweener = GetComponent<Slime_Scale>();
    }
    public void TakeHit(Vector3 hitDir, float force, string damageSource)
    {
        if (absorptionSound)
            audioSrc.PlayOneShot(absorptionSound);

        if (mover)
            mover.AddKnockback(hitDir, force);

        Vector3 shrinkAmount = Vector3.zero;

        if (damageSource == "Rat")
        {
            shrinkAmount = Vector3.one * ratDamage;
        }
        else if (damageSource == "Person")
        {
            shrinkAmount = Vector3.one * personDamage;
        }
        else
        {
            Debug.LogWarning($"Unknown damage source: {damageSource}");
        }

        transform.localScale -= shrinkAmount;
        transform.localScale = Vector3.Max(transform.localScale, Vector3.one * minScale);

        if (transform.localScale.x <= minScale)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.25f);    
        SceneManager.LoadScene("Death_Scene");
    }
}




//BEFORE ADDING PEOPLE STUFF: 

// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class LoseSlime : MonoBehaviour
// {
//     [Tooltip("Scale divisor applied per hit ( >1 )")]
//     public float sizeDecrease = 1.1f;
//     public float minScale = 0.3f;                 
//     public AudioClip absorptionSound;

//     AudioSource audioSrc;

//     BlobMovementRelative mover; 

//     void Awake()
//     {
//         audioSrc = GetComponent<AudioSource>();
//         if (audioSrc == null) audioSrc = gameObject.AddComponent<AudioSource>();
//         mover    = GetComponent<BlobMovementRelative>();
//     }

// 	void Start(){
		
// 	}

//     public void TakeHit(Vector3 hitDir, float force)
//     {
//         if (absorptionSound) audioSrc.PlayOneShot(absorptionSound);

//         if (mover)
//             mover.AddKnockback(hitDir, force);

//         transform.localScale /= sizeDecrease;


//         if (transform.localScale.x <= minScale)
//         {
//             StartCoroutine(Die());
//         }
//     }
//     System.Collections.IEnumerator Die()
//     {
//         yield return new WaitForSeconds(0.25f);    
//         SceneManager.LoadScene("Death_Scene");
//     }

//     // void OnTriggerEnter(Collider other)
//     // {
//     //     if (other.CompareTag("Enemy"))
//     //     {
//     //         TakeHit();
//     //     }
//     // }
// }





// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;
// using System;

// public class LoseSlime : MonoBehaviour
// {
//     public float sizeDecrease = 1.1f;
//     public Transform centerPnt;
//     public AudioClip absorptionSound;
//     private AudioSource audioSource;

//     public List<GameObject> myFood = new List<GameObject>();
//     int myFoodIndex = 0;

//     void Start()
//     {
//         // Try to get an AudioSource; if none exists, add one.
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource == null)
//         {
//             audioSource = gameObject.AddComponent<AudioSource>();
//         }
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Enemy"))
//         {
//             if (absorptionSound != null)
//             {
//                 audioSource.PlayOneShot(absorptionSound);
//             }

//             transform.localScale /= sizeDecrease;
//             //GetComponent<BlobMovementWorldSpace>().moveSpeed -= 0.1f;

//             // MoveFood foodMovement = other.gameObject.GetComponent<MoveFood>();
//             // if (foodMovement != null)
//             // {
//             //     foodMovement.isAbsorbed = false;
//             // }


//             //other.gameObject.transform.position = centerPnt.position;
//             //other.gameObject.transform.parent = centerPnt;
//             //myFood.Add(other.gameObject);
//             //myFoodIndex --;
//             //FoodSizer();
//             //Destroy(other.gameObject);
//         }
//     }


//     // void FoodSizer(){
//     //     Vector3 blobDivider = this.gameObject.transform.localScale; 
//     //     for (int i = 0; i < myFood.Count; i++){
//     //         myFood[i].transform.localScale = new Vector3(
//     //             1 / blobDivider.x,
//     //             1 / blobDivider.y,
//     //             1 / blobDivider.z);
//     //     }
//     //}
// }

