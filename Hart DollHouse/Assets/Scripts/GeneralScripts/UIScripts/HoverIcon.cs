using UnityEngine;
using UnityEngine.UI;

public class HoverIcon : MonoBehaviour {

    [SerializeField] private Sprite defaultCrosshair;
    [SerializeField] private Sprite interactCrosshair;
    [SerializeField] private Image crosshair;

    #region Singleton
    public static HoverIcon instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        crosshair = GetComponent<Image>();
        crosshair.sprite = defaultCrosshair;
    }
    #endregion

    public void InteractCrosshair()
    {
        crosshair.sprite = interactCrosshair;
    }

    public void DefaultCrossHair()
    {
        crosshair.sprite = defaultCrosshair;
    }
}
