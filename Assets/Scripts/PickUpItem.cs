
using UnityEngine;
using System.Collections;

public class PickUpItem 
{
	public GameObject itemGameObject {get; private set;}
	public Tile occupiedTile {get; private set;}
	public Vector3 position {get; private set;}
	public int tileNumber {get; private set;}
	public int typeOfPickUp {get; private set;}

	public enum PickUpType : int
	{
		GAMEBOARD_ITEM  = 0,
		GOD_ACTION		= 1	
	};

	public PickUpItem(GameObject itemGameObject, Tile tile, int tileNumber)
	{
		this.itemGameObject = itemGameObject;
		this.occupiedTile = tile;
		this.position = tile.position;
		this.tileNumber = tileNumber;
	}

}
