using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleUIManager : MonoBehaviour {

    [SerializeField] private ArticlePopup popup;
    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private Image icon;
    [SerializeField] private Text prompt;

    private Sound articleUIOpen;
    private Sound articleUIClose;
    
    // Use this for initialization
    void Start () {
        UIElement = GetComponent<CanvasGroup>();
        popup = GetComponentInChildren<ArticlePopup>();
        icon = GetComponentInChildren<Image>();
        prompt = GetComponentInChildren<Text>();

        prompt.enabled = false;
        UIElement.alpha = 0;
	}

    public void ActivateUI()
    {
        MainUIManager.instance.isUIActive = true;
        UIElement.alpha = 1;
        CameraPPSControl.instance.BlurVignetteUIActivate();
        if (articleUIOpen == null)
            articleUIOpen = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "ItemUIOpen");
        AudioManager.instance.PlayClip(articleUIOpen);
    }

    public void DeactivateUI()
    {
        MainUIManager.instance.isUIActive = false;
        UIElement.alpha = 0;
        CameraPPSControl.instance.BlurVignetteUIDeactivate();
        if (articleUIClose == null)
            articleUIClose = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "ItemUIClose");
        AudioManager.instance.PlayClip(articleUIClose);
        popup.ResetDesc();
        prompt.enabled = false;
    }

    public void ActivePrompt() { prompt.enabled = true; }
    public void SetIcon (Sprite sprite) { icon.sprite = sprite; }
    public void SetDesc (List<string> desc) { popup.SetDesc(desc); }
    public void Examine() { popup.ActivateDesc(); }
    public void FinishExamine() { popup.DeactivateDesc(); }

}
