using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	int health = 10; 

	bool hit; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hit) {
			health -= 1; 
			hit = false; 
		}

		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Lightning") {
			hit = true; 
		}
	}
}
