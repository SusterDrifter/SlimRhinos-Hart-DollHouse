using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIFader), typeof(CanvasGroup))]

public abstract class BarUIController : MonoBehaviour {

    [SerializeField] public Slider slider;
    [SerializeField] private UIFader fader;
    [SerializeField] private CanvasGroup UIElement;

    void Start () {
        UIElement = GetComponent<CanvasGroup>();
        fader = GetComponent<UIFader>();
        slider = GetComponentInChildren<Slider>();
        UIElement.alpha = 0;
    }

    public Slider GetSlider() {
        return slider;
    }

    public void FadeTiming () {
        if (slider.value < slider.maxValue && UIElement.alpha != 1)
        {
            fader.FadeIn(UIElement, 2f);
        }
        else if (slider.value == slider.maxValue && UIElement.alpha == 1)
        {
            fader.FadeOut(UIElement, 2f);
        }
    }
}
