﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingHome : MonoBehaviour {

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

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {

			//Press the left and right keys to move
			bool left = Input.GetKey (KeyCode.LeftArrow);

			if (left) {
				vel.x -= accel;

			}

			//Limit the player's max velocity
			vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);

			//If you don't move right or left, then don't move
			if (!left) {
				vel.x = 0;
			}
			
		if (transform.position.x <= -51f) {
			GameManager.instance.switchScene = true;
			GameManager.instance.fadeIn = false; 
		}


			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

	}
}