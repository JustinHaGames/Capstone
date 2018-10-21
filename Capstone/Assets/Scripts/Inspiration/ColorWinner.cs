using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWinner : MonoBehaviour {

	SpriteRenderer sprite; 

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (InspirationManager.exit) {
			sprite.flipX = false;
			transform.Translate (Vector3.right * Time.deltaTime);
		}

		if (transform.position.x >= 10f) {
			InspirationManager.standUp = true;
			Destroy (gameObject);
		}

	}
}
