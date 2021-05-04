using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    private Volume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;

    private bool isBloom = true;
    private bool isChromAb = true;
    private void Awake()
    {
        volume = FindObjectOfType<Volume>();
        if (volume != null)
        {
            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out chromaticAberration);
        }

        isBloom = PlayerPrefs.GetInt("isBloom", 1) > 0;
        isChromAb = PlayerPrefs.GetInt("isChromAberration", 1) > 0;
    }
    private void Start()
    {
        bloom.active = isBloom;
        chromaticAberration.active = isChromAb;
    }
}
