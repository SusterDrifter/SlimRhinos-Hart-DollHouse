using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoaderManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private Dialogue writingEntry;
    [SerializeField] private TextMeshProUGUI textPrompt;
    [SerializeField] private string finishPrompt = "Press any key to continue.";

    private int sceneIndex;

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

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        screenText = texts[0];
        textPrompt = texts[1];

        screenText.SetText("");
        textPrompt.SetText("");

        gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        this.sceneIndex = sceneIndex;
        usingDiaryTransition = true;
        string fullEntry = "";
        
        if (writingEntry != null)
        {
            foreach (string sentence in writingEntry.sentences)
                fullEntry += sentence;
        }

        screenText.SetText(fullEntry);
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
                gameObject.SetActive(false);
            }
        }
    }
}
