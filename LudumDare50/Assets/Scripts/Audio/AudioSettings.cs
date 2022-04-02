using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Basics.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer = null;

    public static AudioSettings Instance = null;

    #region Singleton


    private void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void LoadMixerVolume()
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 32);
        Debug.Log(Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 32);
        mainMixer.SetFloat("SpeechVolume", Mathf.Log10(PlayerPrefs.GetFloat("SpeechVolume")) * 32);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 32);
        mainMixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("EffectsVolume")) * 32);
    }

    public void SetMasterVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 32);
        Debug.Log(Mathf.Log10(volume) * 32);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public float GetMasterVolume()
    {
        if (mainMixer.GetFloat("MasterVolume", out float value))
        {
            return Mathf.Pow(10, value / 32);
        }
        else
        {
            Debug.LogWarning("There is no mixer group with name MasterVolume");
            return PlayerPrefs.GetFloat("MasterVolume");
        }
    }



    public void SetSpeechVolume(float volume)
    {
        mainMixer.SetFloat("SpeechVolume", Mathf.Log10(volume) * 32);
        PlayerPrefs.SetFloat("SpeechVolume", volume);
    }

    public float GetSpeechVolume()
    {
        if (mainMixer.GetFloat("SpeechVolume", out float value))
        {
            return Mathf.Pow(10, value / 32);
        }
        else
        {
            Debug.LogWarning("There is no mixer group with name SpeechVolume");
            return PlayerPrefs.GetFloat("SpeechVolume");
        }
    }



    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 32);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public float GetMusicVolume()
    {
        if (mainMixer.GetFloat("MusicVolume", out float value))
        {
            return Mathf.Pow(10, value / 32);
        }
        else
        {
            Debug.LogWarning("There is no mixer group with name MusicVolume");
            return PlayerPrefs.GetFloat("MusicVolume");
        }
    }



    public void SetEffectsVolume(float volume)
    {
        mainMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 32);
        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }

    public float GetEffectsVolume()
    {
        if (mainMixer.GetFloat("EffectsVolume", out float value))
        {
            return Mathf.Pow(10, value / 32);
        }
        else
        {
            Debug.LogWarning("There is no mixer group with name MusicVolume");
            return PlayerPrefs.GetFloat("EffectsVolume");
        }
    }
}


