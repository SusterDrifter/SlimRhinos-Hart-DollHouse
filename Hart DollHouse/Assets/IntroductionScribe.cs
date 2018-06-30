using System.Collections;
using UnityEngine;
using TMPro;

public class IntroductionScribe : MonoBehaviour {

    [SerializeField] private Dialogue sinclairDialogue;
    [SerializeField] private Dialogue anyaDialogue;
    [SerializeField] private Dialogue evDialogue;
    [SerializeField] private Dialogue charlotteDialogue;

    private bool anyaPath = false;
    private bool evPath = false;
    private bool charlottePath = false;

    private bool panicAttack = false;
    private bool forceFinish = false;
    private bool effectRunning = false;
    private bool decisionRunning = true;

    private bool chapterEnd = false;

    private bool breathingTutStarted = false;
    private bool breathingGameStarted = false;
    private bool breathingGameWon = false;
    private bool doneChapter = false;

    private int sinclairIndex = 0;
    private int playerIndex = 0;
    private int choiceTurn = 0;
    private int talkTurn = 0;

    private float winDuration = 11f;
    private float lastSentenceDur = 0.5f;
    private float lastSentenceStay = 2f;

    private string currText = "";
    [SerializeField] TextMeshProUGUI screenText;
    [SerializeField] TextMeshProUGUI nameText;
    private float delayTime = 0.05f;
    
    [SerializeField] private CanvasGroup charDecision;
    [SerializeField] private CanvasGroup panicDecision;
    [SerializeField] private CanvasGroup talkDecision;
    [SerializeField] private BreathingSystemUIController breathingGame;
    [SerializeField] private UIFader fader;
    [SerializeField] private Timer timer;
    [SerializeField] private Animator animator;

    private string currName = "";
    private int[] talkSequence;

    private Sound[] typingSound;
    private Sound introSancLogo;

    private void Start()
    {
        talkSequence = new int[] { 0, 0, 1, 0, 2, 1, 0, 0, 2, 0, 1, 0, 2, 1, 0, 1, 0, 2, 0, 2};

        typingSound = new Sound[5];
        for (int i = 0; i < typingSound.Length; i++)
        {
            string clipName = "TypeWriter" + i;
            typingSound[i] = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, clipName);
        }

