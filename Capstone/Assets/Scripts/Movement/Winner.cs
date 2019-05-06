using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {

	SpriteRenderer sprite; 

	public Sprite colorWinner;

	bool walkRight;
	bool walkLeft;

	bool moving;

	float timer;

    Animator anim;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (InspirationManager.gotTrophy == true && !moving){
			timer += Time.deltaTime;
			sprite.sprite = colorWinner;
			if (timer >= 2f) {
				walkRight = true;
				moving = true;
			}
		}

		if (walkRight) {
			transform.Translate (Vector3.right * 2f * Time.deltaTime);
			sprite.flipX = false;
            anim.Play("WinnerWalk");
			if (transform.position.x >= 10f) {
				transform.position = new Vector3 (transform.position.x,-3.41f,transform.position.z);
				walkLeft = true;
				walkRight = false;
			}
		}

		if (walkLeft) {
			transform.Translate (Vector3.left * 2f * Time.deltaTime);
			sprite.flipX = true;
            anim.Play("WinnerWalk");
			if (transform.position.x <= 2f) {
				InspirationManager.moveCrowd = true;
				walkLeft = false;
			}
		}

        if (!walkLeft && !walkRight)
        {
            if (InspirationManager.gotTrophy)
            {
                anim.Play("WinnerIdle");
            } else
            {
                anim.Play("WinnerGreyIdle");
            }
        }
	}
}
