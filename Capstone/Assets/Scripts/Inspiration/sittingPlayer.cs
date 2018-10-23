using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sittingPlayer : MonoBehaviour {

	public GameObject standingPlayer; 

	float timer; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (InspirationManager.standUp) {
			timer += 1 * Time.deltaTime;
			if (timer >= 5f) {
				Instantiate (standingPlayer, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
		}

	}
}
