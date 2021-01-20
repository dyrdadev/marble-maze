using System;
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
			SetScoreLabel("" + score);
		}
	}
	private int score = 0;


	public int LostBalls
	{
		get => lostBalls;
		set
		{
			lostBalls = value;
			SetLostBallLabel(""+lostBalls);
		}
	}
	private int lostBalls = 0;

	void SetScoreLabel(string text)
	{
		scoreUI.text = text;
	}
	
	void SetLostBallLabel(string text)
	{
		ballsUI.text = text;
	}

	private void Start()
	{
		Score = 0;
		LostBalls = 0;
	}
}
