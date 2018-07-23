using UnityEngine;

public class BreathingSystemUIController : MonoBehaviour {

    public bool curCycleFinished = false;

    private Sound music;

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
            curCycleFinished = true;
            SoftDisable();
        }    
    }

    public void BeginBreathingSystem()
    {
        curCycleFinished = false;
        handle.HardcoreMode();
        UIElement.blocksRaycasts = true;

        if (music == null)
            music = AudioManager.instance.GetSound(Sound.SoundType.BackgroundMusic, "PanicAttackMusic");

        AudioManager.instance.PlayClip(music);
        fader.FadeIn(UIElement, 0.5f);
        handle.StartBreathingSystem();
    }

    public void BeginTutorial()
    {
        curCycleFinished = false;
        handle.BeginnerMode();
        UIElement.blocksRaycasts = true;

        if (music == null)
            music = AudioManager.instance.GetSound(Sound.SoundType.BackgroundMusic, "PanicAttackMusic");

        AudioManager.instance.PlayClip(music);

        fader.FadeIn(UIElement, 0.5f);
        handle.StartBreathingSystem();
    }

    public void SoftDisable()
    {
        UIElement.alpha = 0;
        UIElement.interactable = false;
        UIElement.blocksRaycasts = false;
        curCycleFinished = true;
        AudioManager.instance.audioFader.FadeOut(music);
        MainUIManager.instance.GetBlackScreen().ChangeAlpha(0);
    }
}
