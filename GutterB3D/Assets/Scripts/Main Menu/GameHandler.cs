using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
      public static GameHandler handler;

      public float slimeSize = 1f;
      public float maxSize = 100f;

      //checks to make sure there is only one game handler in scene
      void Awake() {
            if(handler == null) {
                  handler = this;
                  DontDestroyOnLoad(gameObject);
            } else {
                  Destroy(gameObject);
            }
      }

      public void StartGame() {
            SceneManager.LoadScene("Level 1 - Sewer");
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
}
