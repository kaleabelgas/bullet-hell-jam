using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject tutorial;
    [SerializeField] Slider slider;

    private float timeRemaining = 10;
    private float timePerLevel = 10;

    private float timeToDisplay;

    private int level = 0;

    private void Start()
    {
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
