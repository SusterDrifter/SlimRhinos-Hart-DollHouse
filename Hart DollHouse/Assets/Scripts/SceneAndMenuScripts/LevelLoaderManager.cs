using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoaderManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI endChapterText;
    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private Dialogue writingEntry;
    [SerializeField] private TextMeshProUGUI textPrompt;
    [SerializeField] private string finishPrompt = "Press any key to continue.";
    [SerializeField] private Animator animator;
    [SerializeField] private Dialogue chapterNames;

    private int sceneIndex;

    // Booleans for diary transition
    private bool usingDiaryTransition = false;
    private bool finishLoading = false;
    private bool hasSetText = false;

    #region Singleton
    public static LevelLoaderManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        animator = GetComponent<Animator>();

        if (endChapterText != null)
            endChapterText.SetText("");

        if (screenText != null)
            screenText.SetText("");

        if (textPrompt != null)
            textPrompt.SetText("");

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Update()
    {
        if (usingDiaryTransition)
            DiaryTransition();
    }

    public void LoadScene(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
        usingDiaryTransition = true;
        string fullEntry = "";
        
        if (writingEntry != null)
        {
            foreach (string sentence in writingEntry.sentences)
                fullEntry += sentence;
        }

        screenText.SetText(fullEntry);
        animator.ResetTrigger("FadeOut");
        animator.SetTrigger("FadeIn");
    }

    public void StartLoadAsync()
    {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
            yield return null;

        finishLoading = true;
    }

    private void DiaryTransition()
    {
        if (finishLoading && !hasSetText)
        {
            textPrompt.SetText(finishPrompt);
            hasSetText = true;
        }
        else if (finishLoading)
        {
            textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, Mathf.PingPong(Time.time, 1));

            if (Input.anyKeyDown)
            {
                StopCoroutine("LoadNewScene");
                usingDiaryTransition = false;
                screenText.SetText("");
                textPrompt.SetText("");
                animator.ResetTrigger("FadeIn");
                animator.SetTrigger("FadeOut");
            }
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        animator.ResetTrigger("SimpleFadeOut");
        animator.SetTrigger("SimpleFadeIn");
        this.sceneIndex = sceneIndex;
    }

    public void StartLoadSync()
    {
        SceneManager.LoadScene(sceneIndex);
        animator.ResetTrigger("SimpleFadeIn");
        animator.SetTrigger("SimpleFadeOut");
    }

    private void EndChapter(int chapterNum)
    {
        string chapterName = chapterNames.sentences[chapterNum];
        string formattedText = "End of Chapter " + chapterNum + ": " + " " + chapterName;
        endChapterText.SetText(formattedText);
    }

    public void EndChapterLoadScene(int chapterNum)
    {
        EndChapter(chapterNum);
        animator.SetTrigger("EndChapter");
    }

    public void EndChapterFade(int chapterNum)
    {
        EndChapter(chapterNum);
        animator.SetTrigger("EndChapterSimple");
    }
}
