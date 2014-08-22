
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpManager : MonoBehaviour 
{
	public BoardManager boardManagerScript;
	private GameObject pickUp;
	public List<PickUpItem> pickUpList { get; private set; }

	private int maxNumberPickUps = 4;	// Per round.
	public int totalPickUps;

	void Start() 
	{
		boardManagerScript = FindObjectOfType<BoardManager>();
		pickUp = (GameObject)Resources.Load("Prefabs/Pick Ups/Temporary");	// Change from temp Prefab in future.
		pickUpList = new List<PickUpItem>();
		totalPickUps = 0;

		createPickUps();
	}

	public void createPickUps()
	{
		int randomNumberPickUps = Random.Range(0, maxNumberPickUps);
		Tile[] tiles = boardManagerScript.tiles.ToArray();

		for (int i = 0; i < randomNumberPickUps; i++)
		{
			int randomTilePlacement = Random.Range(2, boardManagerScript.tiles.Count);
			Tile placementTile = tiles[randomTilePlacement];
		
			GameObject currPickUpItem = (GameObject)Instantiate(pickUp, placementTile.position, placementTile.tile.transform.rotation);
			currPickUpItem.collider.isTrigger = true;
			currPickUpItem.transform.localScale = new Vector3(.3f, .3f, .3f);
			currPickUpItem.renderer.material.color = Color.cyan;

			totalPickUps++;

			PickUpItem currItem = new PickUpItem(currPickUpItem, placementTile, totalPickUps);
			pickUpList.Add(currItem);
		}
	}
}
