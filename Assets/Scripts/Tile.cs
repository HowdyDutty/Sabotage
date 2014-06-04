
using UnityEngine;
using System.Collections;

public class Tile
{
	private GameObject _tile;
	private float xPos;
	private float yPos;

	public Vector3 position
	{
		get { return new Vector3(xPos, yPos, 3); }
	}

	public GameObject tile 
	{
		get { return _tile;}
	}

	public Tile(GameObject tile, float x, float y)
	{
		_tile = tile;
		xPos = x;
		yPos = y;
	}
}
