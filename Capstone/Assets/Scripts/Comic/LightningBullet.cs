using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : MonoBehaviour {

	float count = 0; 

	public float speed;  

	public float duration; 

	GameObject room; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector2.right * Time.deltaTime * speed);

		count += 1 * Time.deltaTime;

		if (count >= duration) {
			Destroy (gameObject);
		}

		room = GameObject.FindGameObjectWithTag ("RoomManager");

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}
	}

}
