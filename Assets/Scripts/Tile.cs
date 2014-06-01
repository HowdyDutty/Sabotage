
using UnityEngine;
using System.Collections;

public class Tile
{
	private GameObject _tile;
	private float xPos;
	private float yPos;

	public Vector2 position
	{
		get { return new Vector2(xPos, yPos); }
	}

	public GameObject tile 
	{
		get { return _tile;}
	}

	// Constructor
	public Tile(GameObject tile, float x, float y)
	{
		_tile = tile;
		xPos = x;
		yPos = y;
	}

	//public override boo
}
