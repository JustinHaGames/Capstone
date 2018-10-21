using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour {

	float timer; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1 * Time.deltaTime;

		if (timer >= 2f) {
			Destroy (gameObject);
		}
	}
}
