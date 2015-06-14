using UnityEngine;
using System.Collections;

public class SpotlightScript : MonoBehaviour {

	public SpotlightMovement[] positions;

	private int currentIndex;
	private Vector2 nextDestination;
	private int nSteps = 0;
	private int updatesLeft = 0;
	private float deltaX = 0.0f;
	private float deltaY = 0.0f;

	// Use this for initialization
	void Start () {
		//rigidbody2D.MovePosition(positions[0].destination);
		transform.position = positions[0].destination;
		currentIndex = 1;
		nextDestination = positions[currentIndex].destination;

		deltaX = nextDestination.x - positions[0].destination.x;
		deltaY = nextDestination.y - positions[0].destination.y;

		float distance = Mathf.Sqrt (deltaX*deltaX + deltaY*deltaY);

		updatesLeft = Mathf.RoundToInt( distance / positions[currentIndex].stepLenght );
		nSteps = updatesLeft;
	}

	void FixedUpdate () {
		if(updatesLeft == 0)
		{
			currentIndex = (currentIndex + 1) % positions.Length;

			nextDestination = positions[currentIndex].destination;

			deltaX = nextDestination.x - GetComponent<Rigidbody2D>().position.x;
			deltaY = nextDestination.y - GetComponent<Rigidbody2D>().position.y;

			float distance = Mathf.Sqrt (deltaX*deltaX + deltaY*deltaY);
			
			updatesLeft = Mathf.RoundToInt( distance / positions[currentIndex].stepLenght );
			nSteps = updatesLeft;
		}
		Vector2 nextStep = new Vector2(GetComponent<Rigidbody2D>().position.x + deltaX/nSteps, GetComponent<Rigidbody2D>().position.y + deltaY/nSteps);

		GetComponent<Rigidbody2D>().MovePosition(nextStep);


		updatesLeft--;
	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		/*if(other.gameObject.name == "Player")
		{
			if(!other.GetComponent<PlayerMovementScript>().isHidden)
			{
				Destroy(other);
				Application.LoadLevel(0);
			}
		}*/
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			other.gameObject.GetComponent<PlayerMovementScript>().nOfSpotlights++;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			other.gameObject.GetComponent<PlayerMovementScript>().nOfSpotlights--;
		}
	}
}

[System.Serializable]
public class SpotlightMovement
{
	public Vector2 destination;
	public float stepLenght;
}
