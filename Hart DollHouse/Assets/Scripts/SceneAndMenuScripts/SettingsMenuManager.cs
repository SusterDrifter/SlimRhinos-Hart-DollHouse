using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour {

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider volumeSlider;

    private Resolution[] resolutions;

    private int playerQuality = 2;
    private int playerResolIndex = -1;
    private float playerVolume = 0;
    private bool playerFullscreen = true;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolOpt = new List<string>();
        int curResolIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolOpt.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                curResolIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolOpt);
        resolutionDropdown.value = curResolIndex;
        resolutionDropdown.RefreshShownValue();

        int defQualitySettings = QualitySettings.GetQualityLevel();
        qualityDropdown.value = defQualitySettings;
        qualityDropdown.RefreshShownValue();

        float defVol = 0;
        audioMixer.GetFloat("MasterVolume", out defVol);
        volumeSlider.value = defVol;

        bool defFullScreen = Screen.fullScreen;
        fullScreenToggle.isOn = defFullScreen;
    }


    public void SetResolution(int resolIndex)
    {
        Screen.SetResolution(resolutions[resolIndex].width, resolutions[resolIndex].height, Screen.fullScreen);
        playerResolIndex = resolIndex;
    }

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);
        playerVolume = vol;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        playerQuality = qualityIndex;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        playerFullscreen = isFullScreen;
    }

    public void SaveSettings()
    {
        GameManager.instance.SaveSettings(playerQuality, 
            playerResolIndex, playerVolume, playerFullscreen);
    }

    public void LoadSettings()
    {
        int qual = PlayerPrefs.GetInt(GameManager.instance.QualitySettingStr);
        SetQuality(qual);
        qualityDropdown.value = qual;
        qualityDropdown.RefreshShownValue();

        int resolIndex = PlayerPrefs.GetInt(GameManager.instance.ScreenResolutionIndexStr);
        SetResolution(resolIndex);
        resolutionDropdown.value = resolIndex;
        resolutionDropdown.RefreshShownValue();

        float vol = PlayerPrefs.GetFloat(GameManager.instance.VolumeStr);
        SetVolume(vol);
        volumeSlider.value = vol;

        bool fullScreen = PlayerPrefs.GetInt(GameManager.instance.FullscreenToggleStr) != 0 ? true : false;
        SetFullScreen(fullScreen);
        fullScreenToggle.isOn = fullScreen;
    }
}
