using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamCamera : MonoBehaviour {

	public Transform player; 

	public float yTopDif;

	public float yBottomDif; 

	float timer;

	bool moving; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 movePosition = this.transform.position;

		if (player.transform.position.y >= transform.position.y + yTopDif) {
			moving = true;
			movePosition.y = Mathf.Lerp (transform.position.y, player.position.y, timer);
		} else if (player.transform.position.y <= transform.position.y - yBottomDif) {
			moving = true;
			movePosition.y = Mathf.Lerp (transform.position.y, player.position.y, timer);
		} else {
			moving = false; 
		}

		if (moving) {
			timer += Time.deltaTime;
		} else {
			timer = 0;
		}

		transform.position = movePosition;

	}
}
