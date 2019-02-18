using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchComeAlive : MonoBehaviour {

	public GameObject aliveMonarch; 

	public GameObject player;

	float timer; 

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (GameManager.instance.sceneName == "Dream1") {
			if (player.transform.position.x >= transform.position.x - 5f) {
				timer += 1 * Time.deltaTime; 
				GameManager.instance.monarchComeAlive = true; 
				GameManager.instance.playFlying = true; 
			}
			if (timer >= 3f) {
				Instantiate (aliveMonarch, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}
}
