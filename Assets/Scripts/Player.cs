
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

	private MouseMovement mouseMovementScript;
	private Transform myTransform;
	private bool atTile = false;

	enum rotation : int 
	{
		RIGHT 		= 270,
		LEFT  		= 90,
		LOWER_LEFT  = 240,
		LOWER_RIGHT = 300,
		UPPER_LEFT  = 30,
		UPPER_RIGHT = 330
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
		if (mouseMovementScript.tileFound && !atTile)
		{
			atTile = true;

			Vector3 tileLocation = mouseMovementScript.hitTile.position;
			Vector3 moveDirection = (tileLocation - myTransform.position).normalized;
			moveDirection.z = 2.5f;	// Keep on same z-plane.

			StartCoroutine(move(tileLocation));
		}
	}

	private IEnumerator move(Vector3 tileLocation)
	{
		int playerRotation = findNewOrientation(tileLocation);

		while (Vector3.Distance(myTransform.position, tileLocation) >= 0.06f)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, tileLocation, Time.deltaTime * movementSpeed);
			yield return null;
		}

		atTile = false;
		mouseMovementScript.tileFound = false;
	}

	private int findNewOrientation(Vector3 tileLocation)
	{
		if (tileLocation.x > myTransform.position.x)
		{
			if (tileLocation.y > myTransform.position.y)
			{
				return rotation.UPPER_RIGHT;
			}
			else if (tileLocation.y == myTransform.position.y)
			{
				return rotation.RIGHT;
			}
			else
			{
				return rotation.LOWER_RIGHT;
			}
		}

		else 	// Not right of player, so it has to be left of it.
		{
			if (tileLocation.y > myTransform.position.y)
			{
				return rotation.UPPER_LEFT;
			}
			else if (tileLocation.y == myTransform.position.y)
			{
				return rotation.LEFT;
			}
			else
			{
				return rotation.LOWER_LEFT;
			}
		}
	}
}













