using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIFader), typeof(Timer), typeof(CanvasGroup))]
public class DialogueManager : MonoBehaviour {


    // Components for player hints
    [SerializeField] private Dialogue[] hintsInput;
    [SerializeField] private float timeBetweenHints = 300f; // 5 minutes
    private Queue<Dialogue> playersHints;
    private Timer timer;

    [SerializeField] private CanvasGroup UIElement;
    [SerializeField] private Text screenText;
    [SerializeField] private UIFader fader;
    [SerializeField] private float delayPerChar = 0.15f;
    [SerializeField] private float minDelayBetweenSentences = 3f;
    [SerializeField] private float fadeDuration = 0.75f;

    private Queue<string> dialogues;
    
    private void Start()
    {
        timer = GetComponent<Timer>();
        timer.SetDuration(timeBetweenHints);

        playersHints = new Queue<Dialogue>();
        dialogues = new Queue<string>();

        fader = GetComponent<UIFader>();
        UIElement = GetComponent<CanvasGroup>();
        screenText = GetComponentInChildren<Text>();
        screenText.text = "";

        foreach(Dialogue dialogue in hintsInput)
        {
            playersHints.Enqueue(dialogue);
        }
            
        UIElement.alpha = 0;
        timer.RestartTimer();
    }

    private void Update()
    {
        CheckHintTiming();
    }

    public void BeginDialogue(Dialogue dialogue) {
        StopAllCoroutines();
        dialogues.Clear();
        foreach (string sentence in dialogue.sentences) {
            dialogues.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    private void DisplayNextSentence() {
        StartCoroutine(DelayBySeconds()); 
    }

    IEnumerator DelayBySeconds() {
        fader.FadeIn(UIElement, fadeDuration);
        screenText.text = "- ";
        while (dialogues.Count > 0) {
            string nextSentence = dialogues.Dequeue();
            screenText.text += nextSentence;
            float delayBetweenSentences = nextSentence.Length * delayPerChar;
            if (delayBetweenSentences < minDelayBetweenSentences)
            {
                delayBetweenSentences = minDelayBetweenSentences;
            }
            yield return new WaitForSecondsRealtime(delayBetweenSentences);
            screenText.text = "";
        }
        fader.FadeOut(UIElement, fadeDuration);
        screenText.text = "";
    }

    public void NextHint()
    {
        if (playersHints.Count > 0)
            playersHints.Dequeue();
        timer.RestartTimer();
    }

    /**
     * If there is no current dialogue and hints is not empty,
     * display the hint.
     */
    private void DisplayHint()
    {
        if (playersHints.Count > 0 && screenText.text == "") {
            Dialogue hint = playersHints.Peek();
            BeginDialogue(hint);
        } 

        timer.RestartTimer();
    }

    private void CheckHintTiming()
    {
        if (timer.HasRunOut()) {
            DisplayHint();
        }       
    }
}
