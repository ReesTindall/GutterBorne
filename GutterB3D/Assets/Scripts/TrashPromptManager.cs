using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashPromptManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text mainText;
    public Text pressEIndicator;
    public Image dialogueImage;  // Image to fade in during text typing

    [TextArea(2, 5)]
    public string promptMessage = "Welcome Young Slimey One, do you have anything to offer me?";

    [Header("Timings")]
    public float textSpeed = 0.05f;
    public float fadeInDelay = 1.5f;
    public float fadeDuration = 0.5f;

    private bool hasPressedE = false;
    private bool promptShown = false;

    void Start()
    {
        mainText.text = "";
        SetAlpha(mainText, 0f);
        SetAlpha(pressEIndicator, 0f);
        if (dialogueImage != null)
            SetAlpha(dialogueImage, 0f);
    }

    public void ShowPrompt()
    {
        if (!hasPressedE && !promptShown)
        {
            promptShown = true;
            mainText.text = "";
            StartCoroutine(TypeText(promptMessage));
            StartCoroutine(FadeInImage());
        }
    }

    public void HidePrompt()
    {
        if (!hasPressedE)
        {
            StopAllCoroutines();
            mainText.text = "";
            SetAlpha(mainText, 0f);
            SetAlpha(pressEIndicator, 0f);
            if (dialogueImage != null)
                SetAlpha(dialogueImage, 0f);
            promptShown = false;
        }
    }

    public void ConfirmExpel()
    {
        if (!hasPressedE)
        {
            hasPressedE = true;
            StopAllCoroutines();
            StartCoroutine(FadeOutPrompt());
        }
    }

    IEnumerator TypeText(string message)
    {
        SetAlpha(mainText, 1f);

        foreach (char c in message)
        {
            mainText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(fadeInDelay);
        StartCoroutine(FadeInEIndicator());
    }

    IEnumerator FadeInEIndicator()
    {
        pressEIndicator.gameObject.SetActive(true);
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            SetAlpha(pressEIndicator, alpha);
            yield return null;
        }
    }

    IEnumerator FadeInImage()
    {
        if (dialogueImage == null) yield break;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / (fadeDuration));
            SetAlpha(dialogueImage, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOutPrompt()
    {
        float t = 0f;
        Color cMain = mainText.color;
        Color cE = pressEIndicator.color;
        Color cImg = dialogueImage != null ? dialogueImage.color : Color.clear;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            SetAlpha(mainText, alpha);
            SetAlpha(pressEIndicator, alpha);
            if (dialogueImage != null)
                SetAlpha(dialogueImage, alpha);
            yield return null;
        }

        mainText.text = "";
        pressEIndicator.text = "";
    }

    void SetAlpha(Text txt, float alpha)
    {
        if (txt == null) return;
        Color c = txt.color;
        c.a = alpha;
        txt.color = c;
    }

    void SetAlpha(Image img, float alpha)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
