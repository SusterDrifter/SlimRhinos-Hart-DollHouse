using UnityEngine;
using System.Collections;

public class Chapter2_4 : MonoBehaviour {

    [SerializeField] private Dialogue diaryDiag;
    [SerializeField] private Dialogue bathroomSceneDiag;
    [SerializeField] private Dialogue bathroomSceneTwoDiag;
    [SerializeField] private Item Diary;

    [SerializeField] private GameObject bedToCor;
    [SerializeField] private GameObject bedToBath;

    private Animator animator;
    private bool diaryTrig = false;
    private bool chapterStart = false;

    #region Singleton
    public static Chapter2_4 instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        animator = GetComponent<Animator>();
    }
    #endregion

    void Update() {

        if (Input.anyKeyDown && !chapterStart)
        {
            animator.SetTrigger("Start");
            AudioManager.instance.PlayClip(Sound.SoundType.BackgroundMusic, "Darkness");
            chapterStart = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !diaryTrig)
        {
            TriggerDiaryDiag();
            diaryTrig = true;
        }
    }

    public void TriggerDiaryDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(diaryDiag);
    }

    public void TriggerBathDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(bathroomSceneDiag);
    }

    public void TriggerBathTwoDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(bathroomSceneTwoDiag);
    }

    private void ActivateDiaryScene()
    {
        MainUIManager.instance.GetDiary().ActivateDiary();
    }

    public void ActivateClamping()
    {
        ClampingTrigger.instance.ActivateLocking();
    }

    public void DeactivateClamping()
    {
        ClampingTrigger.instance.DeactivateLocking();
    }

    public void DeactivateTransition(GameObject obj)
    {
        StartCoroutine(CoroutineDestroyObj(obj, 0.1f, 2f));
    }

    IEnumerator CoroutineDestroyObj(GameObject obj, float fadeDuration, float delayDur)
    {
        MainUIManager.instance.GetBlackScreen().BlackFadeIn(fadeDuration);

        yield return new WaitForSecondsRealtime(fadeDuration);

        MainUIManager.instance.GetBlackScreen().BlackFadeOutDelayBy(delayDur);

        if (obj)
            obj.gameObject.SetActive(false);
    }

    public void BathroomScene()
    {
        animator.SetTrigger("Bathroom");
    }

    public void AddDiary()
    {
        if (Diary)
            Inventory.instance.AddToInvent(Diary);
    }

    public void NextChapter()
    {
        GameManager.instance.FadeNextChapter();
    }

    public void DoorNewDiag()
    {
        if (bedToBath)
            bedToBath.GetComponent<DialogueObject>().useNewDiag = true;
        if (bedToCor)
            bedToBath.GetComponent<BedToBath>().useNewDiag = true;
    }
}
