using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class Chapter1_1 : MonoBehaviour {

    [SerializeField] private Dialogue startDiag;

    public bool hasRhymeGameFinished = false;
    public RhymeGame rhymeGame;

    private PostProcessVolume cameraPPV;
    private DepthOfField depthOfField;
    private FloatParameter normAperture;
    private FloatParameter blurAperture;

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
        BlurEffect();
        LockMovement();
    }
    #endregion

    private void BlurEffect()
    {
        StartCoroutine(ChangeBlur(2.0f));
    }

    private void LockMovement()
    {
        StartCoroutine(LockMovementFor(9.0f));
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
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(startDiag);
    }

    IEnumerator LockMovementFor(float seconds)
    {
        ClampingTrigger.instance.ActivateLocking();
        yield return new WaitForSecondsRealtime(seconds);
        ClampingTrigger.instance.DeactivateLocking();
    }

    public void RhymeGameFinish()
    {
        hasRhymeGameFinished = true;
    }
}
