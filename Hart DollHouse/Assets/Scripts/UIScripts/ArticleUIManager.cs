using UnityEngine;
using UnityEngine.UI;

public class ArticleUIManager : MonoBehaviour {

    [SerializeField] private ArticlePopup popup;
    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private Image icon;

    private Sound articleUIOpen;
    private Sound articleUIClose;
    
    // Use this for initialization
    void Start () {
        UIElement = GetComponent<CanvasGroup>();
        popup = GetComponentInChildren<ArticlePopup>();
        icon = GetComponentInChildren<Image>();

        UIElement.alpha = 0;
	}

    public void ActivateUI()
    {
        UIElement.alpha = 1;
        CameraPPSControl.instance.BlurVignetteUIActivate();
        if (articleUIOpen == null)
            articleUIOpen = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "ItemUIOpen");
        AudioManager.instance.PlayClip(articleUIOpen);
    }

    public void DeactivateUI()
    {
        CameraPPSControl.instance.BlurVignetteUIDeactivate();
        UIElement.alpha = 0;
        if (articleUIClose == null)
            articleUIClose = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "ItemUIClose");
        AudioManager.instance.PlayClip(articleUIClose);
        popup.ResetDesc();
    }

    public void SetIcon (Sprite sprite) { icon.sprite = sprite; }
    public void SetDesc (Dialogue dialogue) { popup.SetDesc(dialogue); }
    public void Examine() { popup.ActivateDesc(); }
    public void FinishExamine() { popup.DeactivateDesc(); }

}
