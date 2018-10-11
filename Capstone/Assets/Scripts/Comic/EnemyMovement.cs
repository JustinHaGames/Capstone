using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Vector2 vel; 

	public Rigidbody2D rb; 

	public float accel; 
	public float maxAccel; 

	bool turning; 
	float count; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		count += 1 * Time.deltaTime; 

		if (count >= 3) {
			if (turning == false) {
				turning = true; 
			} else if (turning == true) {
				turning = false;
			}

			count = 0; 
		}

		if (turning) {
			vel.x += accel; 
		}

		if (!turning) {
			vel.x -= accel;
		}

		//Limit the enemy's max velocity
		vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);

		//Move the enemy 
		rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);
	}

	void HitByRay () {
		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Lightning") {
			Destroy (gameObject);
		}
	}
}
