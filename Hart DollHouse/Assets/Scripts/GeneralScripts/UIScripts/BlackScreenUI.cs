using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class BlackScreenUI : MonoBehaviour {

    [SerializeField] public const float fadeDuration = 0.2f;
    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private UIFader fader;
    
    private void Awake()
    {
        UIElement = GetComponent<CanvasGroup>();
        UIElement.alpha = 0;
        fader = GetComponent<UIFader>();
    }

    public void ChangeAlpha(float alpha)
    {
        UIElement.alpha = alpha;
    }

    public void BlackFadeIn(float duration = 0.5f)
    {
        StopAllCoroutines();
        fader.FadeIn(UIElement, duration);
    }

    public void BlackFadeOut(float duration = 0.5f)
    {
        StopAllCoroutines();
        fader.FadeOut(UIElement, duration);
    }

    public void BlackFadeInOut(float waitDur = 1f, float fadeDur = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(BlackFadeInFadeOut(waitDur, fadeDur));
    }

    public void BlackFadeOutDelayBy(float fadeoutDuration = 0.1f, float duration = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(DelayBlackFadeOut(fadeoutDuration, duration));
    }

    IEnumerator BlackFadeInFadeOut(float waitDur, float fadeDur)
    {
        yield return StartCoroutine(fader.FadeUI(UIElement, UIElement.alpha, 1f, fadeDur));
        yield return new WaitForSecondsRealtime(waitDur - fadeDur);
        StartCoroutine(fader.FadeUI(UIElement, UIElement.alpha, 0f, fadeDur));
    }

    IEnumerator DelayBlackFadeOut(float fadeoutDuration = 0.1f, float duration = 1f)
    {
        yield return new WaitForSecondsRealtime(duration);
        StartCoroutine(fader.FadeUI(UIElement, UIElement.alpha, 0f, fadeoutDuration));
    }
}
