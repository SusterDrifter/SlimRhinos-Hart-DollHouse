using UnityEngine;

public class BlackScreenUI : MonoBehaviour {

    [SerializeField] CanvasGroup UIElement;

    private void Start()
    {
        UIElement = GetComponent<CanvasGroup>();
    }

    public void ChangeAlpha(float alpha)
    {
        UIElement.alpha = alpha;
    }
}
