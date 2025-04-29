using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnterGrate : MonoBehaviour
{
    [SerializeField] TrashCounter counter;
    [SerializeField] string cutsceneSceneName = "Sewer_Cutscene1";

    private bool hasEntered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasEntered) return;

        if (counter != null)
        {
            float bananaRatio = counter.currBanana / counter.totalBanana;
            float gumRatio = counter.currGum / counter.totalGum;
            float cupRatio = counter.currCup / counter.totalCup;
            float paperRatio = counter.currPaper / counter.totalPaper;

            float trashRatio = bananaRatio / gumRatio / cupRatio / paperRatio;

            if (other.CompareTag("Entrance") && trashRatio == 1f)
            {
                hasEntered = true;
                SceneManager.LoadScene(cutsceneSceneName);
            }
        }
        else
        {
            Debug.Log("Slime player is missing reference to Trash Counter on EnterGrate.cs!");
        }
    }
}