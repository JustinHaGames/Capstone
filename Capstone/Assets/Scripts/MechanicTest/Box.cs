﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 

	bool grounded; 
	bool stacked; 

	bool set;
	bool setChecked; 

	bool held; 

	Vector3 setPosition; 

	public float throwVel; 
	public float upThrowVel;
	public float downThrowVel; 

	public float gravity; 

	//Was last action left or right throw?
	bool lastLR;

	public Transform player; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		box = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		Grounded (); 

		if (set) {
			if (!setChecked) {
				setPosition = transform.position; 
				setChecked = true;
			}
			rb.isKinematic = true; 
			vel.x = 0; 
			transform.position = setPosition; 
			lastLR = false; 
		}

		RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - .25f), Vector2.down * .5f);
		Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y - .25f), Vector2.down * .5f, Color.red);
	
		if (hit.collider.gameObject.layer == 14) {
			set = true; 
		}

		if (!held || !set) {
			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);
		} 
	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null; 

		if (grounded) {
			set = true; 
		} else {
			//If you threw the box and the last throw was not thrown left or right
			if (!held && !lastLR) {
				vel.y += gravity;
			} else {
				//if you haven't throw it, don't apply gravity
				vel.y = 0; 
			}
			//if you're holding the box, don't set it
			set = false; 
			setChecked = false; 
		}

	}

	public void PickUp() {
		Debug.Log ("Pick up");
		rb.isKinematic = true; 
		transform.position = player.transform.position; 
		transform.parent = player.transform;
		held = true;
	}

	public void Up () {
		Debug.Log ("Up");
		transform.parent = null;
		rb.isKinematic = false;
		vel.y = upThrowVel; 
		lastLR = false;
		held = false; 
	}

	public void Right () {
		Debug.Log ("Right");
		transform.parent = null;
		rb.isKinematic = false;
		vel.x = throwVel; 
		lastLR = true; 
		held = false; 
	}

	public void Left () {
		Debug.Log ("Left");
		transform.parent = null;
		rb.isKinematic = false;
		vel.x = -throwVel; 
		lastLR = true;
		held = false; 
	}

	public void Down () {
		Debug.Log ("Up");
		transform.parent = null;
		rb.isKinematic = false;
		vel.y = downThrowVel; 
		lastLR = false;
		held = false; 
	}

	public void Drop () {
		Debug.Log ("Drop");
		transform.parent = null;
		rb.isKinematic = false;
		held = false; 
	}
}
