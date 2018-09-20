using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 

	bool grounded; 

	public float accel;
	public float maxAccel;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
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
		}
		if (left) {
			vel.x -= accel;
		}

		//Limit the player's max velocity
		vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);

		//If you don't move right or left, then don't move
		if (!right && !left) {
			vel.x = 0;
		}

		//Move the player according to the inputs made
		rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

		Debug.Log (vel.x);
	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null; 
	}
}
