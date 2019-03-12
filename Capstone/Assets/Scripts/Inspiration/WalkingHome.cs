using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingHome : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	SpriteRenderer sprite;

    Animator anim;

	public float accel;
	public float maxAccel;

	public float sceneChangeSpot; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator>();
		sprite.flipX = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
    
        float xInput = Input.GetAxis("Horizontal");

        //Move Left
        if (xInput < 0) {
            anim.Play("ButtonDownPlayerWalking");
			vel.x -= accel;
			sprite.flipX = true;
		}

        //Move Right
		if (xInput > 0) {
            anim.Play("ButtonDownPlayerWalking");
            vel.x += accel;
			sprite.flipX = false;
		}

		//Limit the player's max velocity
        vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);
        Debug.Log(Mathf.Abs(xInput));
        //If you don't move right or left, then don't move
        if (Mathf.Abs(xInput) < 0.1f)
        {
            anim.Play("ButtonDownPlayerIdle");
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