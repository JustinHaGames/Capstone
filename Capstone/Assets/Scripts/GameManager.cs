using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Singleton
	public static GameManager instance;

	//The transparency number for the fade screen that goes from 0-1
	public float alphaNum; 
	public bool fadeIn;

	public bool switchScene; 
	// Use this for initialization
	void Start () {
		alphaNum = 1f;
		fadeIn = true;
		instance = this; 

	}
	
	// Update is called once per frame
	void Update () {
		if (fadeIn && alphaNum > 0f) {
			alphaNum -= .5f * Time.deltaTime;
		} else if (!fadeIn && alphaNum < 1f) {
			alphaNum += .5f * Time.deltaTime;
		}

		if (alphaNum >= 1f) {
			alphaNum = 1f;
		} else if (alphaNum <= 0f) {
			alphaNum = 0f; 
		}

		if (switchScene) {
			StartCoroutine (SceneChange ());
		}

	}

	IEnumerator SceneChange(){
		for (int i = 0; i < 20; i++) {
			yield return new WaitForFixedUpdate();
		}
		SceneManager.LoadScene (1);
	}
}
