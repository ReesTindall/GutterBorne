using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CollisionCanvasController : MonoBehaviour
{
    public Canvas collisionCanvas; // Reference to the canvas
    // public Camera mainCamera; // Reference to the camera
    // public float canvasDistance = 5f; // Distance in front of the camera
    public float fadeDuration = 2f; // Duration to fade the canvas out

    private CanvasGroup canvasGroup; // To control the canvas' fade effect

    void Start()
    {
        // Initially hide the canvas
        if (collisionCanvas != null)
        {
            collisionCanvas.gameObject.SetActive(false);
        }

        // Get the CanvasGroup component (for fading purposes)
        canvasGroup = collisionCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // If CanvasGroup doesn't exist, add one
            canvasGroup = collisionCanvas.gameObject.AddComponent<CanvasGroup>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ShowCanvas();
    }

    void ShowCanvas()
    {
        if (collisionCanvas != null)
        {
            // Make the canvas appear
            collisionCanvas.gameObject.SetActive(true);

            // Start the fading out process
            StartCoroutine(FadeOutCanvas());
        }
    }

    IEnumerator FadeOutCanvas()
    {
        // Fade in (set alpha to 1)
        canvasGroup.alpha = 1f;

        // Wait for the specified duration (2 seconds)
        yield return new WaitForSeconds(2f);

        // Fade out (set alpha to 0) over the specified duration
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is 0 and hide the canvas
        canvasGroup.alpha = 0f;
        collisionCanvas.gameObject.SetActive(false);
    }
}
