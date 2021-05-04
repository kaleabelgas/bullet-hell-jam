using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class DisplaySettingsScript : MonoBehaviour
{
    private Volume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;

    private Resolution[] resolutions;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle bloomToggle;
    [SerializeField] Toggle CAToggle;
    [SerializeField] Toggle camShakeToggle;
    [SerializeField] Toggle fullscreenToggle;
    private void OnEnable()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i <resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            if (resolutionOptions.Contains(option))
                continue;
            resolutionOptions.Add(option);

            if (resolutions[i].width.Equals(Screen.width) && resolutions[i].height.Equals(Screen.height))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        volume = FindObjectOfType<Volume>();

        if (volume != null)
        {
            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out chromaticAberration);
        }

        bloomToggle.isOn = PlayerPrefs.GetInt("isBloom", 1) > 0;
        CAToggle.isOn = PlayerPrefs.GetInt("isChromAberration", 1) > 0;
        camShakeToggle.isOn = PlayerPrefs.GetInt("isCameraShake", 1) > 0;
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void ToggleBloom(bool value)
    {
        bloom.active = value;
        PlayerPrefs.SetInt("isBloom", value ? 1 : 0);
    }
    public void ToggleChromaticAberration(bool value)
    {
        chromaticAberration.active = value;
        PlayerPrefs.SetInt("isChromAberration", value ? 1 : 0);
    }
    public void ToggleCamShake(bool value)
    {
        PlayerPrefs.SetInt("isCameraShake", value ? 1 : 0);
        CameraShake.IsEnabled = value;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Debug.Log($"{res.height} {res.width}");
        Screen.SetResolution(res.width, res.height, Screen.fullScreen); 
    }
}
