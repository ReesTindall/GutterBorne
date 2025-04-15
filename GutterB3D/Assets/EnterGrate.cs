using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGrate : MonoBehaviour
{
    [SerializeField] DivineSlimeCounter counter;

    void OnTriggerEnter(Collider other) {
        float divineRatio = counter.currDivineSlime / counter.totalDivineSlime;

        if(other.CompareTag("Entrance") && divineRatio == 1f) {
            int nextSceneIdx = SceneManager.GetActiveScene().buildIndex - 1;
            SceneManager.LoadScene("Level " + nextSceneIdx);
        }
    }
}
