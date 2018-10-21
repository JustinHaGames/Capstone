﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour {

	bool descend; 

	public GameObject particleEffect; 

	bool effectPlayed; 


	// Use this for initialization
	void Start () {
		descend = true; 
	}
	
	// Update is called once per frame
	void Update () {

		if (InspirationManager.activateScene) {
			if (descend) {
				transform.Translate ((Vector2.down * Time.deltaTime) * 1.75f);
			} 

			if (transform.position.y <= -.8f) {
				transform.position = new Vector3 (transform.position.x, -.8f, transform.position.z); 
				InspirationManager.gotTrophy = true; 
				descend = false; 
				if (!effectPlayed) {
					Instantiate (particleEffect, transform.position, Quaternion.identity);
					effectPlayed = true; 
				}
			}
		}

		if (InspirationManager.exit) {
			transform.Translate (Vector3.right * Time.deltaTime);
		}

		if (transform.position.x >= 10f) {
			Destroy (gameObject);
		}
	}
}
