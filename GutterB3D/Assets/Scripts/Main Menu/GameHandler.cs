using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public void StartGame() {
            SceneManager.LoadScene("Rees Work");
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
