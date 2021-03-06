﻿
/*
 * Written By: Roman Larionov
 * Script: GameBoardPlayer.cs
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
using Sabotage.Enums;

public class GameBoardPlayer : MonoBehaviour
{
	public float movementSpeed  = 3f;
	public float retardSpeed    = 1.5f;
	public float rotationSpeed  = 10f;
	public int movesPerTurn     = 300;
	public int movesRemaining   = 300;
	public int pointsPerMove	= 20;

	private MouseMovement mouseMovementScript;
	private BoardManager boardManagerScript;
	private PlayerManager playerManagerScript;
	private ScoreManager scoreManagerScript;

	private IList<Tile> tileList;
	private Transform myTransform;
	private bool headingToTile = false;
	private Tile occupiedTile;

	void Start()
	{
		mouseMovementScript = this.GetComponent<MouseMovement>();
		boardManagerScript  = FindObjectOfType<BoardManager>();
		scoreManagerScript  = FindObjectOfType<ScoreManager>();
		playerManagerScript = FindObjectOfType<PlayerManager>();
		tileList = boardManagerScript.tiles;

		this.renderer.material.color = Color.black;
		myTransform = this.transform;
		myTransform.rotation = Quaternion.Euler(0, 0, 300);	// Starting rotation.
	}

	void Update()
	{
		// If this player is currently on the board and it's their turn.
		if (playerManagerScript.activePlayer == (int)PlayerType.GAMEBOARD)	
		{
			// Switch turns because the player has taken too many steps.
			if (movesRemaining == 0)
			{
				playerManagerScript.changeTurn();
				movesRemaining = movesPerTurn;
			}
			// If there has been a hit tile and the player is not currently heading to a tile.
			else if (mouseMovementScript.tileFound && !headingToTile)
			{
				headingToTile = true;
				Path<Tile> shortestPath = findShortestPath(occupiedTile, mouseMovementScript.hitTile);

				// If the tile found is unreachable or requires too many steps to get to.
				if ((shortestPath == null) || (shortestPath.getNumTilesInPath() > movesRemaining))
				{
					headingToTile = false;
					mouseMovementScript.tileFound = false;
					Debug.Log("The desired destination is either blocked or too far away :(");
				}
				else
				{	// Start the journey.
					StartCoroutine(pathCoroutine(shortestPath));
				}
			}
		}
	}

	// Saves the tile that the player is currently occupying.
	void OnTriggerEnter(Collider other)
	{
		if (other.name.Equals("Finish Tile"))
		{
			playerManagerScript.changeRound();
			scoreManagerScript.updateScore(0);
		}
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
		if (other.tag.Equals("PickUp"))
		{
			// Add condition for if the player chose the Gameboard Plater Item or God Player Action.

			if (playerManagerScript.getGameBoardInventory().Count < playerManagerScript.maxGameBoardItems)
			{
				Debug.Log("Picked up an item!!");
				GameObject otherGameObject = other.gameObject;
				otherGameObject.SetActive(false);
				playerManagerScript.getGameBoardInventory().Add(other);
			}
			/*if (playerManagerScript.getGodInventory().Count < playerManagerScript.maxGodItems)
			{
				Debug.Log("Picked up an item!!");
				GameObject otherGameObject = other.gameObject;
				otherGameObject.SetActive(false);
				playerManagerScript.getGodInventory().Add(other);
			}*/
		}
	}

	//-------------------------------------Movement-----------------------------------------//

	// A* algorithm to find shortest path to desired tile.
	private Path<Tile> findShortestPath(Tile start, Tile end)
	{
		PriorityQueue<int, Path<Tile>> open = new PriorityQueue<int, Path<Tile>>();
		HashSet<Tile> closed = new HashSet<Tile>();
		open.Enqueue(0, new Path<Tile>(start));
		int cost = 1; 

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

				var newPath = path.AddStep(t, cost);
				open.Enqueue(newPath.TotalCost, newPath);
			}
		}
		return null;
	}

	private IEnumerator pathCoroutine(Path<Tile> shortestPath)
	{
		foreach (Tile t in shortestPath)
		{
			if (movesRemaining > 0)
			{
				t.tile.renderer.material.color = Color.magenta;
				int newRotation = newOrientation(t.position);
				rotatePlayer(newRotation);
				StartCoroutine(movePlayer(t.position));
				movesRemaining--;
			}
			else
			{
				break;
			}

			yield return new WaitForSeconds(retardSpeed);
		}
	}

	// Moves Player, one tile per function call.
	private IEnumerator movePlayer(Vector3 tileLocation)
	{
		scoreManagerScript.updateScore(pointsPerMove);
		while (Vector3.Distance(myTransform.position, tileLocation) >= 0.06f)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, tileLocation, Time.deltaTime * movementSpeed);
			yield return null;
		} 

		mouseMovementScript.hitTile.hasPlayer = true;
		headingToTile = false;
		mouseMovementScript.tileFound = false;
	}

	// Checks rotation of the Player every time he moves.
	private void rotatePlayer(int zRot)
	{
		Quaternion newRotation = Quaternion.AngleAxis(zRot, Vector3.forward);
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, newRotation, rotationSpeed);
	}

	// Finds the correct way to face before movement.
	private int newOrientation(Vector3 tileLocation)
	{
		Vector3 myPosition = myTransform.position;

		if (tileLocation.x > myPosition.x)
		{
			if (Mathf.Abs(tileLocation.y - myPosition.y) <= 0.2f)
			{
				return (int)rotation.RIGHT;
			}
			else if (tileLocation.y > myPosition.y)
			{
				return (int)rotation.UPPER_RIGHT;
			}
			else
			{
				return (int)rotation.LOWER_RIGHT;
			}
		}
		else
		{ 	// Not right of player, so it has to be left of it.
			if (Mathf.Abs(tileLocation.y - myPosition.y) <= 0.2f)
			{
				return (int)rotation.LEFT;
			}
			else if (tileLocation.y > myPosition.y)
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













