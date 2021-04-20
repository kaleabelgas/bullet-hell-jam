using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour
{
    [SerializeField] private Transform pulse;
    [SerializeField] private float rangeMax = 100;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float minSize;

    private float currentRange;

    public bool PulseDone { get; private set; } = false;
    

    private void Update()
    {
        if (currentRange < rangeMax)
        {
            currentRange += pulseSpeed * Time.deltaTime;
            pulse.localScale = new Vector3(currentRange, currentRange);
            PulseDone = false;
        }
        else
        {
            pulse.localScale = new Vector3(minSize, minSize);
            PulseDone = true;
        }
    }


    public void DoPulse()
    {
        currentRange = minSize;
    }
}
