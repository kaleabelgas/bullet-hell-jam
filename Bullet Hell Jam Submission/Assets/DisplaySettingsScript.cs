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

    private bool _isBloom;
    private bool _isChrom;
    private bool _isCamShake;
    private void OnEnable()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i <resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
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

        _isBloom = PlayerPrefs.GetInt("isBloom") < 1;
        _isChrom = PlayerPrefs.GetInt("isChromAberration") < 1;
        _isCamShake = PlayerPrefs.GetInt("isCameraShake") < 1;

        bloomToggle.isOn = _isBloom;
        CAToggle.isOn = _isChrom;
        camShakeToggle.isOn = _isCamShake;
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void ToggleBloom(bool value)
    {
        bloom.active = value;
        PlayerPrefs.SetInt("isBloom", value ? 0 : 1);
    }
    public void ToggleChromaticAberration(bool value)
    {
        chromaticAberration.active = value;
        PlayerPrefs.SetInt("isChromAberration", value ? 0 : 1);
    }
    public void ToggleCamShake(bool value)
    {
        PlayerPrefs.SetInt("isCameraShake", value ? 0 : 1);
        CameraShake.IsEnabled = value;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen); 
    }
}
