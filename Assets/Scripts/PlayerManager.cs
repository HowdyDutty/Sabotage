
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	public StartTile startTileScript;
	public ScoreManager scoreManagerScript;
	public GameObject startTileGameObject;
	public GameObject playerGameObject;

	public int numPlayers = 2;

	private ArrayList playerOneInventory = new ArrayList();
	private ArrayList playerTwoInventory = new ArrayList();

	public int currPlayer { get; private set;}
	
	enum PLAYER : int 
	{ 
		ONE = 0, 
		TWO = 1
	};

	enum PLAYER_TURN : int
	{
		GAMEBOARD = 0,
		GOD 	  = 1
	};

	void Start()
	{
		currPlayer = (int)PLAYER.ONE;	// Player 1 starts off the game.
		scoreManagerScript = FindObjectOfType<ScoreManager>();

		startTileGameObject = GameObject.Find("Start Tile");
		startTileScript = startTileGameObject.GetComponent<StartTile>();

		playerGameObject = (GameObject)Resources.Load("Prefabs/Player");

		Vector3 playerSpawnLocation = startTileGameObject.transform.position + new Vector3(0, 0, -0.5f);
		GameObject instantiatedPlayer = (GameObject)Instantiate(playerGameObject, 
		                                                        playerSpawnLocation, 
		                                                        startTileGameObject.transform.rotation);
		
		instantiatedPlayer.transform.localScale = new Vector3(.5f, .5f, .5f);
		
		playerGameObject = instantiatedPlayer.gameObject;
		playerGameObject.AddComponent<Player>();
		playerGameObject.AddComponent<MouseMovement>();
		playerGameObject.GetComponent<BoxCollider>().isTrigger = true;
		playerGameObject.AddComponent<Rigidbody>().useGravity = false;
	}

	// Get the correct inventory system for whichever player is 
	// currently on the board.
	public ArrayList getInventory()
	{
		if (currPlayer == (int)PLAYER.ONE)
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
		scoreManagerScript.changePlayer();

		if (currPlayer == (int)PLAYER.ONE)
		{
			currPlayer = (int)PLAYER.TWO;
		}
		else 
		{
			currPlayer = (int)PLAYER.ONE;
		}
	}

	// This is when the 'God' Player gets to use abilities.
	public void changeTurn()
	{

	}
}







