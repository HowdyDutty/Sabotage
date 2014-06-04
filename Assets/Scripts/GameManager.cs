
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private BoardManager boardManagerScript;

	void Start() 
	{
		this.gameObject.AddComponent<BoardManager>();
	}
}
