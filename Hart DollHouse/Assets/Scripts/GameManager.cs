using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isPaused = false;
    public int currentSceneIndex;
    private int menuIndex = 0;
    
    #region Singleton
    public static GameManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame() {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame() {
        if (!isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void IncrSceneIndex()
    {
        currentSceneIndex++;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
