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

    [Header("Target Area")]
    public Transform centerTarget;
    public float targetSpread = 2f;

    [Header("UI Prompt")]
    public GameObject promptUI; // Set a UI panel or text in the scene

    private bool playerInZone = false;
    private bool hasExpelled = false;
    private Transform slimeTransform;

    void Update()
    {
        if (playerInZone && !hasExpelled && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SpewTrash());
            hasExpelled = true;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }

    private IEnumerator SpewTrash()
    {
        for (int i = 0; i < trashCount; i++)
        {
            if (trashPrefabs.Count == 0 || slimeTransform == null || centerTarget == null)
                yield break;

            GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Count)];
            GameObject trash = Instantiate(prefab, slimeTransform.position, Quaternion.identity);

            Vector3 randomTarget = centerTarget.position + new Vector3(
                Random.Range(-targetSpread, targetSpread),
                0f,
                Random.Range(-targetSpread, targetSpread)
            );

            StartCoroutine(ArcMove(trash, slimeTransform.position, randomTarget, arcHeight));

            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator ArcMove(GameObject obj, Vector3 start, Vector3 end, float height)
    {
        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            Vector3 midPoint = Vector3.Lerp(start, end, t);
            midPoint.y += Mathf.Sin(t * Mathf.PI) * height;

            obj.transform.position = midPoint;
            time += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = end;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            slimeTransform = other.transform;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }
}
