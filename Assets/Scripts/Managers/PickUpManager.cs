
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpManager : MonoBehaviour 
{
	public BoardManager boardManagerScript;
	private GameObject pickUp;
	public List<PickUpItem> pickUpList { get; private set; }

	public int numPickUps 	 = 30;
	

	void Start() 
	{
		boardManagerScript = FindObjectOfType<BoardManager>();
		pickUp = (GameObject)Resources.Load("Prefabs/Pick Ups/Temporary");	// Change from temp Prefab in future.
		pickUpList = new List<PickUpItem>();

		createPickUps();
	}

	private void createPickUps()
	{
		int pickUpsLeft = 1;
		foreach (Tile t in boardManagerScript.tiles)
		{	
			if (pickUpsLeft >= numPickUps)
			{
				break;
			}
			else if (Random.Range(1, boardManagerScript.tiles.Count) == t.tileNumber)
			{	
				GameObject currPickUpItem = (GameObject)Instantiate(pickUp, t.position, t.tile.transform.rotation);
				currPickUpItem.collider.isTrigger = true;
				currPickUpItem.transform.localScale = new Vector3(.3f, .3f, .3f);
				currPickUpItem.renderer.material.color = Color.cyan;
				
				PickUpItem currItem = new PickUpItem(currPickUpItem, t, pickUpsLeft);
				pickUpList.Add(currItem);
				pickUpsLeft++;
			}
		}
	}
}
