using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

	//public static GameHandler handler;

      public static float slimeSize = 1f;
      public float maxSize = 39f;
      private static float prevLevelSlimeSize = 1f;
      private SlimeSizeBar bar;

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
                  prevLevelSlimeSize = slimeSize; //save slime size before new level starts
            Debug.Log("Level before switch: " + levelNumber);
            GameObject.FindWithTag("Canvas").GetComponent<Cutscene_Controller>().nextLevelName = "Level" + levelNumber.ToString();
		}
	}

      public void StartGame() {
            slimeSize = 1f; // for if you play again in same game
            levelNumber = 1;
            SceneManager.LoadScene("IntroFamilyDeath");
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

    //public void Retry() {
    //      Time.timeScale = 1f;
    //      Debug.Log("Before slime size change:" + slimeSize);
    //      slimeSize = prevLevelSlimeSize; //load prev slime size
    //      bar.SetSize(0f); //updates UI
    //      Debug.Log("After slime size change:" + slimeSize);
    //      SceneManager.LoadScene("Level" + levelNumber.ToString());
    //}

    public void Retry()
    {
        Time.timeScale = 1f;
        Debug.Log("Before slime size change:" + slimeSize);
        slimeSize = prevLevelSlimeSize;

        // Find bar dynamically if it hasn't been assigned
        if (bar == null)
        {
            bar = FindObjectOfType<SlimeSizeBar>();
        }

        if (bar != null)
        {
            bar.SetSize(0f); //updates UI
        }
        else
        {
            Debug.LogWarning("SlimeSizeBar not found in scene!");
        }

        Debug.Log("After slime size change:" + slimeSize);

        Debug.Log("Level before switch: " + levelNumber);
        SceneManager.LoadScene("Level" + levelNumber.ToString());
    }
}
