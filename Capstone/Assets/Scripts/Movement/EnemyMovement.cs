using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Rigidbody2D rb;
	public GameObject box;

	GameObject player;

	public float speed;

	SpriteRenderer sprite;

	Vector3 vel;
	Vector3 dir;
	Vector3 newDir; 

	bool launch; 

	public float rightBarrier;
	public float leftBarrier;

	float timer;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		dir = (player.transform.position - transform.position).normalized;
		launch = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Flip the sprite so the enemy is always looking at the player
		if (transform.position.x > player.transform.position.x) {
			sprite.flipX = true;
		} else {
			sprite.flipX = false;
		}

		if (dir.x > 0) { 
			if (transform.position.x > player.transform.position.x) {
				launch = false;
				timer = 0;
			}
		} else if (dir.x < 0) {
			if (transform.position.x < player.transform.position.x) {
				launch = false;
				timer = 0;
			}
		}

		if (!launch && (transform.position.x >= rightBarrier || transform.position.x <= leftBarrier)) {
			timer += 1 * Time.deltaTime;
			newDir = (player.transform.position - transform.position).normalized;
			if (timer <= 1.5f) {
				vel.x = 0; 
				vel.y = 0;
			} else {
				vel = newDir * speed;
				launch = true;
			} 
		}

		vel = dir * speed;

		rb.MovePosition (transform.position + vel * Time.deltaTime);

	}
		
	void OnCollisionEnter2D(Collision2D coll){

	}

	public void Dead (){
		Instantiate (box, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
