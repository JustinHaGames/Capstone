using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public Transform player; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.x >= (transform.position.x + 9)) {
			transform.position = new Vector3 (transform.position.x + 19, transform.position.y, transform.position.z);; 
		}
	}
}
