
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
		while (Vector3.Distance(myTransform.position, tileLocation) >= 0)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, tileLocation, Time.deltaTime );
			yield return null;
		}

		Debug.Log("At the target");
		yield return new WaitForSeconds(5);
		Debug.Log("Coroutine has finished");
	}
}













