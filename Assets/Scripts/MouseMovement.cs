
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseMovement : MonoBehaviour 
{
	private GameObject gameManager;
	private BoardManager boardManagerScript;
	private IList<Tile> tileList;
	private Tile _hitTile;
	private bool _tileFound = false;

	public Tile hitTile
	{
		get { return _hitTile; }
	}

	public bool tileFound
	{
		get { return _tileFound; }
		set { _tileFound = value; }
	}

	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		boardManagerScript = gameManager.GetComponent<BoardManager>();
		tileList = boardManagerScript.tiles;
	}

	void Update() 
	{
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction, Color.red);
			RaycastHit hit;

			// If the ray hits something.
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				GameObject hitObject = hit.collider.gameObject;

				// If the GameObject hit is a Tile.
				if (hitObject.name.Equals("Tile"))
				{
					// Loop through the list of tile until the GameObject matches... not elegant, but it works.
					foreach (Tile t in tileList)
					{
						if (t.tile == hitObject)
						{
							_hitTile = t;
							_tileFound = true;
						}
					}
					Debug.Log("It hit a Tile!!");
				}
			}
		}
	} // Update
} // MouseMovement










