using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject Enemy;

	float timer;

	float randomNum;

	bool chosen;
	// Use this for initialization
	void Start () {
		chosen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.specialBoxKilled) {
			timer += 1 * Time.deltaTime;
		}

		if (!chosen) {
			randomNum = Random.Range (5, 10);
			chosen = true;
		}

		if (timer >= randomNum) {
			Instantiate (Enemy, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
			chosen = false;
			timer = 0;
		}
	}
}
