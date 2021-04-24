using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] GameObject globalVolume;
    private void Start()
    {
        globalVolume = GameObject.FindGameObjectWithTag("Volume");
        globalVolume.SetActive(PlayerPrefs.GetInt("isVolumeOn") <= 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TogglePostProcessing();
        }
    }

    public void TogglePostProcessing()
    {
        if (PlayerPrefs.GetInt("isVolumeOn") <= 0)
        {
            globalVolume.SetActive(false);
            PlayerPrefs.SetInt("isVolumeOn", 1);
        }
        else
        {
            globalVolume.SetActive(true);
            PlayerPrefs.SetInt("isVolumeOn", 0);
        }
    }
}
