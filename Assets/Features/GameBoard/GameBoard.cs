using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour
{
    public float maxAngle = 60.0f;
    private float xAngle, yAngle;
    private AccelerometerInput vai;

    private void Start()
    {
        vai = GetComponent<AccelerometerInput>();
        xAngle = 0;
        yAngle = 0;
    }

    private void FixedUpdate()
    {
        var accelerometerData = vai.GetDirection();
        xAngle = accelerometerData.y * maxAngle;
        yAngle = -accelerometerData.x * maxAngle;
        var q = Quaternion.Euler(xAngle, 0, yAngle);
        GetComponent<Rigidbody>().MoveRotation(q);
    }
}