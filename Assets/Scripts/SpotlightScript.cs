using UnityEngine;
using System.Collections;

public class SpotlightScript : MonoBehaviour {

	public SpotlightMovement[] positions;

	private int currentIndex;
	private Vector2 nextDestination;
	private int speed = 0;
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
		speed = updatesLeft;
	}

	void FixedUpdate () {
		if(updatesLeft == 0)
		{
			if(currentIndex + 1 == positions.Length)
			{
				currentIndex = 0;
			}
			else
			{
				currentIndex++;
			}
			nextDestination = positions[currentIndex].destination;

			deltaX = nextDestination.x - rigidbody2D.position.x;
			deltaY = nextDestination.y - rigidbody2D.position.y;

			float distance = Mathf.Sqrt (deltaX*deltaX + deltaY*deltaY);
			
			updatesLeft = Mathf.RoundToInt( distance / positions[currentIndex].stepLenght );
			speed = updatesLeft;
		}
		Vector2 nextStep = new Vector2(rigidbody2D.position.x + deltaX/speed, rigidbody2D.position.y + deltaY/speed);

		rigidbody2D.MovePosition(nextStep);


		updatesLeft--;
	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject.name == "Player")
		{
			if(!other.GetComponent<PlayerMovementScript>().isHidden)
			{
				Destroy(other);
				Application.LoadLevel(0);
			}
		}
	}
}

[System.Serializable]
public class SpotlightMovement
{
	public Vector2 destination;
	public float stepLenght;
}
