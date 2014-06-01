
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	private BoardManager boardManagerScript;

	void Start() 
	{
		this.gameObject.AddComponent<BoardManager>();
	}
}
