
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private BoardManager boardManagerScript;
	private ScoreManager scoreManagerScript;

	void Start() 
	{
		this.gameObject.AddComponent<BoardManager>();
		this.gameObject.AddComponent<ScoreManager>();
	}
}
