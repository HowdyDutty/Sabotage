
using UnityEngine;
using System.Collections;

public class FinishTile : MonoBehaviour 
{
	public GameObject gameManager;
	public Vector3 startTilePos;

	void Start() 
	{
		this.renderer.material.color = Color.green;
		startTilePos = FindObjectOfType<StartTile>().gameObject.transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			StartCoroutine("reset", other);
		}
	}

	IEnumerator reset(Collider other)
	{
		//other.gameObject.SetActive(false);
		yield return new WaitForSeconds(2);
		other.gameObject.transform.position = startTilePos;
	}
}
