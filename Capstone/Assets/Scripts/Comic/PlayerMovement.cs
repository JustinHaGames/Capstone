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
	bool jump; 
	public float jumpVel;

	bool climbing; 
	public float climbVel; 

	public float boundaryL; 
	public float boundaryR;

	bool lastR; 
	bool lastL; 

	bool canAttack;

	public GameObject bullet; 

	float superJumpCounter; 
	bool superJump; 
	public float superJumpVel; 

	Color defaultColor;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer> ();

		canAttack = true;

		lastR = true;
		lastL = false;

		//save the initial color of the player
		defaultColor = GetComponent<SpriteRenderer>().color;

	}

	void Update(){
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && !jump) {
			jump = true;	
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
			vel.x += accel;
			lastR = true; 
			lastL = false; 
			sprite.flipX = false;

		}
		if (left) {
			vel.x -= accel;
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

		if (jump && grounded) {
			vel.y = jumpVel;
		}

		jump = false; 

		//If player is trying to go out of bounds
		if (transform.position.x <= boundaryL) {
			transform.position = new Vector3 (boundaryL, transform.position.y, transform.position.z); 
		} else if (transform.position.x >= boundaryR) {
			transform.position = new Vector3 (boundaryR, transform.position.y, transform.position.z); 
		}

		// Climb up and down the ladder
		if (climbing) {

			bool up = Input.GetKey (KeyCode.UpArrow);
			bool down = Input.GetKey (KeyCode.DownArrow);

			if (up) {
				vel.y = climbVel;
			} else if (down) {
				vel.y = -climbVel;
			}

			if (!up && !down) {
				vel.y = 0;
			}

		}

		//Shoot the lightning bullet in the given direction
		if (Input.GetKeyDown(KeyCode.X) && lastR && canAttack == true) {
			
			Instantiate (bullet, transform.position, Quaternion.identity);
			canAttack = false;
			StartCoroutine (HitDelay ());
		}

		if (Input.GetKeyDown(KeyCode.X) && lastL && canAttack == true) {
			
			LightningBullet temp = Instantiate (bullet, transform.position, Quaternion.identity).GetComponent<LightningBullet>() ;
			temp.speed *= -1;
			canAttack = false;
			StartCoroutine (HitDelay ());
		}

		//If superJump unlocked, allow players to do super jump
		if (GameManager.superJumpUpgrade) {
			if (Input.GetKey (KeyCode.DownArrow)) {
				superJumpCounter += 1 * Time.deltaTime; 
			}

			if (superJumpCounter >= 3) {
				superJump = true; 
				Flashing ();
			}

			if (superJump && Input.GetKeyDown (KeyCode.Z)) {
				vel.y = superJumpVel;
				superJumpCounter = 0; 
				sprite.color = defaultColor;
				superJump = false; 
			}

		}

		rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null; 

		if (grounded) {
			vel.y = 0; 
		} else {
			if (!climbing) {
				vel.y += gravity;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll){

		if (coll.gameObject.tag == "Enemy") {

			transform.position = new Vector3 (-7f, -3f, transform.position.z);

		}

	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Ladder") {
			climbing = true; 
		} 

		if (coll.gameObject.tag == "Portal") {
			GameManager.instance.switchScene = true;
			GameManager.instance.fadeIn = false; 
		}

	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Ladder") {
			climbing = false; 
		} 
	}

	float flashcounter;

	void Flashing(){
		flashcounter += 1; 

		if (flashcounter <= 10) {
			sprite.color = Color.yellow;
		} else {
			sprite.color = defaultColor;
		}

		if (flashcounter >= 20) {
			flashcounter = 0; 
		}
	}

	IEnumerator HitDelay(){
		for (int i = 0; i < 20; i++) {
			yield return new WaitForFixedUpdate();
		}
		canAttack = true;
	}
		
}