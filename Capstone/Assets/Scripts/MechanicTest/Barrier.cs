using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

	public GameObject colorBox; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.targetHit > 7) {
			Instantiate (colorBox, new Vector3(0,0,-1f), Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
