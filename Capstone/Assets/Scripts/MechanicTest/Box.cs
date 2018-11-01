using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 

	bool grounded; 

	bool set;
	bool setChecked; 

	bool held; 

	Vector3 setPosition; 

	public float gravity; 

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
			transform.position = setPosition; 
		}

		if (!held) {
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
			if (!held) {
				vel.y += gravity;
			}
			set = false; 
			setChecked = false; 
		}

	}

	public void PickUp(){
		Debug.Log ("Pick up");
		gameObject.layer = 15; 
		rb.isKinematic = true; 
		transform.position = player.transform.position; 
		transform.parent = player.transform;
		held = true;
	}

	public void Drop () {
		Debug.Log ("Drop");
		transform.parent = null;
		rb.isKinematic = false;
		gameObject.layer = 14; 
		held = false; 
	}
}
