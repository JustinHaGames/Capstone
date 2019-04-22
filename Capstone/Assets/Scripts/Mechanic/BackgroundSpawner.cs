using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

	public GameObject player;

	public GameObject stoneWall;

	public float spawnDistance;

	bool spawned;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawned) {
			if (player.transform.position.y >= transform.position.y) {
				Instantiate (stoneWall, new Vector3 (transform.position.x, transform.position.y + spawnDistance, transform.position.z), Quaternion.identity);
				spawned = true;
			}
		}
	}
}
