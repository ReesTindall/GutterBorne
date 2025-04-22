using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialColliders : MonoBehaviour
{
    [System.Serializable]
    public class ColliderCanvas
    {
        [Tooltip("Tag of the triggering object")]
        public string colliderTag;

        [Tooltip("Canvas to display when that tag is triggered")]
        public Canvas canvas;

        [HideInInspector] public CanvasGroup canvasGroup;
    }

    [Tooltip("One entry per trigger‑tag → canvas")]
    public List<ColliderCanvas> colliderCanvases = new List<ColliderCanvas>();

    [Tooltip("How long the popup stays fully visible (seconds)")]
    public float displayDuration = 2f;
    [Tooltip("How long it takes to fade out (seconds)")]
    public float fadeDuration = 2f;

    void Start()
    {
        // For each mapping, ensure the Canvas has a CanvasGroup & start hidden
        foreach (var pair in colliderCanvases)
        {
            if (pair.canvas == null)
                continue;

            // Try to get the CanvasGroup…
            CanvasGroup cg = pair.canvas.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                // …if missing, add it
                cg = pair.canvas.gameObject.AddComponent<CanvasGroup>();
            }

            // Cache it
            pair.canvasGroup = cg;

            // Start fully transparent and inactive
            cg.alpha = 0f;
            pair.canvas.gameObject.SetActive(false);
        }
    }
void OnTriggerEnter(Collider other)
{
    Debug.Log("Triggered by: " + other.name);  // check

    foreach (var pair in colliderCanvases)
    {
        if (pair.canvas == null) continue;

        if (other.CompareTag(pair.colliderTag))
        {
            Debug.Log("Matched tag: " + pair.colliderTag); // Optional extra check
            StartCoroutine(ShowThenFade(pair));
            break;
        }
    }
}


    IEnumerator ShowThenFade(ColliderCanvas pair)
    {
        // Activate & show
        pair.canvas.gameObject.SetActive(true);
        pair.canvasGroup.alpha = 1f;

        // Hold full opacity
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        float elapsed = 0f, startAlpha = pair.canvasGroup.alpha;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            pair.canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        // Hide
        pair.canvasGroup.alpha = 0f;
        pair.canvas.gameObject.SetActive(false);
    }
}
