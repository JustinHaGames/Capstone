using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 
	SpriteRenderer sprite; 

	bool grounded;

	public float accel;
	public float maxAccel;

	public float gravity; 
	bool canJump;
	bool jump; 
	public float baseJumpVel; 
	public float jumpVel;
	public float maxJumpVel;
	int jumpCounter; 

	bool lastR; 
	bool lastL; 

	public GameObject heldObject;
	bool grab;
	bool holding = false; 

	float waterSpeed = 1;
	bool swim;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer> ();

		lastR = true;
		lastL = false;

		//save the initial color of the player
		heldObject = null;

	}

	void Update(){
		if (canJump || swim) {
			if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Z)) && !jump) {
				jump = true;	
			}
		}

		//When you let go of the jump buttons, make jump false and fall
		if ((Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.Z))) {
			jump = false; 
			jumpCounter = 0;
		}

		//Pickup objects
		if (Input.GetKeyDown (KeyCode.X)) {
			//Box kicking test
			if (heldObject == null) {
				if (lastR) {
					RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - .5f), Vector2.right, 1f);
					heldObject = hit.collider.gameObject;
					heldObject.SendMessage ("PickUp", SendMessageOptions.DontRequireReceiver);
				} else if (lastL) {
					RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - .5f), Vector2.left, 1f);
					heldObject = hit.collider.gameObject;
					heldObject.SendMessage ("PickUp", SendMessageOptions.DontRequireReceiver);
				}
			} else {

				if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)) {
					heldObject.SendMessage ("RightDiagonal", SendMessageOptions.DontRequireReceiver);
					heldObject = null;
				} else if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)) {
					heldObject.SendMessage ("LeftDiagonal", SendMessageOptions.DontRequireReceiver);
					heldObject = null;
				} else if (Input.GetKey (KeyCode.UpArrow)) {
					heldObject.SendMessage ("Up", SendMessageOptions.DontRequireReceiver);
					heldObject = null;
				} else if (Input.GetKey (KeyCode.RightArrow)) {
					heldObject.SendMessage ("Right", SendMessageOptions.DontRequireReceiver);
					heldObject = null; 
				} else if (Input.GetKey (KeyCode.LeftArrow)) {
					heldObject.SendMessage ("Left", SendMessageOptions.DontRequireReceiver);
					heldObject = null; 
				} else if (Input.GetKey (KeyCode.DownArrow)) {
					heldObject.SendMessage ("Down", SendMessageOptions.DontRequireReceiver);
					heldObject = null; 
				} else if (Input.GetKeyDown (KeyCode.X)){
					heldObject.SendMessage ("Drop", SendMessageOptions.DontRequireReceiver);
					heldObject = null;
				}
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		//run Grounded function to see if you're grounded every frame
		Grounded ();

		//Press the left and right keys to move
		bool right = Input.GetKey(KeyCode.RightArrow);
		bool left = Input.GetKey (KeyCode.LeftArrow);

		//If right or left if pressed, accel in that direction
		if (right) {
			vel.x += accel/waterSpeed;
			lastR = true; 
			lastL = false; 
			sprite.flipX = false;

		}
		if (left) {
			vel.x -= accel/waterSpeed;
			lastL = true; 
			lastR = false; 
			if (!right) {
				sprite.flipX = true;
			}
		}

		//Limit the player's max velocity
		vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);

		//If you don't move right or left, then don't move
		if ((!right && !left) || (right && left)) {
			vel.x = 0;
		}

		//Jump and check if the button is still being held to vary jumps
		if (jump) {
			if ((Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.Z))) {
				switch (jumpCounter) {
				case 0:
					jumpVel = jumpVel; 
					break;
				case 1:
					jumpVel += 1;
					break; 
				case 2: 
					
					break;
				case 3:
					break; 
				case 4:
					break; 
				case 5: 
					jumpVel += 3;
					break;
				case 6: 
					jump = false;
					break;
				}
				jumpCounter++; 
				vel.y = jumpVel/waterSpeed; 

			}
		}
			
		if (jumpVel > maxJumpVel) {
			jumpVel = maxJumpVel;
		}

		if (GameManager.instance.sceneID == 3 || GameManager.instance.sceneID == 4) {
				GameManager.instance.monarchComeAlive = true; 
				GameManager.instance.playFlying = true; 
				if (GameManager.instance.monarchFlying == false) {
					vel.x = 0; 
					vel.y = 0; 
				}
		}
			
		rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null || Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("PlayerTop")) != null; 

		if (grounded && vel.y <= 0) {
			vel.y = 0; 
			canJump = true;
			jumpCounter = 0; 
			jumpVel = baseJumpVel; 
		} else {
			canJump = false;
			if (!swim) {
				vel.y += gravity;
			} else {
				vel.y += gravity *(waterSpeed / 2.5f);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll){

		if (coll.gameObject.tag == "Enemy") {

		}

	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Portal") {
			GameManager.instance.switchScene = true;
			GameManager.instance.fadeIn = false; 
		}

	}

	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.tag == "Water") {
			waterSpeed = 3; 
			swim = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Water") {
			waterSpeed = 1; 
			swim = false;
		}
	}
		
}