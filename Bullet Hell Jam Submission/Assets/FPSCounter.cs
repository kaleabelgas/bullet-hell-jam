using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsCounter;

    private float newValue; //this is an example for the value to be averaged
    private const int MovingAverageLength = 3; //made public in case you want to change it in the Inspector, if not, could be declared Constant

    private int count;
    private float movingAverage;

    private const float defaultTimer = 0.3f;
    private float timer;


    // Update is called once per frame
    void Update()
    {
        newValue = 1f / Time.unscaledDeltaTime;
        count++;

        //This will calculate the MovingAverage AFTER the very first value of the MovingAverage
        if (count > MovingAverageLength)
        {
            movingAverage = movingAverage + (newValue - movingAverage) / (MovingAverageLength + 1);

            Debug.Log("Moving Average: " + movingAverage); //for testing purposes
            //DisplayFPS();

        }
        else
        {
            //NOTE: The MovingAverage will not have a value until at least "MovingAverageLength" values are known (10 values per your requirement)
            movingAverage += newValue;

            //This will calculate ONLY the very first value of the MovingAverage,
            if (count == MovingAverageLength)
            {
                movingAverage /= count;
                //Debug.Log("Moving Average: " + movingAverage); //for testing purposes
            }
            //DisplayFPS();
        }

        timer -= Time.unscaledDeltaTime;
        if(timer <= 0)
        {
            timer = defaultTimer;
            DisplayFPS();
        }
    }
private void DisplayFPS()
    {
        fpsCounter.text = $"{movingAverage}";
        Debug.Log(movingAverage);
    }
}
