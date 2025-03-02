using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    

    public AudioMixer audioMixer;
    public AudioMixer audioMi;
    public AudioMixer audios;
    public void setvolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMi.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audios.SetFloat("SFXVolume", volume);
    }

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }


    public void Quality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
