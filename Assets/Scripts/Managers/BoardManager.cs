
/*
 * Written By: Roman Larionov
 * Script: BoardManager.cs
 * 
 * Attached to GameManager GameObject. 
 * 
 * !! This script is added to the GameManager GameObject through code in the GameManager Script!!
 * 
 * This script controls the Game Board. The various
 * things that it does are: create the tile grid,
 * instantiate all of the tiles, add scripts and 
 * components to the tiles, and saves all connected
 * tiles in the tile objects.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoardManager : MonoBehaviour 
{
	public int gridWidth     = 10;
	public int gridHeight    = 10;
	private float tileWidth  = 116;	// fit to local scale of TileGrid Parent GameObject.
	private float tileHeight = 116;

	public GameObject tileGameObject;
	private Vector3 initPos;
	private GameObject tileGrid;
	private List<Tile> _tiles;
	public List<Tile> tiles { get { return _tiles; } }

	void Start() 
	{
		tileGameObject = (GameObject)Resources.Load("Prefabs/HexGrid");
		tileGameObject.transform.localScale = new Vector3(tileWidth, 1, tileHeight);

		_tiles   = new List<Tile>();
		tileGrid = new GameObject("Grid");
		initPos  = new Vector3(-(tileWidth*gridWidth/2f) + (tileWidth/2), 
		                      gridHeight/(2f*tileHeight) - (tileHeight/2), 0);

		createBoard();
		createConnections();
	}

	private void createBoard() 
	{
		int tileCounter = 0;
		for (int y = 0; y < gridHeight; y++)
		{	
			for (int x = 0; x < gridWidth; x++)
			{
				tileCounter++;
				Vector3 currTilePosition = calcWorldCoord(x,y);

				GameObject currGameObject = (GameObject)Instantiate(tileGameObject, currTilePosition, Quaternion.Euler(270, 0, 0));
				Tile currTile = new Tile(currGameObject, currTilePosition.x, currTilePosition.y, tileCounter);

				currGameObject.name = "Tile";
				currGameObject.tag = "Tile";
				currGameObject.AddComponent<SphereCollider>().isTrigger = true;
				currGameObject.GetComponent<SphereCollider>().radius = 0.00432f;

				_tiles.Add(currTile);
				currGameObject.transform.parent = tileGrid.transform;	// Becomes a child of tileGrid.

				if (tileCounter == 1)
				{
					currGameObject.AddComponent<StartTile>();
					currGameObject.name = "Start Tile";
					currTile.hasPlayer = true;
				}
				else if(tileCounter == (gridWidth * gridHeight))
				{
					currGameObject.AddComponent<FinishTile>();
					currGameObject.name = "Finish Tile";
				}
			}
		}
	}

	private Vector3 calcWorldCoord(int gridPosX, int gridPosY)											
	{																							
		float offset = 0;		
		if (gridPosY % 2 != 0)		
		{
			offset = tileWidth / 2;																	
		}

		// These use some random constants that seem to make this work the way I want it too. Don't judge.
		float x = (initPos.x + offset + (gridPosX * tileWidth)) / 112;										
		float y = (initPos.y - (gridPosY * tileHeight * 0.87f)) / 112;										

		return new Vector3(x, y, 3);																
	}	

	private void createConnections()
	{
		foreach (Tile currTile in _tiles)
		{
			foreach (Tile potentialConnection in _tiles)
			{
				if (currTile.hasMaxConnections())
				{
					break;
				}
				// Can't be your own connection.
				if (potentialConnection.tile == currTile.tile)
				{
					continue;
				}
				if (isConnected(currTile, potentialConnection))
				{
					currTile.addConnection(potentialConnection);
				}
			}
		}
	}

	private bool isConnected(Tile start, Tile end) 
	{
		// 1.37 is the distance between the centers of two Tiles.
		return (Vector3.Distance(end.position, start.position) <= 1.4f);	
	}
} // BoardManager










