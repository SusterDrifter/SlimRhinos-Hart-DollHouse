using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UIFader), typeof(CanvasGroup))]
public class WhiteScreenUI : MonoBehaviour {

    [SerializeField] private UIFader fader;
    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private float endAlpha = 0.2f;

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
