using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;
	private Vector3 position;
	// We need to keep a reference to the board so we can convert between board 
	// coordinates and global coordinates
	[SerializeField] private Transform gameBoard;
	
	void Start () {
		position = gameBoard.InverseTransformPoint(transform.position);
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
	}
	
	void ResetBall() {
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		// reset the position to the initial position relative to the game board
		// the ball will always spawn in the right place on the maze, even if the
		// plane is tilted
		transform.position = gameBoard.TransformPoint(position);
	}

	void Update () {
		// Reset the ball if it drop below the playing area.
		if (transform.position.y < -5) {			
			gameManager.LostBalls++;
			ResetBall();
		}
	}
	
	void OnTriggerEnter(Collider collider) {
		switch (collider.gameObject.tag) {
			case "Danger":
				gameManager.LostBalls++;
				ResetBall();
				break;
			case "Goal":
				gameManager.Score++;
				ResetBall();
				break;
			default:
				break;
		}
	}
}
