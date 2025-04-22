using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class tutorialPopups : MonoBehaviour
{
    public Text messageText;
    // public TextMeshProUGUI messageText;
    // public Transform playerCamera;

    public void SetMessage(string message)
    {
        messageText.text = message;
    }

    // void Update()
    // {
    //     if (playerCamera)
    //     {
    //         // Always face the camera
    //         transform.LookAt(playerCamera);
    //         transform.Rotate(0, 180, 0); // To flip if it's backward
    //     }
    // }
}
