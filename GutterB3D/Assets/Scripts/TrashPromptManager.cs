using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrashPromptManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text mainText;
    public Text pressEIndicator;

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
        // Setup
        mainText.text = "";
        SetAlpha(pressEIndicator, 0f);
        SetAlpha(mainText, 0f);
    }

    public void ShowPrompt()
    {
        if (!hasPressedE && !promptShown)
        {
            promptShown = true;
            SetAlpha(mainText, 100f);
            mainText.text = ""; // clear in case it's not
            StartCoroutine(TypeText(promptMessage));
        }
    }

    public void HidePrompt()
    {
        if (!hasPressedE)
        {
            StopAllCoroutines();
            mainText.text = "";
            SetAlpha(pressEIndicator, 0f);
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

    IEnumerator FadeOutPrompt()
    {
        float t = 0f;
        Color cMain = mainText.color;
        Color cE = pressEIndicator.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            SetAlpha(mainText, alpha);
            SetAlpha(pressEIndicator, alpha);
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
}
