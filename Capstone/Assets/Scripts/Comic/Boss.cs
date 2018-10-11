using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	int health = 10; 

	bool hit; 

	SpriteRenderer sprite;

	Color defaultColor; 

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();	
		defaultColor = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (hit) {
			health -= 1;
			sprite.color = Color.white;
			hit = false; 
		} else {
			sprite.color = defaultColor;
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
