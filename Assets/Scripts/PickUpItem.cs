
using UnityEngine;
using System.Collections;

public class PickUpItem 
{
	public GameObject itemGameObject {get; private set;}
	public Tile occupiedTile {get; private set;}
	public Vector3 position {get; private set;}
	public int tileNumber {get; private set;}

	public PickUpItem(GameObject itemGameObject, Tile tile, int tileNumber)
	{
		this.itemGameObject = itemGameObject;
		occupiedTile = tile;
		position = tile.position;
		this.tileNumber = tileNumber;
	}

}
