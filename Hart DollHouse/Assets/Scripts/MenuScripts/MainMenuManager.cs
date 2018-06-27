using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour {

    private Sound button;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button sceneSelectButton;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private TextMeshProUGUI sceneSelectText;

    private void Awake()
    {
        int playerProgress = PlayerPrefs.GetInt(GameManager.instance.PlayerSceneStr);

        if (playerProgress <= 1)
        {
            continueButton.interactable = false;
            sceneSelectButton.interactable = false;
            continueText.alpha = 0.2f;
            sceneSelectText.alpha = 0.2f;
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
