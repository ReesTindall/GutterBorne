using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialTriggers : MonoBehaviour
{
    // [TextArea]
    // public string tutorialMessage;

    public GameObject tutorialCanvas; // The Canvas in your scene, set inactive by default
    // public Text messageText;          // The legacy UI Text component on that Canvas

    private void Start() {
        tutorialCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialCanvas.SetActive(true);
            // messageText.text = tutorialMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialCanvas.SetActive(false);
        }
    }
}


// public class tutorialTriggers : MonoBehaviour
// {
//     [TextArea]
//     public string tutorialMessage;
//     public Transform playerCamera;

//     public GameObject popupPrefab;

//     private GameObject currentPopup;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player") && currentPopup == null)
//         {
//             Vector3 popupPosition = transform.position + Vector3.up * 2;

//             // currentPopup = Instantiate(popupPrefab);

//             currentPopup = Instantiate(popupPrefab, popupPosition, Quaternion.identity);
//             tutorialPopups popupScript = currentPopup.GetComponent<tutorialPopups>();
//             popupScript.SetMessage(tutorialMessage);
//             popupScript.playerCamera = playerCamera;
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player") && currentPopup)
//         {
//             Destroy(currentPopup);
//         }
//     }
// }
