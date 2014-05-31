﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]	// TODO: Get rid of this.
public class BoardManager : MonoBehaviour 
{
	public GameObject tile;
	public int gridWidth     = 10;
	public int gridHeight    = 10;

	private float tileWidth  = 116;	// Weird number that seems to work with the Model.
	private float tileHeight = 116;
	private Vector3 initPos;
	private GameObject _tiles;

	public GameObject tiles
	{
		get { return _tiles; }
	}

	void Start() 
	{
		initPos = new Vector3(-(tileWidth*gridWidth/2f) + (tileWidth/2), 
		                      gridHeight/(2f*tileHeight) - (tileHeight/2), 0);
		_tiles = new GameObject("Grid");
		createBoard();
	}

	private void createBoard() 
	{
		tile.transform.localScale = new Vector3(tileWidth, 1, tileHeight);
		for (int y = 0; y < gridHeight; y++)
		{	
			for (int x = 0; x < gridWidth; x++)
			{
				Vector2 tileNumber = new Vector2(x,y);
				Vector3 tilePosition = calcWorldCoord(tileNumber);
				GameObject currTile = (GameObject)Instantiate(tile, tilePosition, Quaternion.Euler(270, 0, 0));
				currTile.transform.parent = _tiles.transform;
			}
		}
	}
																								
	private Vector3 calcWorldCoord(Vector2 gridPos)											
	{																							
		float offset = 0;		
		if (gridPos.y % 2 != 0)		
		{
			offset = tileWidth / 2;																	
		}

		// These are random constants that seem to work the way I want them too. Don't judge.
		float x = (initPos.x + offset + (gridPos.x * tileWidth)) / 112;										
		float y = (initPos.y - (gridPos.y * tileHeight * 0.87f)) / 112;										

		return new Vector3(x, y, 3);																
	}	
																									
} // BoardManager




