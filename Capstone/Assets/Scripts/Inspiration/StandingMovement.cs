using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingMovement : MonoBehaviour {

	float timer; 

	SpriteRenderer sprite; 

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1 * Time.deltaTime; 

		if (timer >= 3f) {
			sprite.flipX = true; 
			transform.Translate (Vector3.left * Time.deltaTime);
		}

		if (transform.position.x <= -10f) {
			GameManager.instance.switchScene = true;
			GameManager.instance.fadeIn = false;
		}

	}
}
