using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int currentSceneIndex;
    
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
}
