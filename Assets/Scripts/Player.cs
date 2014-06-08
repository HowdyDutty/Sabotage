
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

public class Player : MonoBehaviour 
{
	public float movementSpeed = 7f;
	public float rotationSpeed = 10f;

	private MouseMovement mouseMovementScript;
	private Transform myTransform;
	private bool headingToTile = false;

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

			findShortestPath(myTransform.position, tileLocation);

			rotatePlayer(newRotation);
			StartCoroutine(movePlayer(tileLocation));
		}
	}

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
	private Vector3[] findShortestPath(Vector3 start, Vector3 end) 
	{
		Vector3[] path = new Vector3[1];


		return path;
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













