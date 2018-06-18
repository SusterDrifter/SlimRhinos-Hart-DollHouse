using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemDesc;
    [SerializeField] private CanvasGroup UIElement;

    private Sound itemUIOpen;
    private Sound itemUIClose;

    public void Awake()
    {
        UIElement = GetComponent<CanvasGroup>();
        UIElement.alpha = 0;

        itemDesc = GetComponentInChildren<Text>();
        itemIcon = GetComponentInChildren<Image>();

    }

    public void ActivateUI() {
        UIElement.alpha = 1;
        CameraPPSControl.instance.BlurVignetteUIActivate();
        if (itemUIOpen == null)
            itemUIOpen = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "ItemUIOpen");
        AudioManager.instance.PlayClip(itemUIOpen);
    }

    public void DeactivateUI() {
        CameraPPSControl.instance.BlurVignetteUIDeactivate();
        UIElement.alpha = 0;
        if (itemUIClose == null)
            itemUIClose = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "ItemUIClose");
        AudioManager.instance.PlayClip(itemUIClose);
        itemDesc.text = "";
    }

    public void SetIcon(Sprite icon) {
        itemIcon.sprite = icon;
    }

    public void SetDesc(List<string> desc) {
        string text = "";
        foreach(string line in desc)
        {
            text += line;
        }
        itemDesc.text = text;
    }
}
