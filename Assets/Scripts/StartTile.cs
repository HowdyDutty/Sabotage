
/*
 * Written By: Roman Larionov
 * Script: StartTile.cs
 * 
 * Attached to Start Tile GameObject. 
 * 
 * !! This script is added to the Start Tile GameObject through code in the BoardManager Script !!
 * 
 * This script contols the Start Tile, including: 
 * Instantiating the player, detecting that the 
 * game has started, and being rendered as the 
 * "Start Tile".
 */

using UnityEngine;
using System.Collections;

public class StartTile : MonoBehaviour
{
	void Start() 
	{
		this.renderer.material.color = Color.blue;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			Debug.Log("Player A has started playing!!!");
		}
	}

}
