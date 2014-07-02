
using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	private GameObject currentPlayerGameObject;
	private GameObject playerScoreGameObject;
	public GameObject currentPlayer;
	public GameObject playerScore;

	public ScoreManager scoreManagerScript;
	public PlayerManager playerManagerScript;

	void Start() 
	{
		scoreManagerScript  = FindObjectOfType<ScoreManager>();
		playerManagerScript = FindObjectOfType<PlayerManager>();

		currentPlayerGameObject = new GameObject();
		playerScoreGameObject   = new GameObject();
		currentPlayerGameObject.AddComponent<GUIText>();
		playerScoreGameObject.AddComponent<GUIText>();

		currentPlayerGameObject.name = "A";
		playerScoreGameObject.name = "B";

		Vector3 cameraPosition    = Camera.main.transform.localPosition;
		Quaternion cameraRotation = Camera.main.transform.rotation;

		currentPlayer = (GameObject)Instantiate(currentPlayerGameObject, 
		                                        cameraPosition + new Vector3(-1f, 1f, 0), 
		                                        cameraRotation);
		playerScore   = (GameObject)Instantiate(playerScoreGameObject, 
		                                        cameraPosition + new Vector3(1f, 1f, 0), 
		                                        cameraRotation);

		int updatedPlayer = playerManagerScript.currGameBoardPlayer + 1;
		int updatedScore  = scoreManagerScript.getScore();

		updateHUD(updatedPlayer, updatedScore);
	}

	public void updateHUD(int playerText, int scoreText)
	{
		currentPlayer.GetComponent<GUIText>().text = "Player: " + playerText;
		playerScore.GetComponent<GUIText>().text   = "Score: "  + scoreText;
	}
}
