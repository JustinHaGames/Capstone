using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 

	bool grounded; 
	bool stacked; 

	bool set;

	bool held; 

	Vector3 setPosition; 

	public float throwVel; 
	public float upThrowVel;

	public float gravity; 

	//Was last action left or right throw?
	bool lastL;
	bool lastR; 

	public Transform player; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		box = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Grounded (); 

		if (!held) {
			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);
		} 
	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null || Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("BoxTop")) != null; 

		RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - .25f), Vector2.down * .5f);

		if (grounded) {
			vel.x = 0; 
			vel.y = 0; 
			transform.GetChild(0).gameObject.SetActive(true);
			rb.isKinematic = true; 
			lastL = false;
			lastR = false; 
		} else {
			//If you threw the box and the last throw was not thrown left or right
			if (!held && !lastL && !lastR) {
				vel.y += gravity;
			} else {
				//if you haven't throw it, don't apply gravity
				vel.y = 0; 
			}
			transform.GetChild(0).gameObject.SetActive(false);
		}

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (lastL || lastR) {
			if (coll.gameObject.tag == "Box") {
				lastL = false; 
				lastR = false; 
				vel.x = 0; 
			}
			if (coll.gameObject.tag == "Wall") {
				lastL = false; 
				lastR = false; 
				vel.x = 0; 
			}
		}
	}

	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.tag == "Water") {
				vel.x += 1 * Time.deltaTime; 
		}
	}

	public void PickUp() {
		rb.isKinematic = true; 
		transform.position = player.transform.position; 
		transform.parent = player.transform;
		held = true;
	}

	public void Up () {
		transform.parent = null;
		rb.isKinematic = false;
		vel.y = upThrowVel; 
		lastL = false;
		lastR = false; 
		held = false; 
	}

	public void Right () {
		transform.parent = null;
		rb.isKinematic = false;
		vel.x = throwVel; 
		lastR = true; 
		held = false; 
	}

	public void Left () {
		transform.parent = null;
		rb.isKinematic = false;
		vel.x = -throwVel; 
		lastL = true;
		held = false; 
	}

	public void Down () {
		transform.parent = null;
		rb.isKinematic = false;
		vel.y = -upThrowVel; 
		lastL = false;
		lastR = false; 
		held = false; 
	}

	public void RightDiagonal (){
		transform.parent = null;
		rb.isKinematic = false;
		vel.y = upThrowVel; 
		vel.x = throwVel;
		lastL = false;
		lastR = false; 
		held = false; 
	}

	public void LeftDiagonal () {
		transform.parent = null;
		rb.isKinematic = false;
		vel.y = upThrowVel; 
		vel.x = -throwVel;
		lastL = false;
		lastR = false; 
		held = false; 
	}

	public void Drop () {
		transform.parent = null;
		rb.isKinematic = false;
		held = false; 
	}
}
