using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	bool rising;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//In scene 5, water shouldn't rise until the monarch is flying
		if (GameManager.instance.sceneID == 3) {
			if (GameManager.instance.monarchFlying) {
				rising = true;
			}
		}

		//In scene 5, water shouldn't rise until the box is picked up
		if (GameManager.instance.sceneID == 5) {
			if (GameManager.instance.specialBoxPickedUp) {
				rising = true; 
			}
		}

		if (rising) {
			transform.Translate ((Vector3.down * Time.deltaTime) * -.25f);
		}
	}
}
