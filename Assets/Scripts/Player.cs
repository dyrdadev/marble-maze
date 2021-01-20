using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text scoreUI, ballsUI;
	// Very simple player script to display score and lost balls
	// Values will be updated from the ball script
	
	public static int score = 0;
	public static int lostBalls = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void FixedUpdate() {
		// Just show two new labels with score and lost ball count
		scoreUI.text = "Score: " + score;
		ballsUI.text = "Lost balls: " + lostBalls;
	}
}
