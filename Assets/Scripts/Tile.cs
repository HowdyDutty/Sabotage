
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	// Private fields.
	private GameObject tileGameObject;
	private ArrayList _connectedTiles;
	private int _numConnected;

	private float xPos;
	private float yPos;
	private int _tileNumber;		// Number instantiated.
	private int maxConnections = 6;	// Hexagon only has 6 sides.

	private bool _hasPlayer;
	private bool _isBlocked;


	// Public getters.
	public GameObject tile       	 { get { return tileGameObject;} }
	public ArrayList connectedTiles { get { return _connectedTiles; } }
	public int numConnected      	 { get { return _numConnected; } }
	public Vector3 position 	 	 { get { return new Vector3(xPos, yPos, 2.5f); } }
	public int tileNumber 		 	 { get { return _tileNumber; } }
	public bool hasPlayer 			 { get { return _hasPlayer; } 
									   set { _hasPlayer = value; } }
	public bool isBlocked			 { get { return _isBlocked; } }

	// Constructor.
	public Tile(GameObject tile, float x, float y, int number) 
	{
		_connectedTiles = new ArrayList();
		tileGameObject = tile;
		xPos = x;
		yPos = y;
		_tileNumber = number;
		_numConnected = 0;
		_hasPlayer = false;
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

	public void setBlocked()
	{
		if (!_isBlocked)
		{
			_isBlocked = true;
			tileGameObject.renderer.material.color = Color.red;
			Debug.Log("Tile " + tileNumber + " is now blocked.");
		}
		else 
		{
			_isBlocked = false;
			tileGameObject.renderer.material.color = Color.white;
			Debug.Log("Tile " + tileNumber + " is now unblocked.");
		}
	}
}






