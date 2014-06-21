
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public BoardManager boardManagerScript;
	public ScoreManager scoreManagerScript;
	public PlayerManager playerManagerScript;

	void Start() 
	{
		this.gameObject.AddComponent<BoardManager>();
		this.gameObject.AddComponent<ScoreManager>();
		this.gameObject.AddComponent<PlayerManager>();
	}


}
