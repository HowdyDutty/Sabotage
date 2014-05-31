
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

	private BoardManager boardManagerScript;

	void Start() 
	{
		boardManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
	}
	/*
	void Update() 
	{
	
	}*/
}
