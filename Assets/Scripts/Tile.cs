
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	private int maxConnections = 6;	// Hexagon only has 6 sides.
	public GameObject tile       	 { get; private set;}
	public ArrayList connectedTiles  { get; set;}
	public int numConnected      	 { get; set;}
	public Vector3 position 	 	 { get; private set;}
	public int tileNumber 		 	 { get; private set;}
	public bool hasPlayer 			 { get; set;}
	public bool isBlocked			 { get; private set;}

	// Constructor.
	public Tile(GameObject tile, float x, float y, int number) 
	{
		connectedTiles = new ArrayList();
		position = new Vector3(x, y, 2.5f);
		this.tile = tile;
		tileNumber = number;
		numConnected = 0;
		hasPlayer = false;
	}

	// Methods.
	public void addConnection(Tile connection)
	{
		if (!hasMaxConnections())
		{
			connectedTiles.Add(connection);
			numConnected++;
		}
		else
		{
			Debug.Log("This Tile has already established it's maximum number of connections.");
		}
	}

	public void removeConnection(int removeIndex)
	{
		if (numConnected > 0)
		{
			numConnected--;
			connectedTiles.RemoveAt(removeIndex);
		}
	}

	public void removeConnection(Tile removeTile)
	{
		if (numConnected > 0)
		{
			numConnected--;
			connectedTiles.Remove(removeTile);
		}
	}

	public bool hasMaxConnections()
	{
		return (numConnected == maxConnections);
	}

	public void setBlocked()
	{
		if (!isBlocked)
		{
			isBlocked = true;
			tile.renderer.material.color = Color.red;
		}
		else 
		{
			isBlocked = false;
			tile.renderer.material.color = Color.white;
		}
	}
}






