using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour {

    private Sound button;
    private Sound backgroundMusic;

    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueText;
    public bool musicPlay = false;

    private void Awake()
    {
        int playerProgress = PlayerPrefs.GetInt(GameManager.instance.PlayerSceneStr);

        if (playerProgress <= 1)
        {
            continueButton.interactable = false;
            continueText.alpha = 0.2f;
        }
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (backgroundMusic == null)
            backgroundMusic = AudioManager.instance.GetSound(Sound.SoundType.BackgroundMusic, "MenuBackgroundMusic");
        backgroundMusic.source.Play();
        musicPlay = true;
    }

    private void StopMusic()
    {
        AudioManager.instance.audioFader.FadeOut(backgroundMusic);
    }

    public void PlayGame()
    {
        GameManager.instance.FadeNextChapter();
        StopMusic();
    }

    public void Continue()
    {
        GameManager.instance.Continue();
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
}
