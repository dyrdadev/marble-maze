using UnityEngine;
using System.Collections;


// Script that handles the rotation input into the marble game

public class GameBoard : MonoBehaviour {
	
	// maximum rotation angle for accelerometer input
	public float maxAngle = 60.0f;
	
	// current x and y euler angle
	float xAngle, yAngle;


    AccelerometerInput vai;

    // Use this for initialization
    void Start () {

        vai = GetComponent< AccelerometerInput > ();
		// initialize the plane as flat
		xAngle = 0;
		yAngle = 0;
	}
	
	// Update is called once per physics tick
	void FixedUpdate () {

        var accelerometerData = vai.GetDirection();

        xAngle = accelerometerData.y * maxAngle;
        yAngle = - accelerometerData.x * maxAngle;
        Quaternion q = Quaternion.Euler(xAngle, 0, yAngle);
        GetComponent<Rigidbody>().MoveRotation(q);

    }
}
