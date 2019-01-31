using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingHome : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	SpriteRenderer sprite; 

	public float accel;
	public float maxAccel;

	public float sceneChangeSpot; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer> ();
		sprite.flipX = true;
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {

			//Press the left and right keys to move
			bool left = Input.GetKey (KeyCode.LeftArrow);
			bool right = Input.GetKey (KeyCode.RightArrow);

		if (left) {
			vel.x -= accel;
			sprite.flipX = true;
		}

		if (right) {
			vel.x += accel;
			sprite.flipX = false;
		}

			//Limit the player's max velocity
			vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);

			//If you don't move right or left, then don't move
		if (!left && !right) {
				vel.x = 0;
			}
			
		if (transform.position.x <= sceneChangeSpot) {
			GameManager.instance.switchScene = true;
			GameManager.instance.fadeIn = false; 
			vel.x = 0; 
			if (GameManager.instance.sceneName == "GoingToSleep") {
				sprite.enabled = false; 
			}
		}


			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

	}
}