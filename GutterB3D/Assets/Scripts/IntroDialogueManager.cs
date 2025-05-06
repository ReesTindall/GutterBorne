using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueManager : MonoBehaviour
{
    [Header("UI References")]    
    public GameObject introBox;                  // The UI panel containing dialogue
    public Text introText;                       // The legacy UI Text component
    public Text enterIndicator;                  // The "Press Enter >" text
    public Image dialogueImage;                  // UI Image for dialogue-related pictures

    [Header("Dialogue Content")]
    [TextArea(2, 5)]
    public string[] dialogueLines;               // Array of dialogue lines
    public Sprite[] dialogueSprites;             // Array of sprites corresponding to each line

    [Header("Optional Objects")]
    public GameObject objectToDestroy;

    [Header("Timings")]
    public float textSpeed = 0.05f;              // Speed of the typewriter effect
    public float fadeDuration = 0.5f;            // Duration of fade-in effect

    private int currentLine = 0;
    private bool isTyping = false;
    private bool textFullyDisplayed = false;
    private bool hasTriggered = false;           // Ensure dialogue shows only once

    void Start()
    {
        // Hide dialogue UI at start
        introBox.SetActive(false);
        if (dialogueImage != null)
            dialogueImage.gameObject.SetActive(false);
        enterIndicator.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only trigger once
        if (hasTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartDialogue();

            // Optionally disable collider if present
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;
        }
    }

    /// <summary>
    /// Public method to kick off the dialogue sequence.
    /// </summary>
    public void StartDialogue()
    {
        if (hasTriggered && introBox.activeSelf)
            return;

        currentLine = 0;
        introBox.SetActive(true);
        if (dialogueImage != null && dialogueSprites != null && dialogueSprites.Length > 0)
        {
            dialogueImage.sprite = dialogueSprites[0];
            dialogueImage.gameObject.SetActive(true);
        }
        enterIndicator.gameObject.SetActive(false);

        StartCoroutine(TypeText(dialogueLines[currentLine]));
    }

    void Update()
    {
        if (introBox.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                introText.text = dialogueLines[currentLine];
                isTyping = false;
                textFullyDisplayed = true;
                StartCoroutine(FadeInEnterIndicator());
            }
            else if (textFullyDisplayed)
            {
                NextDialogue();
            }
        }
    }

    void NextDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            if (dialogueImage != null && dialogueSprites != null && dialogueSprites.Length > currentLine)
            {
                dialogueImage.sprite = dialogueSprites[currentLine];
            }

            enterIndicator.gameObject.SetActive(false);
            StartCoroutine(TypeText(dialogueLines[currentLine]));
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        textFullyDisplayed = false;
        introText.text = string.Empty;
        enterIndicator.gameObject.SetActive(false);

        foreach (char letter in text)
        {
            introText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        textFullyDisplayed = true;
        StartCoroutine(FadeInEnterIndicator());
    }

    IEnumerator FadeInEnterIndicator()
    {
        enterIndicator.gameObject.SetActive(true);
        Color c = enterIndicator.color;
        c.a = 0f;
        enterIndicator.color = c;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            enterIndicator.color = c;
            yield return null;
        }

        c.a = 1f;
        enterIndicator.color = c;
    }

    void EndDialogue()
    {
        // Simply hide the dialogue UI so gameplay continues
        introBox.SetActive(false);
        if (dialogueImage != null)
            dialogueImage.gameObject.SetActive(false);
        enterIndicator.gameObject.SetActive(false);

        // Reset typing flags
        isTyping = false;
        textFullyDisplayed = false;
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}


// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class IntroDialogueManager : MonoBehaviour
// {
//     public GameObject introBox;                // The UI panel containing dialogue
//     public Text introText;                     // The legacy UI Text component
//     public Text enterIndicator;                // The "Press Enter >" text
//     public Image dialogueImage;                // UI Image for dialogue-related pictures

//     [TextArea(2, 5)]
//     public string[] dialogueLines;             // Array of dialogue lines
//     public Sprite[] dialogueSprites;           // Array of sprites corresponding to each line

//     private int currentLine = 0;
//     private bool isTyping = false;
//     private bool textFullyDisplayed = false;

//     public float textSpeed = 0.05f;            // Speed of the typewriter effect
//     public float fadeDuration = 0.5f;          // Duration of fade-in effect

//     void Start()
//     {
//         introBox.SetActive(true);               // Show dialogue box at start
//         enterIndicator.gameObject.SetActive(false); // Hide indicator initially

//         // If we have sprites set, show the first one
//         if (dialogueImage != null && dialogueSprites.Length > 0)
//         {
//             dialogueImage.sprite = dialogueSprites[0];
//             dialogueImage.gameObject.SetActive(true);
//         }

//         StartCoroutine(TypeText(dialogueLines[currentLine])); // Start first dialogue
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Return))
//         {
//             if (isTyping)
//             {
//                 StopAllCoroutines();
//                 introText.text = dialogueLines[currentLine]; // Instantly display full line
//                 isTyping = false;
//                 textFullyDisplayed = true;
//                 StartCoroutine(FadeInEnterIndicator());   // Show enter text with fade-in
//             }
//             else if (textFullyDisplayed)
//             {
//                 NextDialogue();
//             }
//         }
//     }

//     void NextDialogue()
//     {
//         currentLine++;
//         if (currentLine < dialogueLines.Length)
//         {
//             // Update the dialogue image if we have a sprite for this line
//             if (dialogueImage != null && dialogueSprites.Length > currentLine)
//             {
//                 dialogueImage.sprite = dialogueSprites[currentLine];
//             }
            
//             enterIndicator.gameObject.SetActive(false); // Hide indicator while new text loads
//             StartCoroutine(TypeText(dialogueLines[currentLine]));
//         }
//         else
//         {
//             EndDialogue();
//         }
//     }

//     IEnumerator TypeText(string text)
//     {
//         isTyping = true;
//         textFullyDisplayed = false;
//         introText.text = "";                         // Clear text
//         enterIndicator.gameObject.SetActive(false);    // Hide indicator while typing

//         foreach (char letter in text)
//         {
//             introText.text += letter;
//             yield return new WaitForSeconds(textSpeed); // Simulate typing effect
//         }

//         isTyping = false;
//         textFullyDisplayed = true;
//         StartCoroutine(FadeInEnterIndicator());        // Show enter text with fade-in
//     }

//     IEnumerator FadeInEnterIndicator()
//     {
//         enterIndicator.gameObject.SetActive(true);
//         enterIndicator.color = new Color(
//             enterIndicator.color.r,
//             enterIndicator.color.g,
//             enterIndicator.color.b,
//             0f);  // Set alpha to 0

//         float elapsedTime = 0f;
//         while (elapsedTime < fadeDuration)
//         {
//             elapsedTime += Time.deltaTime;
//             float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
//             enterIndicator.color = new Color(
//                 enterIndicator.color.r,
//                 enterIndicator.color.g,
//                 enterIndicator.color.b,
//                 alpha);
//             yield return null;
//         }

//         enterIndicator.color = new Color(
//             enterIndicator.color.r,
//             enterIndicator.color.g,
//             enterIndicator.color.b,
//             1f); // Fully visible
//     }

//     void EndDialogue()
//     {
//         introBox.SetActive(false);                  // Hide the dialogue box
//         SceneManager.LoadScene("Level1");          // Load the first level
//     }
// }

