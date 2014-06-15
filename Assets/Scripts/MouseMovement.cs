
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MouseMovement : MonoBehaviour 
{
	private BoardManager boardManagerScript;
	private IList<Tile> tileList;

	private GameObject hitObject;
	private Tile tileHelper;
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
		boardManagerScript = FindObjectOfType<BoardManager>();
		tileList = boardManagerScript.tiles;
	}

	void Update() 
	{
		// Left click
		if (Input.GetMouseButtonDown(0))
		{
			takeAction(
			() => 
			{
				if (tileHelper.tile == hitObject)
				{
					tileHelper.setBlocked();
				}
			});
		}

		// Right click.
		if (Input.GetMouseButtonDown(1))
		{
			takeAction(
			() => 
			{
				if ((tileHelper.tile == hitObject) && (!tileHelper.isBlocked))
				{
					_hitTile = tileHelper;
					_tileFound = true;
				}
			});
		}
	} // Update

	private void takeAction(Action action)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction, Color.red);
		RaycastHit hit;
		
		// If the ray hits something.
		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			hitObject = hit.collider.gameObject;
			
			// If the GameObject hit is a Tile.
			if (hitObject.tag == "Tile")
			{
				// Loop through the list of tile until the GameObject matches... not elegant, but it works.
				foreach (Tile t in tileList)
				{
					tileHelper = t;
					if (action != null)
					{
						action();
					}
				}
			}
		}
	}

} // MouseMovement










