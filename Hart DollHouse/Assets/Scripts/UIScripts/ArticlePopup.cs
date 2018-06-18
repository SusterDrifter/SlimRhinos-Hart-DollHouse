using UnityEngine;
using UnityEngine.UI;

public class ArticlePopup : MonoBehaviour {

    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private Text textDesc;

	// Use this for initialization
	void Start () {
        UIElement = GetComponent<CanvasGroup>();
        textDesc = GetComponentInChildren<Text>();
        UIElement.alpha = 0;
        textDesc.text = "";
	}
	
    public void ActivateDesc() { UIElement.alpha = 1; }
    public void ResetDesc() { textDesc.text = ""; }
    public void DeactivateDesc() { UIElement.alpha = 0; }

	public void SetDesc(Dialogue dialogue)
    {
        string desc = "";

        foreach (string sentence in dialogue.sentences)
            desc += sentence;

        textDesc.text = desc;
    }

}
