using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Drag your IntroDialogueManager GameObject here.")]
    public IntroDialogueManager dialogueManager;

    void Reset()
    {
        // Ensure this collider is a trigger by default
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!dialogueManager) return;
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue();

            // Optional: disable this trigger so it only fires once
            GetComponent<Collider>().enabled = false;
        }
    }
}
