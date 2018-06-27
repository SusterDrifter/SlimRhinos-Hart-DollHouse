using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private Button continueButton;
    [SerializeField] private Button sceneSelectButton;
    private Sound button;

    private void Awake()
    {
        int playerProgress = GameManager.instance.currentSceneIndex;
        if (playerProgress <= 1)
        {
            continueButton.interactable = false;
            sceneSelectButton.interactable = false;
        }
    }

    public void PlayGame()
    {
        GameManager.instance.NextChapter();
    }

    public void Continue()
    {
        SceneManager.LoadScene(GameManager.instance.currentSceneIndex);
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
