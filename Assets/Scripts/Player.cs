
/*
 * Written By: Roman Larionov
 * Script: Player.cs
 * 
 * Attached to Player GameObject. 
 * 
 * !! This script is added to the Player GameObject through code in the Start Tile Script !!
 * 
 * This script controls the Player and all of the attributes about 
 * it, including: health, movement, location tracking, and more, soon.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	public float movementSpeed = 3f;
	public float tilesPerSecond = 1.5f;
	public float rotationSpeed = 10f;

	private MouseMovement mouseMovementScript;
	private BoardManager boardManagerScript;
	private IList<Tile> tileList;

	private Transform myTransform;
	private bool headingToTile = false;
	private Tile occupiedTile;

	enum rotation : int 
	{
		RIGHT 		= 0,
		LEFT  		= 180,
		LOWER_LEFT  = 240,
		LOWER_RIGHT = 300,
		UPPER_LEFT  = 120,
		UPPER_RIGHT = 60
	};

	void Start()
	{
		mouseMovementScript = this.GetComponent<MouseMovement>();
		boardManagerScript = FindObjectOfType<BoardManager>();
		tileList = boardManagerScript.tiles;

		this.renderer.material.color = Color.black;
		myTransform = this.transform;
		myTransform.rotation = Quaternion.Euler(0, 0, 300);	// Starting rotation.
	}
	
	void Update()
	{
		if (mouseMovementScript.tileFound && !headingToTile)
		{
			headingToTile = true;
			float tick = Time.time + 400000;
			
			Path<Tile> shortestPath = findShortestPath(occupiedTile, mouseMovementScript.hitTile);
			StartCoroutine(pathCoroutine(shortestPath));
		}
	}

	// Saves the tile that the player is currently occupying.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Tile"))
		{
			GameObject otherGameObject = other.gameObject;
			foreach (Tile t in tileList)
			{
				if (t.tile == otherGameObject)
				{
					occupiedTile = t;
				}
			}
		}
	}

	//-------------------------------------Movement-----------------------------------------//

	// A* algorithm to find shortest path to desired tile.
	private Path<Tile> findShortestPath(Tile start, Tile end)
	{
		PriorityQueue<int, Path<Tile>> open = new PriorityQueue<int, Path<Tile>>();
		HashSet<Tile> closed = new HashSet<Tile>();
		open.Enqueue(0, new Path<Tile>(start));

		while (!open.isEmpty())
		{
			var path = open.Dequeue();
			if (closed.Contains(path.LastStep))
			{
				continue;
			}
			if (path.LastStep.Equals(end))
			{
				return path;
			}
			closed.Add(path.LastStep);
			foreach (Tile t in path.LastStep.connectedTiles)
			{
				if (t.isBlocked)
				{
					closed.Add(t);
					continue;
				}

				int dist = 1;  //calcDistance(path.LastStep.position, t.position);
				var newPath = path.AddStep(t, dist);
				open.Enqueue(newPath.TotalCost, newPath);
			}
		}
		Debug.Log("There is no acceptable path :(");
		return null;
	}


	// Calculating distance between two tiles using Axial Cooridnate systems.
	private double calcDistance(Vector3 startPos, Vector3 endPos)
	{
		var q1 = startPos.x;
		var q2 = startPos.y;
		var r1 = endPos.x;
		var r2 = endPos.y;

		return (Mathf.Abs(q1 - q2) + Mathf.Abs(r1 - r2) + Mathf.Abs(q1 + r1 - q2 - r2)) / 2;
	}

	private IEnumerator pathCoroutine(Path<Tile> shortestPath)
	{
		foreach (Tile t in shortestPath)
		{
			t.tile.renderer.material.color = Color.magenta;
			int newRotation = newOrientation(t.position);
			rotatePlayer(newRotation);
			StartCoroutine(movePlayer(t.position));

			yield return new WaitForSeconds(tilesPerSecond);
		}
	}

	// Moves Player, one tile per function call.
	private IEnumerator movePlayer(Vector3 tileLocation)
	{
		while (Vector3.Distance(myTransform.position, tileLocation) >= 0.06f)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, tileLocation, Time.deltaTime * movementSpeed);
			yield return null;
		} 

		mouseMovementScript.hitTile.hasPlayer = true;
		headingToTile = false;
		mouseMovementScript.tileFound = false;
	}

	// Checks rotation of Player every time he moves.
	private void rotatePlayer(int zRot)
	{
		Quaternion newRotation = Quaternion.AngleAxis(zRot, Vector3.forward);
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, newRotation, rotationSpeed);
	}

	// Finds the new way to face before movement.
	private int newOrientation(Vector3 tileLocation)
	{
		Vector3 myPosition = myTransform.position;

		if (tileLocation.x > myPosition.x)
		{
			if (Mathf.Abs(tileLocation.y - myPosition.y) <= 0.2f)
			{
				return (int)rotation.RIGHT;
			}
			if (tileLocation.y > myPosition.y)
			{
				return (int)rotation.UPPER_RIGHT;
			}
			else
			{
				return (int)rotation.LOWER_RIGHT;
			}
		}

		else 	// Not right of player, so it has to be left of it.
		{
			if (Mathf.Abs(tileLocation.y - myPosition.y) <= 0.2f)
			{
				return (int)rotation.LEFT;
			}
			if (tileLocation.y > myPosition.y)
			{
				return (int)rotation.UPPER_LEFT;
			}
			else
			{
				return (int)rotation.LOWER_LEFT;
			}
		}
	}
}













