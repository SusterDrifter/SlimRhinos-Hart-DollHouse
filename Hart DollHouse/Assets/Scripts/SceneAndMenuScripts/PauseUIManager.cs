using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour {

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    public static bool isPaused = false;
    public static float playerVolume = 0;
    private Sound button;
    private Sound UIActive = null;
    private Sound UIDeactivate = null;

    #region Singleton
    public static PauseUIManager instance;

    void Awake () {

        if (instance != null)
            return;

        instance = this;

        PauseUIManager.instance.gameObject.SetActive(false);
    }
    #endregion

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        if (GameManager.instance.currentSceneIndex > 1)
        GameManager.instance.UnlockCursor();

        if (UIActive == null)
            UIActive = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "PauseActive");

        AudioManager.instance.PlayClip(UIActive);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (GameManager.instance.currentSceneIndex > 1)
            GameManager.instance.LockCursor();

        if (UIDeactivate == null)
            UIDeactivate = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "PauseDeactivate");

        AudioManager.instance.PlayClip(UIDeactivate);
        PauseUIManager.instance.gameObject.SetActive(false);
    }

    public void GoMenu()
    {
        UnpauseGame();
        GameManager.instance.LoadMenu();
        GameManager.instance.UnlockCursor();
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
        playerVolume = vol;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(GameManager.instance.VolumeStr, playerVolume);
        
    }

    public void LoadSettings() {
        float vol = PlayerPrefs.GetFloat(GameManager.instance.VolumeStr);
        SetVolume(vol);
        volumeSlider.value = vol;
    }
}
