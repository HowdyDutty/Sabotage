
/*
 * Written By: Roman Larionov
 * Script: BoardManager.cs
 * 
 * Attached to GameManager GameObject. 
 * 
 * !! This script is added to the GameManager GameObject through code !!
 * 
 * This script controls the Game Board. The various
 * things that it does are: create the tile grid,
 * instantiate all of the tiles, add scripts and 
 * components to the tiles.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour 
{
	public GameObject tile;
	public int gridWidth     = 10;
	public int gridHeight    = 10;

	private float tileWidth  = 116;	// Weird number that seems to work with the Model.
	private float tileHeight = 116;
	private Vector3 initPos;
	private GameObject tileGrid;
	private List<Tile> _tiles;

	public List<Tile> tiles
	{
		get { return _tiles; }
	}

	void Start() 
	{
		tile     = (GameObject)Resources.Load("Prefabs/HexGrid");
		_tiles   = new List<Tile>();
		tileGrid = new GameObject("Grid");
		initPos  = new Vector3(-(tileWidth*gridWidth/2f) + (tileWidth/2), 
		                      gridHeight/(2f*tileHeight) - (tileHeight/2), 0);

		createBoard();
		createConnections();
	}

	private void createBoard() 
	{
		tile.transform.localScale = new Vector3(tileWidth, 1, tileHeight);
		int tileCounter = 0;

		for (int y = 0; y < gridHeight; y++)
		{	
			for (int x = 0; x < gridWidth; x++)
			{
				tileCounter++;
				Vector3 tilePosition = calcWorldCoord(x,y);

				GameObject currGameObject = (GameObject)Instantiate(tile, tilePosition, Quaternion.Euler(270, 0, 0));
				Tile currTile = new Tile(currGameObject, tilePosition.x, tilePosition.y, tileCounter);

				currGameObject.name = "Tile";
				currGameObject.AddComponent<SphereCollider>().isTrigger = true;
				currGameObject.GetComponent<SphereCollider>().radius = 0.00432f;

				_tiles.Add(currTile);
				currGameObject.transform.parent = tileGrid.transform;	// Becomes a child of tileGrid.

				if (tileCounter == 1)
				{
					currGameObject.AddComponent<StartTile>();
					currGameObject.name = "Start Tile";
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
		foreach (Tile t in _tiles)
		{
			foreach (Tile s in _tiles)
			{
				if (t.hasMaxConnections())
				{
					break;
				}
				// Don't be your own connection.
				if (s.tile.Equals(t.tile))
				{
					continue;
				}
				if (isConnected(t, s))
				{
					t.addConnection(s);
				}
			}
		}
	}

	private bool isConnected(Tile start, Tile end) 
	{
		return (Vector3.Distance(end.position, start.position) <= 1.4f);
	}

																						
} // BoardManager










