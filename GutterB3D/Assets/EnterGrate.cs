using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGrate : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Entrance")) {
            int nextSceneIdx = SceneManager.GetActiveScene().buildIndex - 1;
            SceneManager.LoadScene("Level " + nextSceneIdx);
        }
    }
}
