
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	public int totalScore;

	private Player playerScript;

	void Start()
	{
		totalScore = 0;
		playerScript = FindObjectOfType<Player>();
	}

	public void getUpdatedScore(int update)
	{
		totalScore += update;
		Debug.Log(totalScore);
	}

}
