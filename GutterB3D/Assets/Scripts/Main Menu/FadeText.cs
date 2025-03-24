using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FadeTextOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Graphic uiText;
    public float fadeDuration = 0.5f;
    private Color originalColor;
    private Coroutine fadeCoroutine;

    void Start()
    {
        if (uiText != null)
            originalColor = uiText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToAlpha(0.3f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToAlpha(1f));
    }

    private System.Collections.IEnumerator FadeToAlpha(float targetAlpha)
    {
        float startAlpha = uiText.color.a;
        float time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}