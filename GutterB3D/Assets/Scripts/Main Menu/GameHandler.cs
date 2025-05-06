using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

	//public static GameHandler handler;

      public static float slimeSize = 1f;
      public float maxSize = 100f;

	//for cutscenes:
	public static int levelNumber = 1;

      //checks to make sure there is only one game handler in scene
	  /*
      void Awake() {
            if(handler == null) {
                  handler = this;
                  DontDestroyOnLoad(gameObject);
            } else {
                  Destroy(gameObject);
            }
      }
	  */

	void Start(){
		//Cutscene stuff to send player to next level:
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Sewer_Cutscene1"){
			levelNumber +=1;
			GameObject.FindWithTag("Canvas").GetComponent<Cutscene_Controller>().nextLevelName = "Level" + levelNumber.ToString();
		}
	}

      public void StartGame() {
            SceneManager.LoadScene("Level1");
      }

      // Return to MainMenu
      public void MainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main_Menu");
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Credits");
      }

      public void Options()
      {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Option_Menu");
      }

      public void Retry() {
            SceneManager.LoadScene("Level" + levelNumber.ToString());
      }
}
