using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGrate : MonoBehaviour
{
    [SerializeField] TrashCounter counter;

    void OnTriggerEnter(Collider other) {
        if(counter != null) {
            float bananaRatio = counter.currBanana / counter.totalBanana;
            float gumRatio = counter.currGum / counter.totalGum;
            float cupRatio = counter.currCup / counter.totalCup;
            float paperRatio = counter.currPaper / counter.totalPaper;

            float trashRatio = bananaRatio / gumRatio / cupRatio / paperRatio;

            if(other.CompareTag("Entrance") && trashRatio == 1f) {
                int nextSceneIdx = SceneManager.GetActiveScene().buildIndex - 1;
                SceneManager.LoadScene("Level " + nextSceneIdx);
        }
        } else {
            Debug.Log("Slime player is missing reference to Trash Counter on EnterGrate.cs!");
        }
        
    }
}
