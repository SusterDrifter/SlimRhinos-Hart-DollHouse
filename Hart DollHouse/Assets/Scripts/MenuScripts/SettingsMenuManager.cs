using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class SettingsMenuManager : MonoBehaviour {

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

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
    }


    public void SetResolution(int resolIndex)
    {
        Screen.SetResolution(resolutions[resolIndex].width, resolutions[resolIndex].height, Screen.fullScreen);
    }

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);        
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
