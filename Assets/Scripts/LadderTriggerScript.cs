using UnityEngine;
using System.Collections;

public class LadderTriggerScript : MonoBehaviour {

	private bool TOP = true;
	private bool BOTTOM = false;

	public bool type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(type == TOP)
			{
				other.gameObject.GetComponent<PlayerMovementScript>().atTop = true;
			}
			else if(type == BOTTOM)
			{
				other.gameObject.GetComponent<PlayerMovementScript>().atBottom = true;
			}
			other.gameObject.GetComponent<PlayerMovementScript>().canClimb = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(type == TOP)
			{
				other.gameObject.GetComponent<PlayerMovementScript>().atTop = false;
			}
			else if(type == BOTTOM)
			{
				other.gameObject.GetComponent<PlayerMovementScript>().atBottom = false;
			}
			other.gameObject.GetComponent<PlayerMovementScript>().canClimb = false;
		}
	}
}
