using UnityEngine;
using System.Collections;

public class SpotlightScript : MonoBehaviour {

	public Vector2[] positions;
	public GameObject player;

	private int nextPositionIndex;
	private Vector2 nextPosition;

	// Use this for initialization
	void Start () {
		transform.position = positions[0];
		nextPositionIndex = 1;
		nextPosition = positions[nextPositionIndex];
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.Lerp(transform.position, nextPosition, 1.0f * Time.deltaTime);

		float currentX = transform.position.x;
		float currentY = transform.position.y;
		if(currentX + 0.1f >= nextPosition.x && currentX - 0.1f <= nextPosition.x)
		{
			if(nextPositionIndex + 1 == positions.Length)
			{
				nextPositionIndex = 0;
			}
			else
			{
				nextPositionIndex++;
			}
			nextPosition = positions[nextPositionIndex];
		}
	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if(!player.GetComponent<PlayerMovementScript>().isHidden)
		{
			Destroy(player);
			Application.LoadLevel(0);
		}
	}
}
