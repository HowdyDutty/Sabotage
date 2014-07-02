
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	private PlayerManager playerManagerScript;
	private int currPlayer;
	public int[] playerScores	;// This holds the scores of both players.

	void Start()
	{
		currPlayer = 0;
		playerScores = new int[2];
		playerManagerScript = FindObjectOfType<PlayerManager>();
	}

	public void changePlayer()
	{
		if (currPlayer == 0)
		{
			currPlayer = 1;
		}
		else
		{
			currPlayer = 0;
		}
	}

	public void updateScore(int update)
	{
		playerScores[currPlayer] += update;
		Debug.Log("Player " + currPlayer + " score is " + playerScores[currPlayer]);
	}

	public int getScore()
	{
		return playerScores[currPlayer];
	}
}
