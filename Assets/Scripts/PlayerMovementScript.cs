using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	private bool LEFT = true;
	private bool RIGHT = false;

	public bool canHide = false;
	public bool isHidden = false;

	public float maxSpeed;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.touchCount == 1)
		{
			anim.SetTrigger("Moving");
			Touch touch = Input.GetTouch (0);
			if((touch.position.x / Screen.width) > 0.5f)
			{
				MovePlayer(RIGHT);
			}
			else
			{
				MovePlayer(LEFT);
			}
		}
		else if(Input.touchCount > 1)
		{
			anim.SetTrigger("Moving");
			StandStill();
		}
		else
		{
			Interact();
		}
	}

	void MovePlayer (bool direction)
	{
		isHidden = false;
		if(direction == RIGHT)
		{
			//GetComponent<SpriteRenderer>().color = new Color(1.0f, 235.0f/255.0f, 4.0f/255.0f, 1.0f);
			rigidbody2D.velocity = new Vector2 (maxSpeed, rigidbody2D.velocity.y);
		}
		else if(direction == LEFT)
		{
			//GetComponent<SpriteRenderer>().color = new Color(1.0f, 235.0f/255.0f, 4.0f/255.0f, 1.0f);
			rigidbody2D.velocity = new Vector2 (-maxSpeed, rigidbody2D.velocity.y);
		}
	}

	void Interact ()
	{
		if(canHide)
		{
			anim.SetTrigger("Hiding");
			isHidden = true;
			//GetComponent<SpriteRenderer>().color = new Color(1.0f, 235.0f/255.0f, 4.0f/255.0f, 0.3f);
		}
		else
		{
			anim.SetTrigger ("Crawling");
			isHidden = false;
			//GetComponent<SpriteRenderer>().color = Color.green;
		}

		rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
	}

	void StandStill ()
	{
		isHidden = false;
		GetComponent<SpriteRenderer>().color = new Color(1.0f, 235.0f/255.0f, 4.0f/255.0f, 1.0f);
		rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
	}

}
