using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform realityPlayer; 

	GameObject comicPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (GameManager.instance.playerSwitch == false) {
			transform.position = realityPlayer.transform.position; 
		} else {

			comicPlayer = GameObject.FindGameObjectWithTag ("Player");

			transform.position = comicPlayer.transform.position; 

		}

	}
}
