using UnityEngine;
using System.Collections;

public class HidingPlaceScript : MonoBehaviour {

	public GameObject player;

	void OnTriggerEnter2D(Collider2D other) 
	{
		player.GetComponent<PlayerMovementScript>().canHide = true;
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		player.GetComponent<PlayerMovementScript>().canHide = false;
	}
}
