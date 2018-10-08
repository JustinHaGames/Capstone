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

	public float boundaryL; 
	public float boundaryR;

	public GameObject comicPlayer; 

	bool imagining; 

	bool inactive; 

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
				if (!left) {
					sprite.flipX = false;
				}
			}
			if (left) {
				vel.x -= accel;
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

			//If player is trying to go out of bounds
			if (transform.position.x <= boundaryL) {
				transform.position = new Vector3 (boundaryL, transform.position.y, transform.position.z); 
			} else if (transform.position.x >= boundaryR) {
				transform.position = new Vector3 (boundaryR, transform.position.y, transform.position.z); 
				imagining = true; 
				inactive = true; 
			}

			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);
		}

		if (imagining) {
			Instantiate (comicPlayer, new Vector3 (transform.position.x + 8f, transform.position.y, transform.position.z), Quaternion.identity);
			imagining = false; 
			GameManager.instance.playerSwitch = true; 
		}

	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Portal") {
			GameManager.instance.switchScene = true;
			GameManager.instance.fadeIn = false; 
		}

	}

	void OnTriggerExit2D(Collider2D coll){
	}
		
}