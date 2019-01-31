using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTop : MonoBehaviour {

	GameObject player; 

	BoxCollider2D box;

	// Use this for initialization
	void Start () {
        box = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		player = GameObject.FindGameObjectWithTag ("Player");

		if (player.transform.position.y >= transform.position.y + .5f) {
			box.enabled = true;
		}
	}
}
