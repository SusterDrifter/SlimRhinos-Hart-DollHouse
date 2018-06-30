using UnityEngine;

public class BreathingSystemUIController : MonoBehaviour {

    public bool curCycleFinished = false;

    [SerializeField] BreathingCircBarUIHandle handle;
    [SerializeField] UIFader fader;
    [SerializeField] CanvasGroup UIElement;
    
	void Start () {
        fader = GetComponent<UIFader>();
        UIElement = GetComponent<CanvasGroup>();
        handle = GetComponentInChildren<BreathingCircBarUIHandle>();
        UIElement.alpha = 0;
        UIElement.interactable = false;
        UIElement.blocksRaycasts = false;
    }

    private void Update()
    {
        if (handle.hasFinished)
        {
            SoftDisable();
        }    
    }

    public void BeginBreathingSystem()
    {
        curCycleFinished = false;
        handle.HardcoreMode();
        UIElement.blocksRaycasts = true;
        fader.FadeIn(UIElement, 0.5f);
        handle.StartBreathingSystem();
    }

    public void BeginTutorial()
    {
        curCycleFinished = false;
        handle.BeginnerMode();
        UIElement.blocksRaycasts = true;
        fader.FadeIn(UIElement, 0.5f);
        handle.StartBreathingSystem();
    }

    public void SoftDisable()
    {
        Debug.Log("SOFT DISABLE");
        UIElement.alpha = 0;
        UIElement.interactable = false;
        UIElement.blocksRaycasts = false;
        curCycleFinished = true;
        MainUIManager.instance.GetBlackScreen().ChangeAlpha(0);
    }
}
