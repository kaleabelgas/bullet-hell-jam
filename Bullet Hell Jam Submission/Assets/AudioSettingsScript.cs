using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        var newValue = Mathf.Max(.001f, volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.InverseLerp(0, 10, newValue)) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        var newValue = Mathf.Max(.001f, volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.InverseLerp(0, 10, newValue)) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        var newValue = Mathf.Max(.001f, volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.InverseLerp(0, 10, newValue)) * 20);
    }
}
