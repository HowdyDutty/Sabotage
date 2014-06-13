
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
using System.Resources;

public class Player : MonoBehaviour 
{
	public float movementSpeed = 7f;
	public float rotationSpeed = 10f;

	private GameObject gameManager;
	private BoardManager boardManagerScript;
	private IList<Tile> tileList;


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
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		boardManagerScript = gameManager.GetComponent<BoardManager>();
		tileList = boardManagerScript.tiles;

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
		closed = {}
		q = emptyqueue;
		q.enqueue(0.0, makepath(start))
		while q is not empty
		    p = q.dequeueCheapest
		    if closed contains p.last then continue;
		    if p.last == destination then return p
		    closed.add(p.last)
		    foreach n in p.last.neighbours 
		        newpath = p.continuepath(n)
		        q.enqueue(newpath.TotalCost + estimateCost(n, destination), newpath)
		return null
		--------------------------------------------------------------------------------

		static public Path<Node> FindPath<Node>(
		    Node start, 
		    Node destination, 
		    Func<Node, Node, double> distance, 
		    Func<Node, double> estimate)
		    where Node : IHasNeighbours<Node>
		{
		    var closed = new HashSet<Node>();
		    var queue = new PriorityQueue<double, Path<Node>>();
		    queue.Enqueue(0, new Path<Node>(start));
		    while (!queue.IsEmpty)
		    {
		        var path = queue.Dequeue();
		        if (closed.Contains(path.LastStep))
		            continue;
		        if (path.LastStep.Equals(destination))
		            return path;
		        closed.Add(path.LastStep);
		        foreach(Node n in path.LastStep.Neighbours)
		        {
		            double d = distance(path.LastStep, n);
		            var newPath = path.AddStep(n, d);
		            queue.Enqueue(newPath.TotalCost + estimate(n), newPath);
		        }
		    }
		    return null;

	*/

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
				/*if (t.isBlocked)
				{
					closed.Add(t);
					break;
				}*/

				int dist = 1;  //calcDistance(path.LastStep.position, t.position);
				var newPath = path.AddStep(t, dist);
				open.Enqueue(newPath.TotalCost, newPath);
			}
		}
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













