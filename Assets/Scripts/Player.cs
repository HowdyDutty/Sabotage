
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float movementSpeed = 2f;

	private MouseMovement mouseMovementScript;
	private Transform myTransform;
	private bool currMoving = false;

	void Start()
	{
		this.renderer.material.color = Color.black;
		myTransform = this.transform;
		myTransform.rotation = Quaternion.Euler(0, 0, -63);
		mouseMovementScript = this.GetComponent<MouseMovement>();
	}
	
	void Update()
	{
		if (mouseMovementScript.moveToTile && !currMoving)
		{
			currMoving = true;
			Vector3 tileLocation = mouseMovementScript.hitTile.position;
			Debug.Log(tileLocation.x + "   " + tileLocation.y);

			Vector3 moveDirection = (tileLocation - myTransform.position).normalized;
			moveDirection.z = 2;	// Keep on same z-plane.

			StartCoroutine(move(tileLocation));

			currMoving = false;
			mouseMovementScript.moveToTile = false;
		}
	}

	IEnumerator move(Vector3 tileLocation)
	{
		while (Vector3.Distance(myTransform.position, tileLocation) >= 0.01f)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, tileLocation, Time.deltaTime * movementSpeed);
			Debug.Log("Its still going!");
			yield return null;
		}

		yield return new WaitForSeconds(5);
	}
}













