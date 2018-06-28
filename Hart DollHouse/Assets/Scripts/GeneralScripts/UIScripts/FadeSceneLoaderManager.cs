using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneLoaderManager : MonoBehaviour {

    [SerializeField] private Animator animator;
    private int sceneIndex;

    #region Singleton
    public static FadeSceneLoaderManager instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        animator = GetComponent<Animator>();
        
    }
    #endregion

    public void FadeToScene(int sceneIndex)
    {
        animator.SetTrigger("FadeOut");
        this.sceneIndex = sceneIndex;
    }

    public void TriggerActualLoad()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
