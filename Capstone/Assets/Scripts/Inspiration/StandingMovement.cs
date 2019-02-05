using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingMovement : MonoBehaviour {

	float timer; 

	SpriteRenderer sprite; 

	Sprite defaultSprite;

	bool imagining; 

	bool left; 
	bool right;

	int counter = 0;

	public Sprite fallenPlayer; 

	bool fall;
	bool leave;

	float fallTimer;
	float leaveTimer;

	public GameObject dreamPlayer;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		defaultSprite = sprite.sprite;
		left = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Do this in the first scene
		if (GameManager.instance.sceneName == "Inspiration") {

			if (transform.position.y <= -3.41f) {
				transform.position = new Vector3 (transform.position.x, -3.41f, transform.position.z);
			}

			if (InspirationManager.moveCrowd && !fall && !leave) {
				timer += Time.deltaTime;
				if (timer >= 1f && transform.position.x < -.5f) {
					transform.Translate (Vector3.right * 3f * Time.deltaTime);
					if (transform.position.x >= -.5f) {
						fall = true;
					}
				}
			}

			if (fall) {
				sprite.sprite = fallenPlayer; 
				fallTimer += 1 * Time.deltaTime;
				if (fallTimer < .5f) {
					transform.Translate (Vector3.up * 3f * Time.deltaTime);
					transform.Translate (Vector3.left * 1f * Time.deltaTime);
				} else {
					transform.Translate (Vector3.down * 3f * Time.deltaTime);
					transform.Translate (Vector3.left * 1f * Time.deltaTime);
				}
				if (fallTimer >= 1f) {
					sprite.sprite = defaultSprite;
					leave = true;
					fall = false;
				}
			}

			if (leave) {
				leaveTimer += 1 * Time.deltaTime;
				if (leaveTimer >= 2f) {
					sprite.flipX = true;
					transform.Translate (Vector3.left * 1f * Time.deltaTime);
				}
			}

			if (transform.position.x <= -10f) {
				GameManager.instance.switchScene = true;
				GameManager.instance.fadeIn = false;
			}

		}

        //Do this in the 4th scene
        if (GameManager.instance.sceneName == "BoxCloset") {
			
			if (left) {
				transform.Translate (Vector3.left * Time.deltaTime * 2f);
				sprite.flipX = true;
			}

			if (right) {
				transform.Translate (Vector3.right * Time.deltaTime * 2f);
				sprite.flipX = false;
			}

			if (transform.position.x <= -6f) {
				left = false; 
				right = true;
				//sprite.flipX = false;
				counter += 1;
			} else if (transform.position.x >= 6f) {
				left = true;
				right = false;
				//sprite.flipX = true;
				counter += 1;
			}

			if (counter == 2) {
				if (GameManager.instance.playerSwitch == false) {
					GameManager.instance.playerImagining = true; 
					Instantiate (dreamPlayer, transform.position, Quaternion.identity);
					GameManager.instance.playerSwitch = true;
				}
			}

		}
	}

	IEnumerator Fall() 
	{
		for (float f = 10f; f >= 0; f -= 0.1f) 
		{
			float newTimer = 0;
			newTimer += Time.deltaTime;

			if (newTimer < .5f) {
				transform.Translate (Vector3.up * 3f * Time.deltaTime);
				transform.Translate (Vector3.left * 1f * Time.deltaTime);
			} else {
				transform.Translate (Vector3.down * 3f * Time.deltaTime);
				transform.Translate (Vector3.left * 1f * Time.deltaTime);
			}
			yield return null;
		}
	}

}
