using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArticlePopup : MonoBehaviour {

    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private TextMeshProUGUI textDesc;

	// Use this for initialization
	void Start () {
        UIElement = GetComponent<CanvasGroup>();
        textDesc = GetComponentInChildren<TextMeshProUGUI>();
        UIElement.alpha = 0;
        textDesc.SetText("");
	}
	
    public void ActivateDesc() { UIElement.alpha = 1; }
    public void ResetDesc() { textDesc.text = ""; }
    public void DeactivateDesc() { UIElement.alpha = 0; }

	public void SetDesc(List<string> description)
    {
        string desc = "";

        foreach (string sentence in description)
            desc += sentence;

        textDesc.SetText(desc);
    }

}
