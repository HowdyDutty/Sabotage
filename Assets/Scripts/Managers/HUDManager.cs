using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	public float distance = 0.27f;
	
	public GameObject currentPlayer;
	public GameObject playerScore;
	
	GameObject instantiatedCurrentPlayer;
	GameObject instantiatedPlayerScore;
	
	public ScoreManager scoreManagerScript;
	public PlayerManager playerManagerScript;
	
	void Start() 
	{
		scoreManagerScript  = FindObjectOfType<ScoreManager>();
		playerManagerScript = FindObjectOfType<PlayerManager>();
		
		currentPlayer = (GameObject)Resources.Load("Prefabs/HUD/CurrentPlayer");
		playerScore   = (GameObject)Resources.Load("Prefabs/HUD/PlayerScore");
		
		instantiatedCurrentPlayer = (GameObject)Instantiate(currentPlayer);
		instantiatedPlayerScore   = (GameObject)Instantiate(playerScore);
		
		instantiatedCurrentPlayer.GetComponent<GUIText>().color = Color.green;
		instantiatedPlayerScore.GetComponent<GUIText>().color   = Color.green;
		
		instantiatedCurrentPlayer.transform.parent = Camera.main.transform;
		instantiatedPlayerScore.transform.parent   = Camera.main.transform;
		
		instantiatedCurrentPlayer.transform.localPosition += new Vector3(-distance, 1.8f *distance, 0);
		instantiatedPlayerScore.transform.localPosition   += new Vector3(distance, 1.8f *distance, 0);
		
		int updatedPlayer = playerManagerScript.currGameBoardPlayer + 1;
		int updatedScore  = scoreManagerScript.getScore();
		
		updateHUD(updatedPlayer, updatedScore);
	}
	
	public void updateHUD(int playerText, int scoreText)
	{
		instantiatedCurrentPlayer.GetComponent<GUIText>().text = "Player: " + playerText;
		instantiatedPlayerScore.GetComponent<GUIText>().text   = "Score: "  + scoreText;
	}
}