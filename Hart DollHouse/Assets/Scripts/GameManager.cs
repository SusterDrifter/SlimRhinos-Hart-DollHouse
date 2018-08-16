using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum Settings {
        PlayerScene,
        QualitySettings,
        ScreenResolutionIndex,
        Volume,
        FullscreenToggle
    }

    public string PlayerSceneStr = Enum.GetName(typeof(Settings), Settings.PlayerScene);
    public string QualitySettingStr = Enum.GetName(typeof(Settings), Settings.QualitySettings);
    public string ScreenResolutionIndexStr = Enum.GetName(typeof(Settings), Settings.ScreenResolutionIndex);
    public string VolumeStr = Enum.GetName(typeof(Settings), Settings.Volume);
    public string FullscreenToggleStr = Enum.GetName(typeof(Settings), Settings.FullscreenToggle);

    public int currentSceneIndex;
    
    #region Singleton
    public static GameManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    #endregion

    public void Death() {
        // Play some UI
        ResetChapter();
    }   
    
    public void ResetChapter() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextChapter()
    {
        LevelLoaderManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SavePlayerScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeNextChapter()
    {
        LevelLoaderManager.instance.FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
        SavePlayerScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndChapterNextChapter(int chapterNum)
    {
        LevelLoaderManager.instance.EndChapterLoadScene(chapterNum, SceneManager.GetActiveScene().buildIndex + 1);
        SavePlayerScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndChapterFadeNextChapter(int chapterNum)
    {
        LevelLoaderManager.instance.EndChapterFade(chapterNum, SceneManager.GetActiveScene().buildIndex + 1);
        SavePlayerScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Continue()
    {
        if (PlayerPrefs.GetInt(PlayerSceneStr) != null && PlayerPrefs.GetInt(PlayerSceneStr) > 0)
            LockCursor(); 
        SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerSceneStr));
    }

    public void IncrSceneIndex()
    {
        currentSceneIndex++;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SavePlayerScene(int sceneIndex) {
        PlayerPrefs.SetInt(PlayerSceneStr, sceneIndex);
    }

    public void SaveSettings(int qualityIndex, int resolIndex, float volume, bool isFullScreen)
    {
        int fullScreenIndex = isFullScreen ? 1 : 0;
        PlayerPrefs.SetInt(QualitySettingStr, qualityIndex);
        PlayerPrefs.SetFloat(ScreenResolutionIndexStr, resolIndex);
        PlayerPrefs.SetFloat(VolumeStr, volume);
        PlayerPrefs.SetFloat(FullscreenToggleStr, fullScreenIndex);
    }


}
