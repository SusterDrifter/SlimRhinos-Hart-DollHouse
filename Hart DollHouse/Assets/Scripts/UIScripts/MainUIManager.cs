using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {

    #region Singleton
    public static MainUIManager instance;

    void Awake () {
	    if (instance != null)
        {
            Debug.Log("WARNING! More than one instance of MainUIManager is created.");
            return;
        }
        instance = this;
	}
    #endregion

    [SerializeField] private BlackScreenUI blackScreen;
    [SerializeField] private WhiteScreenUI whiteScreen;
    [SerializeField] private ItemUI itemUI;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private BreathingSystemUIController breathingManager;
    [SerializeField] private DiaryManager diary;
    [SerializeField] private NotificationUIManager notification;
    [SerializeField] private ArticleUIManager article;

    private void Start()
    {
        blackScreen = GetComponentInChildren<BlackScreenUI>();
        whiteScreen = GetComponentInChildren<WhiteScreenUI>();
        itemUI = GetComponentInChildren<ItemUI>();
        dialogueManager = GetComponentInChildren<DialogueManager>();
        breathingManager = GetComponentInChildren<BreathingSystemUIController>();
        diary = GetComponentInChildren<DiaryManager>();
        notification = GetComponentInChildren<NotificationUIManager>();
        article = GetComponentInChildren<ArticleUIManager>();
    }
    
    public ItemUI GetItemUI()
    {
        return itemUI;
    }

    public DialogueManager GetDialogueManager()
    {
        return dialogueManager;
    }

    public BreathingSystemUIController GetBreathingManager()
    {
        return breathingManager;
    }
    
    public WhiteScreenUI GetWhiteScreen()
    {
        return whiteScreen;
    }

    public DiaryManager GetDiary()
    {
        return diary;
    }

    public BlackScreenUI GetBlackScreen()
    {
        return blackScreen;
    }

    public NotificationUIManager GetNotificationUI()
    {
        return notification;
    }

    public ArticleUIManager GetArticleManager()
    {
        return article;
    }

}
