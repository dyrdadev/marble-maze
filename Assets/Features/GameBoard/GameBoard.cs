using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour {
	public float maxAngle = 60.0f;
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
