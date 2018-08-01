using UnityEngine;

public class BreathingSystemUIController : MonoBehaviour {

    public bool curCycleFinished = false;

    private Sound music;
    private Sound anyaPanting;

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

        if (anyaPanting == null)
            anyaPanting = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "AnyaPA");

        AudioManager.instance.PlayClip(music);
        AudioManager.instance.PlayClip(anyaPanting);
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
        UIElement.alpha = 0;
        UIElement.interactable = false;
        UIElement.blocksRaycasts = false;
        curCycleFinished = true;
        MainUIManager.instance.GetBlackScreen().ChangeAlpha(0);

        if (music == null)
            music = AudioManager.instance.GetSound(Sound.SoundType.BackgroundMusic, "PanicAttackMusic");

        if (music.source.isPlaying)
            AudioManager.instance.audioFader.FadeOut(music, 5f);

        if (anyaPanting == null)
            anyaPanting = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "AnyaPA");

        if (anyaPanting.source.isPlaying)
            AudioManager.instance.audioFader.FadeOut(anyaPanting, 3f);
    }
}
