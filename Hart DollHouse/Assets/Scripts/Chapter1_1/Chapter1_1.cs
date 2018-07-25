using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class Chapter1_1 : MonoBehaviour {

    [SerializeField] private Dialogue startDiag;
    [SerializeField] private Dialogue endingDiag;

    public bool hasRhymeGameFinished = false;
    public bool hasTrunkOpened = false;
    public RhymeGame rhymeGame;

    private bool endingSequenceStarted = false;
    private bool endingTriggered = false;
    
    private PostProcessVolume cameraPPV;
    private DepthOfField depthOfField;
    private FloatParameter normAperture;
    private FloatParameter blurAperture;

    private bool gameStart = false;

    #region Singleton
    public static Chapter1_1 instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        rhymeGame = GetComponent<RhymeGame>();

        #region BlurIn
        cameraPPV = Camera.main.GetComponent<PostProcessVolume>();
        cameraPPV.profile.TryGetSettings<DepthOfField>(out depthOfField);

        float nor = depthOfField.aperture.value;

        normAperture = new FloatParameter();
        normAperture.value = nor;

        blurAperture = new FloatParameter();
        blurAperture.value = 0.01f;

        #endregion

    }
    #endregion

    private void Update()
    {
        if (!gameStart && Input.anyKeyDown) {
            gameStart = true;
            StartCoroutine(ChangeBlur(4.0f));
            StartCoroutine(StartDiag(1.0f));
        }

        if (endingSequenceStarted)
        {
            if (MainUIManager.instance.GetBreathingManager().curCycleFinished && !endingTriggered)
            {

                endingTriggered = true;
                StartCoroutine(EndingScene(5f));
            }
        }


    }

    public void EndSequence()
    {
        StartCoroutine(StartPanic(8.0f));
        endingSequenceStarted = true;
    }

    IEnumerator EndingScene(float duration)
    {
        endingTriggered = true;
        yield return new WaitForSecondsRealtime(duration);
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(endingDiag);
        yield return new WaitForSecondsRealtime(30f);
        GameManager.instance.FadeNextChapter();
    }

    IEnumerator ChangeBlur(float duration)
    {
        float newDur = 0.5f * duration / (Mathf.Pow(Time.deltaTime, 2));
        depthOfField.aperture.value = blurAperture.value;

        float amountLeft = depthOfField.aperture.value - normAperture.value;
        float blurIncrease = amountLeft / newDur;
        while (normAperture.value - depthOfField.aperture.value > 4f) {

            amountLeft = normAperture.value - depthOfField.aperture.value;
            blurIncrease = amountLeft / newDur;

            float curAperture = Mathf.Lerp(depthOfField.aperture.value, normAperture.value, blurIncrease);
            depthOfField.aperture.value = curAperture;
            yield return null;
        }

        depthOfField.aperture.value = normAperture.value;
    }

    IEnumerator LockMovementFor(float seconds)
    {
        ClampingTrigger.instance.ActivateLocking();
        yield return new WaitForSecondsRealtime(seconds);
        ClampingTrigger.instance.DeactivateLocking();
    }

    IEnumerator StartDiag(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(startDiag);
    }

    IEnumerator StartPanic(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (hasTrunkOpened)
            PanicAttackController.instance.PanicAttackAnim();
    }

    public void RhymeGameFinish()
    {
        hasRhymeGameFinished = true;
    }
}
