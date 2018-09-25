using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//Singleton
	public static GameManager instance;

	//The transparency number for the fade screen that goes from 0-1
	public float alphaNum; 
	public bool fadeIn;

	// Use this for initialization
	void Start () {
		alphaNum = 1f;
		fadeIn = true;
		instance = this; 

	}
	
	// Update is called once per frame
	void Update () {
		if (fadeIn && alphaNum > 0f) {
			alphaNum -= 1 * Time.deltaTime;
		} else if (!fadeIn && alphaNum < 1f) {
			alphaNum += 1 * Time.deltaTime;
		}

		if (alphaNum >= 1f) {
			alphaNum = 1f;
		} else if (alphaNum <= 0f) {
			alphaNum = 0f; 
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if (fadeIn) {
				fadeIn = false;
			} else {
				fadeIn = true;
			}
		}

	}
}
