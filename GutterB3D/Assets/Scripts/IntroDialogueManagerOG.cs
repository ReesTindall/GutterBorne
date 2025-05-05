using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroDialogueManagerOG : MonoBehaviour
{
    public GameObject introBox;                // The UI panel containing dialogue
    public Text introText;                     // The legacy UI Text component
    public Text enterIndicator;                // The "Press Enter >" text
    public Image dialogueImage;                // UI Image for dialogue-related pictures

    [TextArea(2, 5)]
    public string[] dialogueLines;             // Array of dialogue lines
    public Sprite[] dialogueSprites;           // Array of sprites corresponding to each line

    private int currentLine = 0;
    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    public float textSpeed = 0.05f;            // Speed of the typewriter effect
    public float fadeDuration = 0.5f;          // Duration of fade-in effect

    void Start()
    {
        introBox.SetActive(true);               // Show dialogue box at start
        enterIndicator.gameObject.SetActive(false); // Hide indicator initially

        // If we have sprites set, show the first one
        if (dialogueImage != null && dialogueSprites.Length > 0)
        {
            dialogueImage.sprite = dialogueSprites[0];
            dialogueImage.gameObject.SetActive(true);
        }

        StartCoroutine(TypeText(dialogueLines[currentLine])); // Start first dialogue
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                introText.text = dialogueLines[currentLine]; // Instantly display full line
                isTyping = false;
                textFullyDisplayed = true;
                StartCoroutine(FadeInEnterIndicator());   // Show enter text with fade-in
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
            // Update the dialogue image if we have a sprite for this line
            if (dialogueImage != null && dialogueSprites.Length > currentLine)
            {
                dialogueImage.sprite = dialogueSprites[currentLine];
            }
            
            enterIndicator.gameObject.SetActive(false); // Hide indicator while new text loads
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
        introText.text = "";                         // Clear text
        enterIndicator.gameObject.SetActive(false);    // Hide indicator while typing

        foreach (char letter in text)
        {
            introText.text += letter;
            yield return new WaitForSeconds(textSpeed); // Simulate typing effect
        }

        isTyping = false;
        textFullyDisplayed = true;
        StartCoroutine(FadeInEnterIndicator());        // Show enter text with fade-in
    }

    IEnumerator FadeInEnterIndicator()
    {
        enterIndicator.gameObject.SetActive(true);
        enterIndicator.color = new Color(
            enterIndicator.color.r,
            enterIndicator.color.g,
            enterIndicator.color.b,
            0f);  // Set alpha to 0

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            enterIndicator.color = new Color(
                enterIndicator.color.r,
                enterIndicator.color.g,
                enterIndicator.color.b,
                alpha);
            yield return null;
        }

        enterIndicator.color = new Color(
            enterIndicator.color.r,
            enterIndicator.color.g,
            enterIndicator.color.b,
            1f); // Fully visible
    }

    void EndDialogue()
    {
        introBox.SetActive(false);                  
        SceneManager.LoadScene("Level1");          
    }
}

