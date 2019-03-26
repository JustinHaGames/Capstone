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

    bool flipVelY;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		dir = (player.transform.position - transform.position).normalized;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (this.gameObject.tag == "Enemy")
        {
            //Flip the sprite so the enemy is always looking at the player
            if (transform.position.x > player.transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }

		vel = dir * speed;

        if (GameManager.instance.currentSpot >= 8 || transform.position.x <= -20f || transform.position.x >= 20f)
        {
            Destroy(gameObject);
        }

        rb.MovePosition (transform.position + vel * Time.deltaTime);
	}
		
	void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "BoxPusher" || coll.gameObject.tag == "BoxTop" || coll.gameObject.tag == "Box")
        {
            Dead();
        }

        if (coll.gameObject.tag == "Floor")
        {
            dir.y *= -1f;
        }

    }

	public void Dead (){
		Instantiate (box, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
