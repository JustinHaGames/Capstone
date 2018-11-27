using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingMovement : MonoBehaviour {

	float timer; 

	SpriteRenderer sprite; 

	bool imagining; 

	bool left; 
	bool right;

	int counter = 0;

	public GameObject dreamPlayer;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		left = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Do this in the first scene
		if (GameManager.instance.sceneID == 0) {
			timer += 1 * Time.deltaTime; 
			sprite.flipX = true; 
			transform.Translate (Vector3.left * Time.deltaTime);

			if (transform.position.x <= -10f) {
				GameManager.instance.switchScene = true;
				GameManager.instance.fadeIn = false;
			}

		}

		if (GameManager.instance.sceneID == 4) {
			
			if (left) {
				transform.Translate (Vector3.left * Time.deltaTime * 2f);
			}

			if (right) {
				transform.Translate (Vector3.right * Time.deltaTime * 2f);
			}

			if (transform.position.x <= -6f) {
				left = false; 
				right = true;
				sprite.flipX = false;
				counter += 1;
			} else if (transform.position.x >= 6f) {
				left = true;
				right = false;
				sprite.flipX = true;
				counter += 1;
			}

			if (counter == 4) {
				if (GameManager.instance.playerSwitch == false) {
					Instantiate (dreamPlayer, transform.position, Quaternion.identity);
					GameManager.instance.playerSwitch = true;
				}
			}

		}

	}
}
