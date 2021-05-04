using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider master;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;

    private void Awake()
    {
        master.value = PlayerPrefs.GetFloat("mastervolume", 10);
        music.value = PlayerPrefs.GetFloat("musicvolume", 10);
        sfx.value = PlayerPrefs.GetFloat("sfxvolume", 10);
        Debug.Log(PlayerPrefs.GetFloat("mastervolume"));
    }
    private void Start()
    {
        SetMasterVolume((int)PlayerPrefs.GetFloat("mastervolume"));
        SetMusicVolume((int)PlayerPrefs.GetFloat("musicvolume"));
        SetSFXVolume((int)PlayerPrefs.GetFloat("sfxvolume"));
    }

    public void SetMasterVolume(float volume)
    {
        var newValue = Mathf.Max(.001f, volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.InverseLerp(0, 10, newValue)) * 20);
        PlayerPrefs.SetFloat("mastervolume", volume);
        Debug.Log("Used" + volume);
    }
    public void SetMusicVolume(float volume)
    {
        var newValue = Mathf.Max(.001f, volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.InverseLerp(0, 10, newValue)) * 20);
        PlayerPrefs.SetFloat("musicvolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        var newValue = Mathf.Max(.001f, volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.InverseLerp(0, 10, newValue)) * 20);
        PlayerPrefs.SetFloat("sfxvolume", volume);
    }
}
