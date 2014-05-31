
using UnityEngine;
using System.Collections;

public class FinishTile : MonoBehaviour 
{
	void Start() 
	{
		this.renderer.material.color = Color.green;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Equals("Player"))
		{
			Debug.Log("Player A wins!!!");
		}
	}
}
