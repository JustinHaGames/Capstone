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

	// Use this for initialization
	void Start () {
		right = true; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		timer += Time.deltaTime;

		if (GameManager.instance.monarchComeAlive) {
			
			ascend = true; 


			if (timer < 7f) {
				transform.Translate (Vector3.up * Time.deltaTime);

			}else{
				ascend = false; 
				GameManager.instance.monarchFlying = true; 
				GameManager.instance.monarchComeAlive = false; 
			}

		}

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
				transform.Translate (Vector3.up * Time.deltaTime * 1.5f);
			}

			if (timer >= 40f) {
				GameManager.instance.switchScene = true;
				GameManager.instance.fadeIn = false; 
			}
		}
	}
}
