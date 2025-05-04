using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnterGrate : MonoBehaviour
{
    [SerializeField] TrashCounter counter;
    [SerializeField] string cutsceneSceneName = "Sewer_Cutscene1";

    private bool hasEntered = false;

	void Start(){
		
	}

    void OnCollisionEnter(Collision other){
		Debug.Log("I collided with: " + other.gameObject.name);
		if (other.gameObject.tag == "Entrance"){
        	if (hasEntered) return;

        	if (counter != null){
				int numTotalTrash = 0;
				float bananaRatio = 0;
				float gumRatio = 0;
				float cupRatio = 0;
				float paperRatio = 0;

				if (counter.totalBanana > 0) {
					bananaRatio = counter.currBanana / counter.totalBanana;
					numTotalTrash +=1;
					}
				if (counter.totalGum > 0) {
					gumRatio = counter.currGum / counter.totalGum;
					numTotalTrash +=1;
					}
				if (counter.totalCup > 0) {
					cupRatio = counter.currCup / counter.totalCup;
					numTotalTrash +=1;
					}
				if (counter.totalPaper > 0) {
					paperRatio = counter.currPaper / counter.totalPaper;
					numTotalTrash +=1;
					}

				float trashAmount = bananaRatio + gumRatio + cupRatio + paperRatio;
				trashAmount /= numTotalTrash;
				Debug.Log("trashAmount = " + trashAmount + ", numTotalTrash = " + numTotalTrash);

				if (trashAmount == 1f){
					hasEntered = true;
					SceneManager.LoadScene(cutsceneSceneName);
				}
        	}
			else{
				Debug.Log("Slime player is missing reference to Trash Counter on EnterGrate.cs!");
        	}
		}
    }
}