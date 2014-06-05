
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	// Private fields.
	private GameObject tileGameObject;
	private List<Tile> _connectedTiles;
	private int _numConnected;

	private float xPos;
	private float yPos;
	private int _tileNumber;		// Number instantiated.
	private int maxConnections = 6;	// Hexagon only has 6 sides.

	// Public getters.
	public GameObject tile       	 { get { return tileGameObject;} }
	public List<Tile> connectedTiles { get { return _connectedTiles; } }
	public int numConnected      	 { get { return _numConnected; } }
	public Vector3 position 	 	 { get { return new Vector3(xPos, yPos, 3); } }
	public int tileNumber 		 	 { get { return _tileNumber; } }

	// Constructor.
	public Tile(GameObject tile, float x, float y, int number)
	{
		_connectedTiles = new List<Tile>();
		tileGameObject = tile;
		xPos = x;
		yPos = y;
		_tileNumber = number;
		_numConnected = 0;
	}

	// Methods.
	public void addConnection(Tile connection)
	{
		if (!hasMaxConnections())
		{
			_connectedTiles.Add(connection);
			_numConnected++;
		}
		else
		{
			Debug.Log("This Tile has already established it's maximum number of connections.");
		}
	}

	public void removeConnection(int removeIndex)
	{
		if (_numConnected > 0)
		{
			_numConnected--;
			_connectedTiles.RemoveAt(removeIndex);
		}
	}

	public void removeConnection(Tile removeTile)
	{
		if (_numConnected > 0)
		{
			_numConnected--;
			_connectedTiles.Remove(removeTile);
		}
	}

	public bool hasMaxConnections()
	{
		return (_numConnected == maxConnections);
	}
}






