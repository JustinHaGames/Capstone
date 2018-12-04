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

		if (InspirationManager.moveCrowd) {
				Instantiate (standingPlayer, transform.position, Quaternion.identity);
				Destroy (gameObject);
		}

	}
}
