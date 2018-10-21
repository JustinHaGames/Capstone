using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {

	public GameObject colorWinner; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (InspirationManager.gotTrophy == true){
			Instantiate (colorWinner, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
