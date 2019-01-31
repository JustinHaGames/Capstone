using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 
	SpriteRenderer sprite; 

	Animator anim;

	bool grounded;

	public float accel;
	public float maxAccel;

	public float gravity; 
	public float maxGravity;
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

	//Swim variables
	float waterSpeed = 1;
	bool swim;

	public LayerMask blockMask;

	//Knockback variables
	bool knockedBack;
	public float knockbackSpeed;
	bool inactive;

	//Walljump layermask
	public LayerMask wallMask;
	public float wallJumpX;
	public float wallJumpY;

	//set walljump direction to -1 or 1
	float wallJumpDirection;

	bool falling;
	public Material playerMat;
	public Material defaultMat;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

		lastR = true;
		lastL = false;

		heldObject = null;

	}

	void Update(){

		if (GameManager.instance.sceneName == "Dream1") {
			if (falling) {
				sprite.material = playerMat;
				GameManager.instance.playerFallen = true;
			} else {
				sprite.material = defaultMat;
			}
		}

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

//		//Walljump
//		if (lastL) {
//			Vector3 facingDirection = Vector3.left;
//			RaycastHit2D wallHit = Physics2D.Raycast (transform.position, facingDirection, 1f, wallMask);
//			wallJumpDirection = 1f;
//			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Space)) {
//				StartCoroutine (WallJump ());
//			}
//		} else if (lastR) {
//			Vector3 facingDirection = Vector3.right;
//			RaycastHit2D wallHit = Physics2D.Raycast (transform.position, facingDirection, 1f, wallMask);
//			wallJumpDirection = -1f;
//			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Space)) {
//				StartCoroutine (WallJump ());
//			}
//		}

		//Pickup objects
		if (Input.GetKeyDown (KeyCode.X)) {
			//Box kicking test
			if (heldObject == null) {
				if (lastR) {
					Vector3 facingDirection = Vector3.right;
					RaycastHit2D hit = Physics2D.Raycast (transform.position + (-facingDirection * sprite.bounds.extents.x * .5f) + (Vector3.down * sprite.bounds.extents.y * 0.3f), facingDirection, 1.5f, blockMask);
					heldObject = hit.collider.gameObject;
					heldObject.SendMessage ("PickUp", SendMessageOptions.DontRequireReceiver);
				} else if (lastL) {
					Vector3 facingDirection = Vector3.left;
					RaycastHit2D hit = Physics2D.Raycast (transform.position + (-facingDirection * sprite.bounds.extents.x) + (Vector3.down * sprite.bounds.extents.y * 0.3f), facingDirection, 1.5f,blockMask);
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
				} else if (Input.GetKeyDown (KeyCode.X)){
					heldObject.SendMessage ("Drop", SendMessageOptions.DontRequireReceiver);
					heldObject = null;
				}
			}
		}

		//Only do this in scene 4
		if (GameManager.instance.sceneName == "BoxCloset") {
			if (GameManager.instance.targetHit > 7) {
				if (heldObject.tag == "SpecialBox") {
					GameManager.instance.switchScene = true; 
				}
			}
		}

		//Only do this in scene 5
		if (GameManager.instance.sceneName == "Dream2") {
			if (!GameManager.instance.specialBoxPickedUp) {
				if (heldObject != null) {
					if (heldObject.tag == "SpecialBox") {
						GameManager.instance.specialBoxPickedUp = true;
					}
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
				vel.x += accel / waterSpeed;
				lastR = true; 
				lastL = false; 
				if (!inactive) {
					sprite.flipX = false;
                if (grounded)
                {
                    anim.Play("RunningAnimation");
                }
				}
			}
			if (left) {
				vel.x -= accel / waterSpeed;
				lastL = true; 
				lastR = false; 
				if (!inactive) {
					sprite.flipX = true;
                if (grounded)
                {
                    anim.Play("RunningAnimation");
                }
				}
			}

		//Limit the player's max velocity
		vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);

		//If you don't move right or left, then don't move
		if ((!right && !left) || (right && left) || inactive ) {
            if (grounded)
            {
                anim.Play("Idle");
            }
			vel.x = 0;
		}

		//Jump and check if the button is still being held to vary jumps
		if (jump) {
           anim.Play("Jump");
            if ((Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.Z))) {
				switch (jumpCounter) {
				case 0:
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
				if (!swim) {
					vel.y = jumpVel;
				} else {
					vel.y = jumpVel / (waterSpeed * .75f); 
				}

			}
		}
			
		if (jumpVel > maxJumpVel) {
			jumpVel = maxJumpVel;
		}

		if (GameManager.instance.sceneName == "Dream1") {
			if (GameManager.instance.monarchComeAlive == true) {
				inactive = true;
				canJump = false;
			} else {
				inactive = false;
			}
		}
			
		rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null || Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("PlayerTop")) != null ; 

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
				vel.y += gravity / waterSpeed;
			}
		}

		if (vel.y <= maxGravity) {
			vel.y = maxGravity;
		}
	}

	void OnCollisionEnter2D (Collision2D coll){

		if (coll.gameObject.tag == "Enemy") {
			GameObject collidedObject = coll.collider.gameObject; 
			if (transform.position.y >= collidedObject.transform.position.y + 1f) {
				collidedObject.SendMessage ("Dead", SendMessageOptions.DontRequireReceiver);
				vel.y = 12f;
			} else {
				StartCoroutine (KnockedBack ());
				vel.x = (collidedObject.transform.position.x >= transform.position.x)? vel.x - knockbackSpeed : vel.x + knockbackSpeed;
				vel.y = 3f;
			}
		}

		if (coll.gameObject.tag == "PlayerTop") {
			GameObject collidedObject = coll.collider.gameObject; 
			if (transform.position.y >= collidedObject.transform.position.y + 1f) {
				collidedObject.SendMessage ("PickUp", SendMessageOptions.DontRequireReceiver);
			}
		}

		if (coll.gameObject.tag == "BrokenFloor") {
			falling = true;
		}

	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Water") {
			waterSpeed = 3; 
			vel.y = vel.y / 3;
			swim = true;
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
		
	IEnumerator KnockedBack() 
	{
		inactive = true;
		for (int f = 0; f < 15; f++) 
		{
			maxAccel = knockbackSpeed;
			yield return new WaitForFixedUpdate ();
		}
		maxAccel = 6f;
		inactive = false;
	}

	IEnumerator WallJump(){
		inactive = true;
		for (int f = 0; f < 4; f++) {
			vel.x = wallJumpX * wallJumpDirection;
			vel.y = wallJumpY;
			yield return new WaitForFixedUpdate ();
		}
		inactive = false;
	}
		
}