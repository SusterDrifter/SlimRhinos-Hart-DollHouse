using UnityEngine;
using TMPro;

public class DialogueUIManager : MonoBehaviour {

    [SerializeField] private DialogueManager manager;
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private CanvasGroup UIElement;

    [SerializeField] private string currCharName;

	void Start () {
        manager = GetComponentInChildren<DialogueManager>();
        charName = GetComponentInChildren<TextMeshProUGUI>();
        UIElement = GetComponent<CanvasGroup>();

        currCharName = PlayerPresets.instance.GetPreset().charName;

        UIElement.alpha = 0;
        ResetName();
	}
	
    public void ResetName()
    {
        currCharName = PlayerPresets.instance.GetPreset().charName;
        charName.SetText(currCharName);
    }
    
    public void ResetName(string newName)
    {
        charName.SetText(newName);
    }

    public void Activate()
    {
        UIElement.alpha = 1;
    }

    public void Deactivate()
    {
        UIElement.alpha = 0;
    }

    public DialogueManager GetManager()
    {
        return manager;
    }
}
