
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
	private GameObject player;

	void Start()
	{
		player = this.gameObject;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		float hitDistance;


	}
	
} // MovementController
