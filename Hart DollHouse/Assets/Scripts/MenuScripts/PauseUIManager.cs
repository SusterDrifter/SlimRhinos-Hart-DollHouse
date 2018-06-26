using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[RequireComponent(typeof(CanvasGroup), typeof(UIFader))]
public class PauseUIManager : MonoBehaviour {

    [SerializeField] private int menuIndex = 0;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private UIFader fader;

    [SerializeField] private float fadeDuration = 0.2f;
    // private bool isActive = false;

    private Sound button;

    void Start () {
        UIElement = GetComponent<CanvasGroup>();
        fader = GetComponent<UIFader>();
	}
	
	void Update () {

	}

    public void ActivateUI()
    {
        StopAllCoroutines();
        fader.FadeIn(UIElement, fadeDuration);
    }

    public void DeactivateUI()
    {
        StopAllCoroutines();
        fader.FadeOut(UIElement, fadeDuration);
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }

    public void UnpauseGame()
    {
        GameManager.instance.ResumeGame();
        DeactivateUI();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(menuIndex);
    }
    
    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }

    public void ButtonSfx()
    {
        if (button == null)
            button = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "Button");
        if (button.source.isPlaying)
            button.source.Stop();

        AudioManager.instance.PlayClip(button);
    }

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);
    }
}
