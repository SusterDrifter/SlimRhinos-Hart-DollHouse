using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UIFader), typeof(CanvasGroup))]
public class WhiteScreenUI : MonoBehaviour {

    [SerializeField] private UIFader fader;
    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private float flashDuration = 0.3f;
    [SerializeField] private float endAlpha = 0.1f;

    private void Start()
    {
        fader = GetComponent<UIFader>();
        UIElement = GetComponent<CanvasGroup>();
        UIElement.alpha = 0;
    }

    public void FlashingEffect()
    {
        fader.FlashingEffect(UIElement, endAlpha, flashDuration);
    }
}
