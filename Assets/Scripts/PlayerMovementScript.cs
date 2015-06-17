using UnityEngine;
using System.Collections;
//using System.Diagnostics;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovementScript : MonoBehaviour {

	private float MISS_CHANCE = 0.99f;
	private float PLAYER_WIDTH = 0.64f;
	private float PLAYER_HEIGHT = 1.28f;

	private bool LEFT_OR_DOWN = true;
	private bool RIGHT_OR_UP = false;


	private bool shouldMove = false;
	private bool shouldFreeze = false;
	private bool moveDirection = false;

	// Current status of player.
	public bool canHide = false;
	public bool isHidden = false;
	public bool isCrawling = false;
	public bool canClimb = false;
	public bool isClimbing = false;
	public bool atTop = false;
	public bool atBottom = false;

	public float maxSpeed;
	private Animator anim;

	public int nOfSpotlights;
	private float shotInterval = 0.250f;
	private float nextShot = 0.0f;

	public GameObject Decal;

	void Start () 
	{
		anim = GetComponent<Animator>();
		nOfSpotlights = 0;
	}

	void GetInput ()
	{
		// Go to main menu on back key.
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			Application.LoadLevel(0); 
		}

		GetTouchInput();

		GetEditorInput();
	}

	[System.Diagnostics.Conditional("UNITY_ANDROID")]
	void GetTouchInput ()
	{
		if(Input.touchCount == 1)
		{
			//Debug.Log ("GetTouchInput");
			shouldMove = true;

			Touch touch = Input.GetTouch (0);
			if((touch.position.x / Screen.width) > 0.5f){
				moveDirection = RIGHT_OR_UP;
			}
			else{
				moveDirection = LEFT_OR_DOWN;
			}
		}
		else if(Input.touchCount > 1)
		{
			shouldMove = false;
			shouldFreeze = true;
		}
		else
		{
			shouldMove = false;
			shouldFreeze = false;
		}
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	void GetEditorInput ()
	{

		bool leftPressed = Input.GetKey(KeyCode.LeftArrow);
		bool rightPressed = Input.GetKey(KeyCode.RightArrow);

		if(leftPressed ^ rightPressed)
		{
			//Debug.Log ("GetEditorInput");
			shouldMove = true;

			if(leftPressed)
			{
				moveDirection = LEFT_OR_DOWN;
			}
			else
			{
				moveDirection = RIGHT_OR_UP;
			}
		}
		else if(leftPressed && rightPressed)
		{
			shouldMove = false;
			shouldFreeze = true;
		}
		else
		{
			shouldMove = false;
			shouldFreeze = false;
		}
	}

	void Update(){
		GetInput();

		if(!isHidden && nOfSpotlights > 0){
			if(Time.time >= nextShot){
				nextShot = Time.time + shotInterval;
				GetComponent<AudioSource>().Play();
				//Debug.Log("Shooting\n");

				// Try to make decal appear close to player.
				Vector3 decPosition;
				// Place the decal randomly near the player sprite,  
				decPosition.x = this.transform.position.x - PLAYER_WIDTH + (Random.value * 1.3f * PLAYER_WIDTH);
				decPosition.y = this.transform.position.y - PLAYER_HEIGHT + (Random.value * 1.3f * PLAYER_HEIGHT);
				decPosition.z = this.transform.position.z;
				Decal.transform.position = decPosition;
				Decal.GetComponent<SpriteRenderer>().enabled = true;

				if(Random.value > MISS_CHANCE)
				{
					//Debug.Log("Hit\n");

					Destroy(this);
					Application.LoadLevel(0);
				}
				else
				{
					//Debug.Log("Miss\n");
				}
			}
		}
		else
		{
			Decal.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void FixedUpdate () {
		if(shouldMove)
		{
			MovePlayer (moveDirection);
		}
		else if(shouldFreeze)
		{
			StandStill();
		}
		else
		{
			Interact();
		}
	}

	void MovePlayer (bool direction)
	{
		if(isCrawling)
		{
			enlargeHitbox();
		}

		isHidden = false;
		if(direction == RIGHT_OR_UP)
		{
			if(isClimbing)
			{
				if(canClimb || atTop)
				{
					canClimb = false;
					isClimbing = false;
					GetComponent<Rigidbody2D>().gravityScale = 1;
					anim.SetTrigger("Moving");
				}
				else
				{
					anim.SetTrigger("Climbing");
					GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, maxSpeed);
				}
			}
			else
			{	
				anim.SetTrigger("Moving");
				GetComponent<Rigidbody2D>().velocity = new Vector2 (maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			}
		}
		else if(direction == LEFT_OR_DOWN)
		{
			if(isClimbing)
			{
				if(canClimb || atBottom)
				{
					canClimb = false;
					isClimbing = false;
					GetComponent<Rigidbody2D>().gravityScale = 1;
					anim.SetTrigger("Moving");
				}
				else
				{
					anim.SetTrigger("Climbing");
					GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, -maxSpeed);
				}
			}
			else
			{
				anim.SetTrigger("Moving");
				GetComponent<Rigidbody2D>().velocity = new Vector2 (-maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}

	/* 
	 * Try to interact with the world
	 */
	void Interact ()
	{
		// No interaction if we are in a ladder.
		if(isClimbing){
			// Unless we have entered another ladder trigger.
			if(canClimb)
			{
				canClimb = false;
				isClimbing = false;
				GetComponent<Rigidbody2D>().gravityScale = 1;
				anim.SetTrigger("Moving");
			}
			else
			{
				anim.SetTrigger("Climbing");
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				GetComponent<Rigidbody2D>().gravityScale = 0;
			}
			return;
		}

		if(canHide)
		{
			anim.SetTrigger("Hiding");
			isHidden = true;
		}
		else if(canClimb)
		{
			canClimb = false;
			anim.SetTrigger("Climbing");
			isClimbing = true;
		}
		else
		{
			anim.SetTrigger ("Crawling");
			isHidden = false;

			shrinkHitbox();
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
	}

	void StandStill ()
	{
		if(isCrawling)
		{
			enlargeHitbox();
		}

		if(isClimbing)
		{
			anim.SetTrigger("Climbing");
		}
		else
		{
			anim.SetTrigger("Moving");
		}

		isHidden = false;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
	}

	void enlargeHitbox()
	{
		isCrawling = false;
		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
		collider.size = new Vector2 (0.64f, 1.28f);
		collider.offset = new Vector2 (0.0f, 0.0f);
	}

	void shrinkHitbox()
	{
		isCrawling = true;
		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
		collider.size = new Vector2 (0.64f, 0.64f);
		collider.offset = new Vector2 (0.0f, -0.32f);
	}
}
