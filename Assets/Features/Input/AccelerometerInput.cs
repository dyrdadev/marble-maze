using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour {
    public Vector3 GetDirection()
    {
        return direction;
    }
    private Vector3 direction;
    
    private static float AccelerometerUpdateInterval  = 1.0f / 60.0f;
    private static float LowPassKernelWidthInSeconds = 0.5f;
    private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; 

    private Vector3 lowPassValue = Vector3.zero;

    public void Start()
    {
        lowPassValue = Input.acceleration;
    }

    void Update () {
        float period = 0.0f;
        Vector3 acc = Vector3.zero;

        foreach (AccelerationEvent evnt in Input.accelerationEvents){
            acc += evnt.acceleration * evnt.deltaTime;
            period += evnt.deltaTime;
        }

        if (period > 0){
            acc *= 1.0f / period;
        }
            
        // Apply lowpass filter.
        lowPassValue = Vector3.Lerp(lowPassValue, acc, LowPassFilterFactor);
        
        var accelerometerData = lowPassValue;

        // clamp acceleration vector to the unit sphere
        if (accelerometerData.sqrMagnitude > 1)
        {
            accelerometerData.Normalize();
        }

        direction = accelerometerData;
    }

}
