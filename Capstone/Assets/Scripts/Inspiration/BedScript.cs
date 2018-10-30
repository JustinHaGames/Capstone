using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour {

	public Transform player; 

	public GameObject inBed; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x <= -6.5f) {
			Instantiate (inBed, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
