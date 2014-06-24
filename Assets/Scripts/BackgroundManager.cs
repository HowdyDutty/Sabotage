
using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour 
{
	public GameObject quad;

	void Start() 
	{
		quad = (GameObject)Resources.Load("Prefabs/BackgroundPiece");
		createBackground();
	}

	private void createBackground()
	{
		createPiece(new Vector3(0, 0, 10), new Vector3(0, 0, 0), "Bottom Piece");
		createPiece(new Vector3(-10, 0, 0), new Vector3(0, 270, 0), "Left Piece");
		createPiece(new Vector3(10, 0, 0), new Vector3(0, 90, 0), "Right Piece");
		createPiece(new Vector3(0, 10, 0), new Vector3(270, 0, 0), "Back Piece");
	}

	private void createPiece(Vector3 position, Vector3 rotation, string name)
	{
		GameObject instance = (GameObject)Instantiate(quad);
		instance.renderer.material.color = Color.black;
		instance.transform.position = position;
		instance.transform.localScale = new Vector3(20, 20, 0);
		instance.transform.rotation = Quaternion.Euler(rotation);
		instance.name = name;
	}
}
