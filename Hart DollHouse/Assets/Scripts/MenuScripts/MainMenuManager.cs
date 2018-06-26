using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private Sound button;

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
