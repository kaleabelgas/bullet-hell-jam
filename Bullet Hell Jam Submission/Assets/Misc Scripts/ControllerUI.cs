using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI timer;


    private float timeRemaining = 10;
    private float timePerLevel = 10;

    private float timeToDisplay;

    private int level = 1;

    private void Start()
    {
        currentLevel.text = "LEVEL " + level.ToString();
    }

    public void UpdateCurrentLevel()
    {
        level++;
        currentLevel.text = "LEVEL " + level.ToString();
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
