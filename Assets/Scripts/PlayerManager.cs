
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	public StartTile startTileScript;
	public GameObject startTileGameObject;
	public GameObject playerGameObject;

	public int numPlayers = 2;
	private int _currPlayer;
	public Player playerScript;

	private ArrayList playerOneInventory;
	private ArrayList playerTwoInventory;
	

	public int currPlayer { get { return _currPlayer; } 
							set { _currPlayer = value; }}
	
	enum PLAYER : int 
	{ 
		ONE = 0, 
		TWO = 1
	};

	void Start()
	{
		_currPlayer = (int)PLAYER.ONE;	// Player 1 starts off the game.

		startTileGameObject = GameObject.Find("Start Tile");
		startTileScript = startTileGameObject.GetComponent<StartTile>();

		playerGameObject = (GameObject)Resources.Load("Prefabs/Player");
		playerScript = playerGameObject.GetComponent<Player>();

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

	void Update()
	{

	}

	public void changePlayers()
	{
		if (_currPlayer == (int)PLAYER.ONE)
		{
			_currPlayer = (int)PLAYER.TWO;
		}

		else if (_currPlayer == (int)PLAYER.TWO)
		{
			_currPlayer = (int)PLAYER.ONE;
		}
	}
}







