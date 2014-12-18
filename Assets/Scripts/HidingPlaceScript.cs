using UnityEngine;
using System.Collections;

public class HidingPlaceScript : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.name == "Player")
			other.GetComponent<PlayerMovementScript>().canHide = true;
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
			other.GetComponent<PlayerMovementScript>().canHide = false;
	}
}
