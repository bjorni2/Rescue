using UnityEngine;
using System.Collections;

public class ExitTriggerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.name == "Player")
			Application.LoadLevel(1);
	}
}
