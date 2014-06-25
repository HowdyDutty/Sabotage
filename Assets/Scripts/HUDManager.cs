
using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	public GUIText currentPlayer;
	public GUIText playerScore;

	public ScoreManager scoreManagerScript;
	public PlayerManager playerManagerScript;

	void Start() 
	{
		currentPlayer = new GUIText();
		playerScore   = new GUIText();

		scoreManagerScript = FindObjectOfType<ScoreManager>();
		playerManagerScript = FindObjectOfType<PlayerManager>();

		createHUD();
	}

	private void createHUD()
	{

	}

	private void updateHUD()
	{

	}
}
