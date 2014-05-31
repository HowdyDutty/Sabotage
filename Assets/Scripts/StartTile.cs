
using UnityEngine;
using System.Collections;

public class StartTile : MonoBehaviour 
{
	public GameObject player;

	void Start() 
	{
		this.renderer.material.color = Color.blue;

		player = (GameObject)Resources.Load("Prefabs/Player");
		Vector3 playerSpawnLocation = transform.position + new Vector3(0, 0, -1);
		Instantiate(player, playerSpawnLocation, this.transform.rotation);
		player.transform.localScale = new Vector3(.5f, .5f, .5f);
	}

}
