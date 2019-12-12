using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Dropdown resolutionDropdown;

    public float lookSensitivity;

    private Resolution[] availableResolutions;

    private void Start()
    {
        availableResolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> optionsList = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
            optionsList.Add(option);

            if(availableResolutions[i].width == Screen.currentResolution.width
                && availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volumeLevel)
    {
        masterMixer.SetFloat("MasterVolume", volumeLevel);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality: " + QualitySettings.GetQualityLevel());
    }

    public void SetFullscreen(bool toggleFullscreen)
    {
        //Screen.fullScreen = toggleFullscreen;
        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);

        if (toggleFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Debug.Log("Fullscreen: True");
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Debug.Log("Fullscreen: False");
        }

        Debug.Log("Fullscreen: " + Screen.fullScreen);
    }

    public void SetResolution(int desiredResolution)
    {
        Resolution resolution = availableResolutions[desiredResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Current Resolution: " + Screen.currentResolution.width + " x " + Screen.currentResolution.height);
    }

    public void SetLookSensitivity(float sensitivity)
    {
        lookSensitivity = sensitivity;
    }
}
