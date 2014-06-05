
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
	public GameObject player;

	void Start() 
	{
		this.renderer.material.color = Color.blue;

		player = (GameObject)Resources.Load("Prefabs/Player");
		Vector3 playerSpawnLocation = this.transform.position + new Vector3(0, 0, -0.5f);

		GameObject instantiatedPlayer = (GameObject)Instantiate(player, playerSpawnLocation, this.transform.rotation);

		instantiatedPlayer.transform.localScale = new Vector3(.5f, .5f, .5f);

		GameObject playerGameObject = instantiatedPlayer.gameObject;	// So it only loads once.
		playerGameObject.AddComponent<Player>();
		playerGameObject.AddComponent<MouseMovement>();
		playerGameObject.GetComponent<BoxCollider>().isTrigger = true;
		playerGameObject.AddComponent<Rigidbody>().useGravity = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			Debug.Log("Player A has started playing!!!");
		}
	}

}
