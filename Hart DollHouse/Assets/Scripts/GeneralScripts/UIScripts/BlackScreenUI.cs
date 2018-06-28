using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BlackScreenUI : MonoBehaviour {

    [SerializeField] public const float fadeDuration = 0.2f;
    [SerializeField] private CanvasGroup UIElement;

    private void Awake()
    {
        UIElement = GetComponent<CanvasGroup>();
        UIElement.alpha = 0;
    }

    public void ChangeAlpha(float alpha)
    {
        UIElement.alpha = alpha;
    }
}
