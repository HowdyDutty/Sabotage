
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float movementSpeed = 7f;

	private MouseMovement mouseMovementScript;
	private Transform myTransform;
	private bool atTile = false;

	void Start()
	{
		this.renderer.material.color = Color.black;
		myTransform = this.transform;
		myTransform.rotation = Quaternion.Euler(0, 0, -63);
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

	IEnumerator move(Vector3 tileLocation)
	{
		while (Vector3.Distance(myTransform.position, tileLocation) >= 0.06f)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, tileLocation, Time.deltaTime * movementSpeed);
			yield return null;
		}

		atTile = false;
		mouseMovementScript.tileFound = false;
	}
}













