using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isPaused = false;
    private int currentSceneIndex;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Death() {
        // Play some UI
        ResetChapter();
    }
    
    public void ResetChapter() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
