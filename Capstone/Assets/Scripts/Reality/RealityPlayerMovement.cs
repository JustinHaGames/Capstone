using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityPlayerMovement : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 
	SpriteRenderer sprite; 

	bool grounded;

	public float accel;
	public float maxAccel;

	public GameObject comicPlayer; 

	bool imagining; 

	bool inactive; 

	public GameObject heldObject;

	bool lastR; 
	bool lastL; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer> ();

		inactive = false;

	}

	void Update(){

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!inactive) {
			//Press the left and right keys to move
			bool right = Input.GetKey (KeyCode.RightArrow);
			bool left = Input.GetKey (KeyCode.LeftArrow);

			//If right or left if pressed, accel in that direction
			if (right) {
				vel.x += accel;
				lastR = true;
				lastL = false;
				if (!left) {
					sprite.flipX = false;
				}
			}
			if (left) {
				vel.x -= accel;
				lastR = false;
				lastL = true;
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

			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);
		}

		if (imagining) {
			Instantiate (comicPlayer, new Vector3 (transform.position.x + 8f, transform.position.y, transform.position.z), Quaternion.identity);
			imagining = false; 
			GameManager.instance.playerSwitch = true; 
		}

	}

	void OnCollisionEnter2D (Collision2D coll){
		
	}

	void OnTriggerEnter2D(Collider2D coll){

	}

	void OnTriggerExit2D(Collider2D coll){
	}
		
}