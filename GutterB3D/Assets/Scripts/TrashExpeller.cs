using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashExpeller : MonoBehaviour
{
    [Header("Trash Settings")]
    public List<GameObject> trashPrefabs;
    public int trashCount = 10;
    public float interval = 0.1f;
    public float arcHeight = 3f;
    public float trashArcTime = 2f;

    [Header("Target Area")]
    public Transform centerTarget;
    public float targetSpread = 2f;

    [Header("UI Prompt Manager Object")]
    public TrashPromptManager promptManager;

    [Header("Jump Prompt Canvas")]
    public GameObject jumpCanvas;    // assign a Canvas (with "Jump in!" text) here

    [Header("Cleanup After Expel")]
    public GameObject objectToDestroyAfterExpel; // assign any GameObject you want removed

    private bool playerInZone = false;
    private bool hasExpelled = false;
    private Transform slimeTransform;

    void Start()
    {
        // Hide jump prompt at start
        if (jumpCanvas != null)
            jumpCanvas.SetActive(false);
    }

    void Update()
    {
        if (playerInZone && !hasExpelled && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SpewTrash());
            hasExpelled = true;

            if (promptManager != null)
                promptManager.ConfirmExpel();
        }
    }

    private IEnumerator SpewTrash()
    {
        // Optional: start slime movement tween
        Slime_Scale scaler = slimeTransform?.GetComponent<Slime_Scale>();
        if (scaler != null)
            scaler.moveTweenOn = true;

        // Spawn & arc‑move each trash piece
        for (int i = 0; i < trashCount; i++)
        {
            if (trashPrefabs.Count == 0 || slimeTransform == null || centerTarget == null)
                break;

            GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Count)];
            GameObject trash = Instantiate(prefab, slimeTransform.position, Quaternion.identity);

            Vector3 randomTarget = centerTarget.position + new Vector3(
                Random.Range(-targetSpread, targetSpread),
                0f,
                Random.Range(-targetSpread, targetSpread)
            );

            StartCoroutine(ArcMoveAndDestroy(trash, slimeTransform.position, randomTarget, arcHeight, trashArcTime));

            yield return new WaitForSeconds(interval);
        }

        // Stop slime tween after a brief pause
        if (slimeTransform != null)
        {
            Slime_Scale scaler2 = slimeTransform.GetComponent<Slime_Scale>();
            if (scaler2 != null)
            {
                yield return new WaitForSeconds(0.5f);
                scaler2.moveTweenOn = false;
            }
        }

        // Destroy the assigned object now that all trash is out
        if (objectToDestroyAfterExpel != null)
        {
            Destroy(objectToDestroyAfterExpel);
        }

        // Finally, show the "Jump in!" prompt
        if (jumpCanvas != null)
            StartCoroutine(ShowJumpPrompt());
    }

    private IEnumerator ArcMoveAndDestroy(GameObject obj, Vector3 start, Vector3 end, float height, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            Vector3 midPoint = Vector3.Lerp(start, end, t);
            midPoint.y += Mathf.Sin(t * Mathf.PI) * height;

            if (obj != null)
                obj.transform.position = midPoint;

            time += Time.deltaTime;
            yield return null;
        }

        if (obj != null)
            obj.transform.position = end;

        Destroy(obj);
    }

    private IEnumerator ShowJumpPrompt()
    {
        jumpCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        jumpCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            slimeTransform = other.transform;

            if (promptManager != null)
                promptManager.ShowPrompt();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;

            if (promptManager != null)
                promptManager.HidePrompt();
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class TrashExpeller : MonoBehaviour
// {
//     [Header("Trash Settings")]
//     public List<GameObject> trashPrefabs;
//     public int trashCount = 10;
//     public float interval = 0.1f;
//     public float arcHeight = 3f;
//     public float trashArcTime = 2f;

//     [Header("Target Area")]
//     public Transform centerTarget;
//     public float targetSpread = 2f;

//     [Header("UI Prompt Manager Object")]
//     public TrashPromptManager promptManager;

//     [Header("Jump Prompt Canvas")]
//     public GameObject jumpCanvas;    // assign a Canvas (with a Text saying "Jump in!") here

//     private bool playerInZone = false;
//     private bool hasExpelled = false;
//     private Transform slimeTransform;

//     void Start()
//     {
//         // Make sure the jump prompt is hidden at start
//         if (jumpCanvas != null)
//             jumpCanvas.SetActive(false);
//     }

//     void Update()
//     {
//         if (playerInZone && !hasExpelled && Input.GetKeyDown(KeyCode.E))
//         {
//             StartCoroutine(SpewTrash());
//             hasExpelled = true;

//             if (promptManager != null)
//                 promptManager.ConfirmExpel();
//         }
//     }

//     private IEnumerator SpewTrash()
//     {
//         // Optional: kick off slime movement
//         Slime_Scale scaler = slimeTransform?.GetComponent<Slime_Scale>();
//         if (scaler != null)
//             scaler.moveTweenOn = true;

//         for (int i = 0; i < trashCount; i++)
//         {
//             if (trashPrefabs.Count == 0 || slimeTransform == null || centerTarget == null)
//                 break;

//             // spawn
//             GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Count)];
//             GameObject trash = Instantiate(prefab, slimeTransform.position, Quaternion.identity);

//             // target
//             Vector3 randomTarget = centerTarget.position + new Vector3(
//                 Random.Range(-targetSpread, targetSpread),
//                 0f,
//                 Random.Range(-targetSpread, targetSpread)
//             );

//             // move + destroy when it lands
//             StartCoroutine(ArcMoveAndDestroy(trash, slimeTransform.position, randomTarget, arcHeight, trashArcTime));

//             yield return new WaitForSeconds(interval);
//         }

//         // stop slime movement tween
//         if (scaler != null)
//         {
//             yield return new WaitForSeconds(0.5f);
//             scaler.moveTweenOn = false;
//         }

//         // after all trash is out, show the "Jump in!" canvas
//         if (jumpCanvas != null)
//             StartCoroutine(ShowJumpPrompt());
//     }

//     // arcs the object along a parabola, then destroys it
//     private IEnumerator ArcMoveAndDestroy(GameObject obj, Vector3 start, Vector3 end, float height, float duration)
//     {
//         float time = 0f;

//         while (time < duration)
//         {
//             float t = time / duration;
//             Vector3 midPoint = Vector3.Lerp(start, end, t);
//             midPoint.y += Mathf.Sin(t * Mathf.PI) * height;

//             if (obj != null)
//                 obj.transform.position = midPoint;

//             time += Time.deltaTime;
//             yield return null;
//         }

//         if (obj != null)
//             obj.transform.position = end;

//         // destroy this trash piece
//         Destroy(obj);
//     }

//     // shows the jump prompt for exactly 1 second
//     private IEnumerator ShowJumpPrompt()
//     {
//         jumpCanvas.SetActive(true);
//         yield return new WaitForSeconds(2f);
//         jumpCanvas.SetActive(false);
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerInZone = true;
//             slimeTransform = other.transform;

//             if (promptManager != null)
//                 promptManager.ShowPrompt();
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerInZone = false;

//             if (promptManager != null)
//                 promptManager.HidePrompt();
//         }
//     }
// }

