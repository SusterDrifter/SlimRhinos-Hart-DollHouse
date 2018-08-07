using UnityEngine;
using System.Collections;
using TMPro;

public class Chapter1_2 : MonoBehaviour {

    [SerializeField] private Dialogue startDiag;
    [SerializeField] private Dialogue endingDiag;
    [SerializeField] private Dialogue ending2Diag;
    [SerializeField] private Dialogue threePhotosDiag;

    [SerializeField] private Sound scribble;

    [SerializeField] private Animator animator;
    private bool charlotteDrawing = false;
    [SerializeField] private TextMeshProUGUI textPrompt;
    
    [HideInInspector] public bool emilPhoto = false;
    [HideInInspector] public bool willPhoto = false;
    [HideInInspector] public bool rosiePhoto = false;

    public static Chapter1_2 instance;

    void Start () {

        if (instance != null)
            return;

        instance = this;

        if (scribble.clip)
        {
            scribble.source = gameObject.AddComponent<AudioSource>();
            scribble.source.clip = scribble.clip;

            scribble.source.volume = scribble.volume;
            scribble.source.pitch = scribble.pitch;
            scribble.source.spatialBlend = scribble.spatialBlend;

            scribble.source.loop = scribble.loop;
            scribble.source.playOnAwake = scribble.playOnAwake;
        }

        animator = GetComponent<Animator>();
        textPrompt = GetComponentInChildren<TextMeshProUGUI>();
        textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, 0);
	}

    private void Update()
    {
        if (charlotteDrawing)
        {
            textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, Mathf.PingPong(Time.time, 1));

            if (Input.anyKeyDown)
            {
                animator.speed = 1;
                textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, 0);
                charlotteDrawing = false;
            }
        }
    }

    public void StartDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(startDiag);
    }

    public void EndDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(endingDiag);
    }

    public void EndTwoDiag()
    {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(ending2Diag);
    }

    public void ScribbleSound()
    {
        AudioManager.instance.PlayClip(scribble);
    }

    public void ThreePhotosDiag()
    {
        StartCoroutine(DelayThreeDiagBy(2.0f));
    }

    public void EndingSeq()
    {
        if (emilPhoto && willPhoto && rosiePhoto)
            animator.SetTrigger("Ending");
    }

    IEnumerator DelayThreeDiagBy(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(threePhotosDiag);
    }

    public void PauseAnim()
    {
        animator.speed = 0;
        charlotteDrawing = true;
        textPrompt.color = new Color(textPrompt.color.r, textPrompt.color.g, textPrompt.color.b, 1f);
    }

    public void FlipSound()
    {
        AudioManager.instance.PlayClip(Sound.SoundType.SoundEffect, "DiaryOpen");
    }

    public void NextChapter()
    {
        ClampingTrigger.instance.InputSet(true);
        ClampingTrigger.instance.MovementSet(true);
        GameManager.instance.NextChapter();
    }

    public void PlayBackgroundMusic()
    {
        AudioManager.instance.PlayClip(Sound.SoundType.BackgroundMusic, "Shadowlands");
    }
}
