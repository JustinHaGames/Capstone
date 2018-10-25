using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchComeAlive : MonoBehaviour {

	public GameObject aliveMonarch; 

	float timer; 

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.monarchComeAlive) {
			timer += 1 * Time.deltaTime; 

			if (timer >= 3f) {
				Instantiate (aliveMonarch, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}
}
