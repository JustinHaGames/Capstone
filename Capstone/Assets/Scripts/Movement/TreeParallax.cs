using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeParallax : MonoBehaviour {

	public float parallaxSpeed;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!GameManager.instance.monarchComeAlive && player.transform.position.x > -88f) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Translate (Vector2.left * parallaxSpeed * Time.deltaTime);
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (Vector2.right * parallaxSpeed * Time.deltaTime);
			}
		}
	}
}
