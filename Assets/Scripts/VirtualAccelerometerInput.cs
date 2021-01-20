using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAccelerometerInput : MonoBehaviour {

    
    private Vector3 direction;

    public Vector3 GetDirection()
    {
        return direction;
    }

    // Low Pass Filter Attributes
    private static float AccelerometerUpdateInterval  = 1.0f / 60.0f;
    private static float LowPassKernelWidthInSeconds = 0.5f;

    private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable

    private Vector3 lowPassValue = Vector3.zero;

    public void Start()
    {
        // Initialize the Low Pass Filter value
        lowPassValue = Input.acceleration;
    }

    private Vector3 LowPassFilterAccelerometer()  {
        
        float period = 0.0f;
        Vector3 acc = Vector3.zero;

        foreach (AccelerationEvent evnt in Input.accelerationEvents){
            acc += evnt.acceleration * evnt.deltaTime;
            period += evnt.deltaTime;
        }

        if (period > 0){
            acc *= 1.0f / period;
        }
        
        lowPassValue = Vector3.Lerp(lowPassValue, acc, LowPassFilterFactor);

        return lowPassValue;
    }

// Update is called once per frame
void Update () {

        var accelerometerData = LowPassFilterAccelerometer(); // Low pass filter 

        // clamp acceleration vector to the unit sphere
        if (accelerometerData.sqrMagnitude > 1)
        {
            accelerometerData.Normalize();
        }

        direction = accelerometerData;
    }

}
