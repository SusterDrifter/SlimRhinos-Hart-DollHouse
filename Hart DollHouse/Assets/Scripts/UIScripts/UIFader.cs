using System.Collections;
using UnityEngine;

public class UIFader : MonoBehaviour {
    
    public void FadeIn(CanvasGroup UIElement, float duration = 0.5f) {
        StartCoroutine(FadeUI(UIElement, UIElement.alpha, 1, duration));        
    }
    
    public void FadeOut(CanvasGroup UIElement, float duration = 0.5f) {
        StartCoroutine(FadeUI(UIElement, UIElement.alpha, 0, duration));
    }

    public void FlashingEffect(CanvasGroup UIElement, float endingAlpha, float duration = 0.2f)
    {
        StartCoroutine(Flash(UIElement, endingAlpha, duration));
    }

    public void UITransitionSequentially(CanvasGroup OldUI, CanvasGroup NewUI, float duration)
    {
        StartCoroutine(FadeInNewUISequentially(OldUI, NewUI, duration));
    }

    public void UITransitionAsync(CanvasGroup OldUI, CanvasGroup NewUI, float duration)
    {
        StartCoroutine(FadeUI(OldUI, OldUI.alpha, 0, duration / 2));
        StartCoroutine(FadeUI(NewUI, NewUI.alpha, 1, duration / 2));
    }

    public IEnumerator FadeUI(CanvasGroup UI, float startAlpha, float endAlpha, float duration = 0.5f) {

        float startTime = Time.time;
        float timeSinceStarted = Time.time - startTime;
        float percentageComplete = timeSinceStarted / duration;

        while (percentageComplete < 1) {
            timeSinceStarted = Time.time - startTime;
            percentageComplete = timeSinceStarted / duration;

            float curAlpha = Mathf.Lerp(startAlpha, endAlpha, percentageComplete);

            UI.alpha = curAlpha;
            yield return null;
        }
    }

    private IEnumerator Flash(CanvasGroup UIElement, float endingAlpha, float duration)
    {
        yield return StartCoroutine(FadeUI(UIElement, UIElement.alpha, endingAlpha, duration / 2));
        StartCoroutine(FadeUI(UIElement, UIElement.alpha, 0f, duration / 2));
    }

    private IEnumerator FadeInNewUISequentially(CanvasGroup OldUI, CanvasGroup NewUI, float duration)
    {
        yield return StartCoroutine(FadeUI(OldUI, OldUI.alpha, 0, duration / 2));
        StartCoroutine(FadeUI(NewUI, NewUI.alpha, 1, duration / 2));
    }
}
