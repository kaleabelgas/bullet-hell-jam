using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class UIMainGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject tutorial;
    [SerializeField] Slider slider;

    ColorAdjustments colorAdjustments;
    Volume volume;

    private bool paused = false;

    private float timeRemaining = 10;
    private float timePerLevel = 10;

    private float timeToDisplay;

    private int level = 0;

    private void Start()
    {
        AudioManager.instance.Play("level moosic");
        currentLevel.text = "LEVEL " + level.ToString();
        
        volume = FindObjectOfType<Volume>();
        if(volume != null)
            volume.profile.TryGet(out colorAdjustments);

    }

    public void UpdateCurrentLevel()
    {
        level++;
        currentLevel.text = "LEVEL " + level.ToString();
    }

    public void DeleteTutorial()
    {
        tutorial.SetActive(false);
    }

    public void SetHealthBarMax(int healthmax)
    {
        slider.maxValue = healthmax;
        slider.value = healthmax;
    }

    public void SetHealthBar(int health)
    {
        slider.value = health;
    }

    public void Pause()
    {
        if(colorAdjustments != null)
            colorAdjustments.active = !colorAdjustments.active;
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            Debug.Log("Paused");
            //AudioManager.instance.Play("level moosic");
        }
        else
        {
            Time.timeScale = 1;
            //AudioManager.instance.UnPause("level moosic");

        }
    }

    public void GoToMainMenu()
    {
        AudioManager.instance.Stop("level moosic");
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeToDisplay = Mathf.CeilToInt(timeRemaining);
        }
        else
        {
            timeRemaining = timePerLevel;
            timeToDisplay = 10;
        }

        timer.text = timeToDisplay.ToString();
    }
}
