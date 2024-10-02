using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sfxSlider;
    public Slider musicSlider;
    public TMP_Dropdown resoDropdown;
    public TMP_Dropdown qualityDropdown;

    private float volumeOut;
    private int qualityLevel;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resoDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResoIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResoIndex = i;
            }
        }

        List<string> opts = new List<string>();
        string opt1 = "160 x 120";
        string opt2 = "800 x 600";
        opts.Add(opt1);
        opts.Add(opt2);

        resoDropdown.AddOptions(opts);

        resoDropdown.AddOptions(options);
        resoDropdown.value = currentResoIndex;
        resoDropdown.RefreshShownValue();

        // Set current values

        audioMixer.GetFloat("volume", out volumeOut);
        musicSlider.value = volumeOut;

        qualityLevel = QualitySettings.GetQualityLevel();
        qualityDropdown.value = qualityLevel;

    }

    public void SetSFXVolume(float volume)
    {

    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetReso(int resoIdx)
    {
        Resolution resolution = resolutions[resoIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
