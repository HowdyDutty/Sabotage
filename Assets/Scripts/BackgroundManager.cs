
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
		// Bottom
		createPiece(new Vector3(0, 0, 10), new Vector3(0, 0, 0));
		// Left
		createPiece(new Vector3(-10, 0, 0), new Vector3(0, 270, 0));
		// Right
		createPiece(new Vector3(10, 0, 0), new Vector3(0, 90, 0));
		// Back
		createPiece(new Vector3(0, 10, 0), new Vector3(270, 0, 0));
	}

	private void createPiece(Vector3 position, Vector3 rotation)
	{
		GameObject instance = (GameObject)Instantiate(quad);
		instance.renderer.material.color = Color.black;
		instance.transform.position = position;
		instance.transform.localScale = new Vector3(20, 20, 0);
		instance.transform.rotation = Quaternion.Euler(rotation);
	}
}
