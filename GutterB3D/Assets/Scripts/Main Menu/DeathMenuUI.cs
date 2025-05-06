using UnityEngine;

public class DeathMenuUI : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f; // Just in case the game was paused
    }
}