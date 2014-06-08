
/*
 * Written By: Roman Larionov
 * Script: Player.cs
 * 
 * Attached to Player GameObject. 
 * 
 * !! This script is added to the Player GameObject through code in the Start Tile Script!!
 * 
 * This script controls the Player and all of the attributes about 
 * it, including: health, movement, location tracking, and more, soon.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	public float movementSpeed = 7f;
	public float rotationSpeed = 10f;

	private MouseMovement mouseMovementScript;
	private Transform myTransform;
	private bool headingToTile = false;
	private Tile playerTile;

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
		this.renderer.material.color = Color.black;
		myTransform = this.transform;
		myTransform.rotation = Quaternion.Euler(0, 0, 300);	// Starting rotation.
		mouseMovementScript = this.GetComponent<MouseMovement>();
	}
	
	void Update()
	{
		if (mouseMovementScript.tileFound && !headingToTile)
		{
			headingToTile = true;

			Vector3 tileLocation = mouseMovementScript.hitTile.position;
			int newRotation = newOrientation(tileLocation);

			//findShortestPath( , mouseMovementScript.hitTile);

			rotatePlayer(newRotation);
			StartCoroutine(movePlayer(tileLocation));
		}
	}

	// Working on saving the tile that the player is standing on.
	/*void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Tile"))
		{
			playerTile = other.gameObject;

			foreach (Tile t in mouseMovementScript.)
		}
	}*/

	/*
		A* pseudocode.
		--------------
		
		OPEN = priority queue containing START
		CLOSED = empty set
		while lowest rank in OPEN is not the GOAL:
		  current = remove lowest rank item from OPEN
		  add current to CLOSED
		  for neighbors of current:
		    cost = g(current) + movementcost(current, neighbor)
		    if neighbor in OPEN and cost less than g(neighbor):
		      remove neighbor from OPEN, because new path is better
		    if neighbor in CLOSED and cost less than g(neighbor): **
		      remove neighbor from CLOSED
		    if neighbor not in OPEN and neighbor not in CLOSED:
		      set g(neighbor) to cost
		      add neighbor to OPEN
		      set priority queue rank to g(neighbor) + h(neighbor)
		      set neighbor's parent to current

		reconstruct reverse path from goal to start
		by following parent pointers

	*/

	// A* algorithm to find shortest path to desired tile.
	private List<Vector3> findShortestPath(Tile start, Tile goal) 
	{
		List<Vector3> path = new List<Vector3>();
		int cost = 0;
		Queue open = new Queue();
		Stack closed = new Stack();

		open.Enqueue(start);
		int movementCost = 1;

		while (open.Peek() != goal)
		{
			Tile current = (Tile)open.Dequeue();
			closed.Push(current);
			ArrayList connections = start.connectedTiles;

			foreach (Tile neighbor in connections)
			{
				cost = findCost(current) + movementCost;

				if (open.Contains(neighbor) && (cost < findCost(neighbor)))
				{
					while (open.Contains(neighbor))
					{
						open.Dequeue();
					}
				}
				if (closed.Contains(neighbor) && (cost < findCost(neighbor)))
				{
					while (closed.Contains(neighbor))
					{
						closed.Pop();
					}
				}
				if (!open.Contains(neighbor) && !closed.Contains(neighbor))
				{
					cost = findCost(neighbor);
					open.Enqueue(neighbor);
					neighbor.parent = current;
				}
			}
		}
		Tile lastTile = (Tile)open.Dequeue();
		path.Add(lastTile.position);

		while (lastTile.parent != null)
		{
			path.Add(lastTile.parent.position);
			lastTile = lastTile.parent;
		}

		path.Reverse();

		foreach (Vector3 p in path)
		{
			Debug.Log(p);
		}

		return path;
	}

	private int findCost(Tile current)
	{
		if (current == null)
			return 1;

		return findCost (current.parent);
	}

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

	private void rotatePlayer(int zRot)
	{
		Quaternion newRotation = Quaternion.AngleAxis(zRot, Vector3.forward);
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, newRotation, rotationSpeed);
	}

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













