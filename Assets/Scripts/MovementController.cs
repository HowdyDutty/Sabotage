
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
	//private bool connected;

	void Start()
	{
		//connected = false;
	}

	void Update()
	{

	}


	/*
	void Awake()
	{
		//StartCoroutine(CheckForControllers());
	}
	// Checks for controller support every second.
	private IEnumerator CheckForControllers()
	{
		string[] controllers = Input.GetJoystickNames();
		
		if (!connected && controllers.Length > 0)
		{
			this.connected = true;
			Debug.Log("Connected");
		}
		
		else if (connected && controllers.Length == 0)
		{
			this.connected = false;
			Debug.Log("Disconnected");
		}
		
		yield return new WaitForSeconds(1f);
	}
	*/
} // MovementController
