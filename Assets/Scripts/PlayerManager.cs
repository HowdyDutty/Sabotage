
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	public StartTile startTileScript;
	public ScoreManager scoreManagerScript;
	public GameObject startTileGameObject;
	public GameObject playerGameObject;

	private ArrayList playerOneInventory = new ArrayList();
	private ArrayList playerTwoInventory = new ArrayList();

	public int currGameBoardPlayer { get; private set; }
	public int activePlayer		   { get; private set; }
	
	enum Player : int 
	{ 
		ONE = 0, 
		TWO = 1
	};

	public enum PlayerType : int
	{
		GAMEBOARD = 0,
		GOD 	  = 1
	};

	void Start()
	{
		currGameBoardPlayer = (int)Player.ONE;				// Player One starts off the game.
		activePlayer 	    = (int)PlayerType.GAMEBOARD;	// The GameBoard Player starts playing first.
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
	}

	// Get the correct inventory system for whichever player is 
	// currently on the board.
	public ArrayList getInventory()
	{
		if (currGameBoardPlayer == (int)Player.ONE)
		{
			return playerOneInventory;
		}
		else
		{
			return playerTwoInventory;
		}
	}

	// This is when the GameBoard Player either dies or makes it
	// to the end of the grid and the roles are reversed.
	public void changePlayer()
	{
		Debug.Log("Players have changed.");
		scoreManagerScript.changePlayer();

		if (currGameBoardPlayer == (int)Player.ONE)
		{
			currGameBoardPlayer = (int)Player.TWO;
		}
		else 
		{
			currGameBoardPlayer = (int)Player.ONE;
		}
	}

	// This is when the 'God' Player gets to use abilities.
	public void changeTurn()
	{
		Debug.Log("Turns have changed.");
		if (activePlayer == (int)PlayerType.GAMEBOARD)
		{
			activePlayer = (int)PlayerType.GOD;
		}
		else
		{
			activePlayer = (int)PlayerType.GAMEBOARD;
		}
	}
}