        introSancLogo = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "IntroSancLogo");

        charDecision.alpha = 0;
        charDecision.interactable = false;

        panicDecision.alpha = 0;
        panicDecision.interactable = false;

        talkDecision.alpha = 0;
        talkDecision.interactable = false;

        animator.SetTrigger("FadeInPlayerDisc");

    }

    public void BeginConvo()
    {
        decisionRunning = false;
        currText = sinclairDialogue.sentences[sinclairIndex];
        currName = sinclairDialogue.speaker + " :";
        nameText.SetText(currName);
        sinclairIndex++;
        StartCoroutine(TypeWriter());
    }

    void Update () {
        if (doneChapter)
        {
            return;
        } else if (breathingTutStarted)
        {
            if (breathingGame.curCycleFinished)
            {
                breathingTutStarted = false;
                talkTurn++;
                decisionRunning = false;
                currText = sinclairDialogue.sentences[sinclairIndex];
                currName = sinclairDialogue.speaker + " :";
                sinclairIndex++;
                StartCoroutine(TypeWriter());
            }
        }
        else if ((panicAttack || talkTurn > talkSequence.Length) && !breathingGameStarted)
        {
            currText = "";
            screenText.SetText("");
            animator.SetTrigger("PanicAttack");
        }
        else if (breathingGameStarted)
        {
            if (timer.HasRunOut() && !breathingGame.curCycleFinished && !breathingGameWon)
            {
                breathingGameWon = true;
            }
            if (breathingGame.curCycleFinished)
            {
                breathingGame.curCycleFinished = false;
                if (breathingGameWon)
                    FlashesWallTally();
                else
                    IntroScene();

                doneChapter = true;
            }
        }
        else if (effectRunning & Input.GetMouseButtonDown(0))
        {
            forceFinish = true;
        }
        else if (!decisionRunning && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            talkTurn++;
            if (talkSequence[talkTurn] == 0)
            {
                currText = sinclairDialogue.sentences[sinclairIndex];
                currName = sinclairDialogue.speaker + " :";
                sinclairIndex++;
                StartCoroutine(TypeWriter());
            }
            else if (talkSequence[talkTurn] == 1)
            {

                if (anyaPath) 
                    currText = anyaDialogue.sentences[playerIndex];
                else if (charlottePath)
                    currText = charlotteDialogue.sentences[playerIndex];
                else if (evPath)
                    currText = evDialogue.sentences[playerIndex];
                else
                    currText = anyaDialogue.sentences[playerIndex];
                
                playerIndex++;
                currName = "... :";
                StartCoroutine(TypeWriter());

            }
            else if (talkSequence[talkTurn] == 2) {
                screenText.SetText("");
                nameText.SetText("");
                if (choiceTurn == 0)
                    FadeCanvasIn(charDecision);
                else if (choiceTurn == 1)
                    FadeCanvasIn(panicDecision);
                else if (choiceTurn == 2)
                    FadeCanvasIn(talkDecision);
                else if (choiceTurn == 3)
                {
                    decisionRunning = true;
                    breathingGame.BeginTutorial();
                    breathingTutStarted = true;
                    choiceTurn++;
                } else if (choiceTurn == 4)
                {
                    panicAttack = true;
                }
            }
        }
	}

    public void StartRealBreathingGame()
    {
        breathingGameStarted = true;
        timer.SetDuration(winDuration);
        timer.RestartTimer();
        breathingGame.BeginBreathingSystem();
    }

    private void FadeCanvasIn(CanvasGroup cg)
    {
        decisionRunning = true;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        fader.FadeIn(cg, 0.2f);
        talkTurn++;
        choiceTurn++;
    }

    private void FadeCanvasOut(CanvasGroup cg)
    {
        fader.FadeOut(cg, 0.2f);
        decisionRunning = false;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void HorrifiedPath() {
        anyaPath = true;
        FadeCanvasOut(charDecision);
        currText = anyaDialogue.sentences[playerIndex];
        playerIndex++;
        currName = "... :";
        StartCoroutine(TypeWriter());
    }

    public void SadPath() {
        charlottePath = true;
        FadeCanvasOut(charDecision);
        currText = charlotteDialogue.sentences[playerIndex];
        playerIndex++;
        currName = "... :";
        StartCoroutine(TypeWriter());
    }

    public void FrustatedPath() {
        evPath = true;
        FadeCanvasOut(charDecision);
        currText = evDialogue.sentences[playerIndex];
        playerIndex++;
        currName = "... :";
        StartCoroutine(TypeWriter());
    }

    public void ReadyPath()
    {
        FadeCanvasOut(panicDecision);
        currText = sinclairDialogue.sentences[sinclairIndex];
        sinclairIndex++;
        currName = sinclairDialogue.speaker + " :";
        StartCoroutine(TypeWriter());
    }

    public void NotReadyPath()
    {
        FadeCanvasOut(panicDecision);
        panicAttack = true;
        talkTurn = talkSequence.Length - 1;
        choiceTurn = 4;
    }

    private void PersonalRecount()
    {
        if (anyaPath)
            currText = anyaDialogue.sentences[playerIndex];
        else if (evPath)
            currText = evDialogue.sentences[playerIndex];
        else if (charlottePath)
            currText = charlotteDialogue.sentences[playerIndex];

        playerIndex = 6;
        currName = "... :";
        StartCoroutine(TypeWriter());
    }

    public void WillPath() {
        PersonalRecount();
        FadeCanvasOut(talkDecision);
    }

    public void RosiePath() {
        playerIndex ++;
        PersonalRecount();
        FadeCanvasOut(talkDecision);
    }

    public void EmilPath() {
        playerIndex += 2;
        PersonalRecount();
        FadeCanvasOut(talkDecision);
    }

    IEnumerator DelayBySeconds(float seconds, IEnumerator couroutine)
    {
        yield return new WaitForSecondsRealtime(seconds);
        StartCoroutine(couroutine);
    }

    IEnumerator TypeWriter()
    {
        effectRunning = true;
        nameText.SetText(currName);
        string textToShow = "";
        float newDelayTime = delayTime;

        for (int i = 0; i < currText.Length; i++) {

            if (forceFinish) {
                textToShow = currText;
                forceFinish = effectRunning = false;
                screenText.SetText(textToShow);
                yield break;
            } else {
                textToShow = currText.Substring(0, i);
                screenText.SetText(textToShow);
            }
            Sound typing = typingSound[Mathf.FloorToInt(Random.value * typingSound.Length)];
            if (screenText.text != "")
                AudioManager.instance.PlayClip(typing);
            yield return new WaitForSecondsRealtime(newDelayTime);
        }
        effectRunning = false;

        if (chapterEnd)
        {
            yield return new WaitForSecondsRealtime(lastSentenceStay);
            screenText.SetText("");
            animator.SetTrigger("FadeInSancLogo");
        }

    }

    private void FlashesWallTally()
    {
        IntroScene();
    }

    public void IntroScene()
    {
        StopAllCoroutines();
        currName = "";
        currText = "...How many? ";
        chapterEnd = true;
        delayTime = lastSentenceDur;
        StartCoroutine(DelayBySeconds(4f, TypeWriter()));
    }

    public void NextChapter()
    {
        GameManager.instance.NextChapter();
    }

    public void PlayIntroSound()
    {
        if (introSancLogo == null)
            introSancLogo = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "IntroSancLogo");

        AudioManager.instance.audioFader.FadeIn(introSancLogo, 1f, introSancLogo.volume);
    }

    public void FadeOutIntroSound()
    {
        if (introSancLogo == null)
            introSancLogo = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "IntroSancLogo");

        AudioManager.instance.audioFader.FadeOut(introSancLogo);
    }
}
