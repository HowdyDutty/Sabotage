
using UnityEngine;
using System.Collections;

public class Tile
{
	private GameObject _tile;
	private Tile[] _connectedTiles;
	private float xPos;
	private float yPos;

	public GameObject tile 
	{
		get { return _tile;}
	}

	public Tile[] connectedTiles
	{
		get { return _connectedTiles; }
		set { _connectedTiles = value; }
	}

	public Vector3 position
	{
		get { return new Vector3(xPos, yPos, 3); }
	}

	public Tile(GameObject tile, float x, float y)
	{
		_tile = tile;
		xPos = x;
		yPos = y;
	}
}
