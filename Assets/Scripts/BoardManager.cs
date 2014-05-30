
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public GameObject tile;
	public int gridWidth   = 10;
	public int gridHeight  = 10;

	private int tileWidth  = 116;
	private int tileHeight = 116;

	private Transform myTransform;
	private Quaternion myRotation;

	void Start () 
	{
		myTransform = this.transform;
		myRotation = this.transform.rotation;
		createBoard();
	}

	void createBoard() 
	{
		List<GameObject> tiles = new List<GameObject>();
		Vector3 currTileLocation = new Vector3(0, 0, 0);
		tile.transform.localScale = new Vector3(tileWidth, 1, tileHeight);

		for (int i = -10; i < gridWidth/2; i++)
		{	
			for (int j = -10; j < gridHeight/2; j++)
			{
				GameObject currTile = (GameObject)Instantiate(tile, currTileLocation, Quaternion.Euler(270, 0, 0));
				tiles.Add(currTile);
				currTileLocation.y += j;
			}
			currTileLocation.x += i;
			currTileLocation.y = 0;
		}
	}
} // BoardManager
