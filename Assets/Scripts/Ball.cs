using UnityEngine;
using System.Collections;

// Ball behaviour
// Basically resetting and scoring

public class Ball : MonoBehaviour {
	
	// Store the initial position of the ball in the floor coordinate system
	Vector3 position;
	
	// We need to keep a reference to the floor so we can convert beween floor 
	// system and global coordinates
	public GameObject floor;
	
	// Use this for initialization
	void Start () {
		// store the inital position of the ball in the floor coordinate system
		position = floor.transform.InverseTransformPoint(transform.position);
		// set the velocity to 0
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
	}
	
	// ball has been lost or reached the goal. reset
	void resetBall() {
		// set the velocity to 0
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		// reset the position to the inital position relative to the floor
		// the ball will always spawn in the right place on the maze, even if the
		// plane is tilted
		transform.position = floor.transform.TransformPoint(position);
	}
	
	// Update is called once per frame
	void Update () {
		// if the ball drop below the playing area, it is lost
		if (transform.position.y < -5) {			
			Player.lostBalls++;
			resetBall();
		}
	}
	
	void OnTriggerEnter(Collider collider) {

		switch (collider.gameObject.tag) {

		// if we collide with a danger object, the ball is lost and will be reset
		case "Danger":
			Player.lostBalls++;
			resetBall();
			break;
		// if we reach the goal, we score a point and the ball is reset			
		case "Goal":
			Player.score++;
			resetBall();
			break;
		default:
			break;
		}
	}
}
