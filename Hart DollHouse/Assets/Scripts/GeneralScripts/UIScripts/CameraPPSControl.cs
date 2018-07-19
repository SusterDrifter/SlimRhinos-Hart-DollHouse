using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraPPSControl : MonoBehaviour {

    #region Singleton
    public static CameraPPSControl instance;

    void Awake () {
        if (instance != null) {
            Debug.Log("WARNING! More than one instance of CameraPPSControl is created.");
            return;
        }
        instance = this;
	}
    #endregion

    private Vignette vignette;
    private DepthOfField depthOfField;
    [SerializeField] private FloatParameter blurAperture;
    [SerializeField] private FloatParameter normAperture;

    private MainUIManager mainUI;

    private void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<DepthOfField>(out depthOfField);
        mainUI = MainUIManager.instance;

        blurAperture = new FloatParameter();
        blurAperture.value = 0.05f;

        normAperture = new FloatParameter();
        normAperture.value = 20f;

        if (vignette)
            vignette.enabled.value = false;
    }

    public void BlurVignetteUIActivate()
    {
        vignette.enabled.value = true;
        // Change aperture to 0.05
        depthOfField.aperture.value = blurAperture.value;
    }

    public void BlurVignetteUIDeactivate()
    {
        vignette.enabled.value = false;
        // Change aperture to normal
        depthOfField.aperture.value = normAperture.value;
    }

    public void PassingOutEffect(float percentage)
    {
        mainUI.GetBlackScreen().ChangeAlpha(percentage);
    }

    public void Flash()
    {
        mainUI.GetWhiteScreen().FlashingEffect();   
    }

}
