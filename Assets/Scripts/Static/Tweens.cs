using System.Collections;
using TMPro;
using UnityEngine;

public static class Tweens
{
    /// <summary>
    /// Fade y position and alpha
    /// </summary>
    public static IEnumerator FadeTextYPositionAndAlpha(TextMeshProUGUI text, float duration, float speed, System.Action onComplete)
    {
        float elapsedTime = 0f;
        float threeFourthDuration = 3 * duration / 4;
        float initialAlpha = text.alpha;
        float finalAlpha = 0f;

        while (elapsedTime < duration)
        {
            if (elapsedTime >= threeFourthDuration)
            {
                text.alpha = Mathf.Lerp(initialAlpha, finalAlpha, elapsedTime / duration);
            }

            text.rectTransform.anchoredPosition += new Vector2(0f, speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.alpha = finalAlpha;

        onComplete?.Invoke();
    }

    /// <summary>
    /// Bounce rect scale in and out
    /// </summary>
    public static IEnumerator BounceInOut(RectTransform rect, float strength, float duration)
    {
        float halfDuration = duration * 0.5f;
        Vector2 initialScale = rect.localScale;
        Vector2 targetScale = new Vector2(initialScale.x - strength, initialScale.y - strength);

        float elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            rect.localScale = Vector2.Lerp(rect.localScale, targetScale, elapsedTime / halfDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rect.localScale = targetScale;

        elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            rect.localScale = Vector2.Lerp(rect.localScale, initialScale, elapsedTime / halfDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rect.localScale = initialScale;
    }
}
