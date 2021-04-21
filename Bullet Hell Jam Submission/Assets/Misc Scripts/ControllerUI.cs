using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ControllerUI : MonoBehaviour
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
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet(out colorAdjustments);

        currentLevel.text = "LEVEL " + level.ToString();
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
        colorAdjustments.active = !colorAdjustments.active;
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            Debug.Log("Paused");
        }
        else
            Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0)
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
