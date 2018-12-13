using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchMovement : MonoBehaviour {

	bool ascend; 

	float timer; 

	bool right; 
	bool left; 

	public float horizontalVel;

	GameObject player; 

	bool flyingRight;

	// Use this for initialization
	void Start () {
		right = true; 

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!GameManager.instance.playerFallen) {
			timer += Time.deltaTime;

			if (GameManager.instance.monarchComeAlive) {
			
				ascend = true; 
				GameManager.instance.monarchFlying = true; 

				if (timer < 6.5f) {
					transform.Translate (Vector3.up * Time.deltaTime);

				} else {
					ascend = false; 
					GameManager.instance.monarchFlying = false; 
					GameManager.instance.monarchComeAlive = false; 
					flyingRight = true; 
				}
				
			}

			if (flyingRight) {
				if (transform.position.x < 0) {
					transform.Translate (Vector2.right * Time.deltaTime * 5f);
				}
			}

		}

		if (GameManager.instance.playerFallen || GameManager.instance.sceneID == 5){

			timer += Time.deltaTime;

			if (GameManager.instance.monarchFlying) {
				if (transform.position.x <= -7f) {
					right = true; 
					left = false; 
				} else if (transform.position.x >= 7f) {
					left = true; 
					right = false; 
				}

				if (right) {
					transform.Translate (Vector3.right * Time.deltaTime * 3f);
				} else if (left) {
					transform.Translate (Vector3.left * Time.deltaTime * 3f);
				}

				player = GameObject.FindGameObjectWithTag ("Player");

				if (transform.position.y <= player.transform.position.y + 2f) {
					transform.position = new Vector3 (transform.position.x, player.transform.position.y + 2f, transform.position.z);
				}
			}

			if (timer >= 40f) {
				GameManager.instance.switchScene = true;
				GameManager.instance.fadeIn = false; 
			}
		}
	}
}
