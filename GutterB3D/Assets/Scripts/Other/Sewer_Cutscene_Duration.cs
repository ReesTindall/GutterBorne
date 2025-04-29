using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] float cutsceneDuration = 6f;
    [SerializeField] string nextLevelName = "Level 2";  // Replace with actual level name or use index

    void Start()
    {
        StartCoroutine(WaitAndLoadNextLevel());
    }

    IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(cutsceneDuration);
        SceneManager.LoadScene(nextLevelName);
    }
}