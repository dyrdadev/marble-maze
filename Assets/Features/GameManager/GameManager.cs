using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text scoreUI, ballsUI;

    public int Score
	{
		get => score;
		set
		{
			score = value;
			scoreUI.text = "" + score;
		}
	}
	private int score = 0;


	public int LostBalls
	{
		get => lostBalls;
		set
		{
			lostBalls = value;
			ballsUI.text = "" + lostBalls;
		}
	}
	private int lostBalls = 0;
}
