using UnityEngine;
using System.Collections;


// Script that handles the rotation input into the marble game

public class GameBoard : MonoBehaviour {
	
	// maximum rotation angle for accelerometer input
	public float maxAngle = 60.0f;
	
	// current x and y euler angle
	float xAngle, yAngle;


    AccelerometerInput accelerometerInput;

    // Use this for initialization
    void Start () {

        accelerometerInput = GetComponent<AccelerometerInput>();
        
		xAngle = 0;
		yAngle = 0;
	}
	
	// FixedUpdate is called once per physics tick
	void FixedUpdate () {
		// ...
	}
}
