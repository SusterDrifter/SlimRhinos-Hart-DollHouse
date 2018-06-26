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

    private MainUIManager mainUI;

    private void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<DepthOfField>(out depthOfField);
        mainUI = MainUIManager.instance;

        vignette.enabled.value = false;
        depthOfField.enabled.value = false;
    }

    public void BlurVignetteUIActivate()
    {
        vignette.enabled.value = true;
        depthOfField.enabled.value = true;
    }

    public void BlurVignetteUIDeactivate()
    {
        vignette.enabled.value = false;
        depthOfField.enabled.value = false;
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
