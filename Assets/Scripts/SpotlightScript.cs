using UnityEngine;
using System.Collections;

public class SpotlightScript : MonoBehaviour {

	public SpotlightMovement[] positions;

	//public Vector2[] positions;
	public GameObject player;

	private int currentIndex;
	private Vector2 nextPosition;
	private int currentSpeed = 0;
	private int updatesLeft = 0;
	private float deltaX = 0.0f;
	private float deltaY = 0.0f;

	// Use this for initialization
	void Start () {
		rigidbody2D.MovePosition(positions[0].position);
		currentIndex = 1;
		nextPosition = positions[currentIndex].position;
		updatesLeft = positions[currentIndex].speed;
		currentSpeed = positions[currentIndex].speed;

		deltaX = nextPosition.x - positions[0].position.x;
		deltaY = nextPosition.y - positions[0].position.y;
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
			nextPosition = positions[currentIndex].position;

			deltaX = nextPosition.x - rigidbody2D.position.x;
			deltaY = nextPosition.y - rigidbody2D.position.y;

			updatesLeft = positions[currentIndex].speed;
			currentSpeed = positions[currentIndex].speed;
		}
		Vector2 nextStep = new Vector2(rigidbody2D.position.x + deltaX/currentSpeed, rigidbody2D.position.y + deltaY/currentSpeed);

		rigidbody2D.MovePosition(nextStep);


		updatesLeft--;
	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject.name == "Player")
		{
			if(!player.GetComponent<PlayerMovementScript>().isHidden)
			{
				Destroy(player);
				Application.LoadLevel(0);
			}
		}
	}
}

[System.Serializable]
public class SpotlightMovement
{
	public Vector2 position;
	public int speed;
}
