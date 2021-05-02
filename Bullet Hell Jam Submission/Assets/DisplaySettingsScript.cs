using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DisplaySettingsScript : MonoBehaviour
{
    private Volume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;


    [SerializeField] Toggle bloomToggle;
    [SerializeField] Toggle CAToggle;
    [SerializeField] Toggle camShakeToggle;

    private bool _isBloom;
    private bool _isChrom;
    private bool _isCamShake;
    private void OnEnable()
    {
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
}
