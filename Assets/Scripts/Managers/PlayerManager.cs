
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sabotage.Enums;

public class PlayerManager : MonoBehaviour 
{
	public StartTile startTileScript;
	public ScoreManager scoreManagerScript;
	public GameObject startTileGameObject;
	public GameObject playerGameObject;

	// Inventorys
	private ArrayList playerOneGameBoardInventory;
	private ArrayList playerOneGodInventory;
	private ArrayList playerTwoGameBoardInventory;
	private ArrayList playerTwoGodInventory;

	public int maxGameBoardItems {get; private set;}
	public int maxGodItems {get; private set;}

	public int currGameBoardPlayer { get; private set; }
	public int activePlayer		   { get; private set; }
	
	void Start()
	{
		currGameBoardPlayer = (int)Sabotage.Enums.Player.ONE;				// Player One starts off the game.
		activePlayer 	    = (int)Sabotage.Enums.PlayerType.GAMEBOARD;	// The GameBoard Player starts playing first.
		scoreManagerScript  = FindObjectOfType<ScoreManager>();

		startTileGameObject = GameObject.Find("Start Tile");
		startTileScript     = startTileGameObject.GetComponent<StartTile>();

		playerGameObject    = (GameObject)Resources.Load("Prefabs/Player");

		Vector3 playerSpawnLocation   = startTileGameObject.transform.position + new Vector3(0, 0, -0.5f);
		GameObject instantiatedPlayer = (GameObject)Instantiate(playerGameObject, 
		                                                        playerSpawnLocation, 
		                                                        startTileGameObject.transform.rotation);
		
		instantiatedPlayer.transform.localScale = new Vector3(.5f, .5f, .5f);
		
		playerGameObject = instantiatedPlayer.gameObject;
		playerGameObject.AddComponent<GameBoardPlayer>();
		playerGameObject.AddComponent<MouseMovement>();
		playerGameObject.GetComponent<BoxCollider>().isTrigger = true;
		playerGameObject.AddComponent<Rigidbody>().useGravity = false;
	
		playerOneGameBoardInventory = new ArrayList();
		playerOneGodInventory		= new ArrayList();
		playerTwoGameBoardInventory = new ArrayList();
		playerTwoGodInventory		= new ArrayList();

		maxGameBoardItems = 5;
		maxGodItems = 10;
	}

	public ArrayList getGameBoardInventory()
	{
		return (currGameBoardPlayer == (int)Player.ONE) ? playerOneGameBoardInventory : playerTwoGameBoardInventory;
	}

	public ArrayList getGodInventory()
	{
		return (currGameBoardPlayer == (int)Player.ONE) ? playerOneGodInventory : playerTwoGodInventory;
	}

	// This is when the 'God' Player gets to use abilities.
	public void changeTurn()
	{
		Debug.Log("Turns have changed.");
		activePlayer = (activePlayer == (int)PlayerType.GAMEBOARD) ? (int)PlayerType.GOD : (int)PlayerType.GAMEBOARD;
	}

	// This is when the GameBoard Player either dies or makes it to the end.
	public void changeRound()
	{
		Debug.Log("Players have changed.");
		scoreManagerScript.changeRound();
		currGameBoardPlayer = (currGameBoardPlayer == (int)Player.ONE) ? (int)Player.TWO : (int)Player.ONE;
	}

	public void stopPlayers()
	{
		// Switch to end game scene.
	}
}







