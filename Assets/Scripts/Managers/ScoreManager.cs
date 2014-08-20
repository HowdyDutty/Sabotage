
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	private PlayerManager playerManagerScript;
	private HUDManager HUDManagerScript;
	private int currPlayer;
	public int[] playerScores	;// This holds the scores of both players.

	public int scoreToWin = 1000;

	void Start()
	{
		currPlayer = 0;
		playerScores = new int[2];
		playerManagerScript = FindObjectOfType<PlayerManager>();
		HUDManagerScript    = FindObjectOfType<HUDManager>();
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
		//HUDManagerScript.updateHUD(currPlayer+1, playerScores[currPlayer]);
		Debug.Log("Player " + currPlayer + " score is " + playerScores[currPlayer]);

		if (playerScores[currPlayer] == scoreToWin)
		{
			endGame(playerScores[currPlayer]);
		}
	}

	public int getScore()
	{
		return playerScores[currPlayer];
	}

	private void endGame(int player)
	{
		// Go to finish screen.
		Debug.Log("Player " + player + " has won!");
	}
}
