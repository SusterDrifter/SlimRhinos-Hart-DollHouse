using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup), typeof(UIFader), typeof(Timer))]
public class NotificationUIManager : MonoBehaviour {

    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private UIFader fader;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI notifText;
    [SerializeField] private float fadeDuration = 0.1f;
    [SerializeField] private float visibleDuration = 4f;

    private bool isActive = false;

    private void Awake()
    {
        UIElement = GetComponent<CanvasGroup>();
        fader = GetComponent<UIFader>();
        timer = GetComponent<Timer>();
        notifText = GetComponentInChildren<TextMeshProUGUI>();

        UIElement.alpha = 0;
        timer.SetDuration(visibleDuration);
    }

    private void Update()
    {
        if (isActive && timer.HasRunOut())
        {
            fader.FadeOut(UIElement, fadeDuration * 3);
            isActive = false;
        }
    }
    public void ShowNotification(string text)
    {
        StopAllCoroutines();
        notifText.SetText(text);
        fader.FadeIn(UIElement, fadeDuration);
        timer.RestartTimer();
        isActive = true;
        return;
    }
}
