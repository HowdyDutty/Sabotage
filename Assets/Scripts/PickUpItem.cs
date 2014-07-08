
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
		this.occupiedTile = tile;
		this.position = tile.position;
		this.tileNumber = tileNumber;

		generateOptions();
	}

	// Proceedurly generates the two possible items this can become at runtime.
	private void generateOptions()
	{

	}

	// Applys this Pick Up items' effect onto the player holding it.
	public void applyEffect()
	{

	}
}
