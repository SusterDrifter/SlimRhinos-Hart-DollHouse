using UnityEngine;
using System.Collections;

public class Chapter1_3 : MonoBehaviour {

    public static Chapter1_3 instance;
    [SerializeField] private Dialogue finishLookDiag;
    [SerializeField] private Dialogue startDiag;

    private bool diaryTrig = false;
    private bool startDiagTrig = false;
    [SerializeField] private float delayNextChap = 5.0f;

    private int tabCount = 0;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    private void Update()
    {
        if (!startDiagTrig && Input.anyKeyDown && startDiag)
        {
            MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(startDiag);
            startDiagTrig = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && Inventory.instance.Contains(4))
        {
            ++tabCount;
        }

        if (tabCount >= 2 && !diaryTrig)
        {
            Ending();
            diaryTrig = true;
        }
    }

    private void Ending()
    {
        StartCoroutine(DelayNextChapBy(delayNextChap));
    }

    IEnumerator DelayNextChapBy(float seconds)
    {
        if (finishLookDiag)
            MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(finishLookDiag);

        yield return new WaitForSecondsRealtime(seconds);
        GameManager.instance.FadeNextChapter();
    }
}
