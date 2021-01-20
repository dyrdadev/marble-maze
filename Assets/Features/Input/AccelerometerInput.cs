using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour {
    public Vector3 GetDirection()
    {
        return direction;
    }
    private Vector3 direction;
    
    public void Start()
    {
        direction = Vector3.zero;
    }

    void Update () {
        // ...
    }

}
