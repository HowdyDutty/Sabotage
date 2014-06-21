
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	public StartTile startTileScript;
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

	void Start()
	{
		currPlayer = (int)PLAYER.ONE;	// Player 1 starts off the game.

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

	public void changePlayer()
	{
		if (currPlayer == (int)PLAYER.ONE)
		{
			currPlayer = (int)PLAYER.TWO;
		}
		else 
		{
			currPlayer = (int)PLAYER.ONE;
		}
	}
}







