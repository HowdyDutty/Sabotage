
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	public int totalScore;

	private Player playerScript;
	private PlayerManager playerManagerScript;
	private int currPlayer;
	private int[] playerScores;	// This holds the scores of both players.

	void Start()
	{
		playerScores = new int[2];
		totalScore = 0;
		playerScript = FindObjectOfType<Player>();
		playerManagerScript = FindObjectOfType<PlayerManager>();
	}

	public void getUpdatedScore(int update)
	{
		totalScore += update;
		Debug.Log(totalScore);
	}

}
