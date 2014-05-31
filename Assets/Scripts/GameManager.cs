
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private BoardManager boardManagerScript;

	void Start() 
	{
		boardManagerScript = GameObject.Find("GameManager").GetComponent<BoardManager>();
	}

}
