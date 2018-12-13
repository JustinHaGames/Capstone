using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeParallax : MonoBehaviour {

	public float parallaxSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.monarchComeAlive) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Translate (Vector2.left * parallaxSpeed * Time.deltaTime);
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (Vector2.right * parallaxSpeed * Time.deltaTime);
			}
		}
	}
}
