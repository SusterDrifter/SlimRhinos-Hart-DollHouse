using UnityEngine;
using TMPro;
using System.Collections;

public class Chapter1_4 : MonoBehaviour {

    public bool scissorsPossessed = false;
    public bool needleStringPossessed = false;

    [SerializeField] private Dialogue startDiag;
    [SerializeField] private Dialogue endingDiag;
    [SerializeField] private Dialogue endingTwoDiag;
    [SerializeField] private Dialogue foundAllDiag;

    [SerializeField] private TextMeshProUGUI textPrompt;
    private Animator animator;
    private bool bathroomScene = false;

    public static Chapter1_4 instance;
    
    void Awake() {

        if (instance != null)
            return;

        instance = this;
        animator = GetComponent<Animator>();
        textPrompt = GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine(DelayStartDiagBy(3f));
    }

    private void Update()
    {
        if (bathroomScene)
        {
            textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, Mathf.PingPong(Time.time, 1));

            if (Input.anyKeyDown)
            {
                animator.speed = 1;
                textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, 0);
                bathroomScene = false;
            }
        }    
            
    }

    public void EndingTrig()
    {
        if (scissorsPossessed && needleStringPossessed)
        {
            MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(foundAllDiag);
            animator.SetTrigger("Ending");
        }
    }

    public void PauseAnim()
    {
        animator.speed = 0f;
        bathroomScene = true;
        textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, 1f);
    }

    public void NextLevel()
    {
        GameManager.instance.EndChapterNextChapter(1);
    }

    IEnumerator DelayStartDiagBy(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(startDiag);
        AudioManager.instance.PlayClip(Sound.SoundType.BackgroundMusic, "November");
    }
    
    public void StartEndDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(endingDiag);
    }

    public void StartEndTwoDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(endingTwoDiag);
    }
}
