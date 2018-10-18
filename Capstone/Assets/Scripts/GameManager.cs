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

	public bool playerSwitch;

	public static bool superJumpUpgrade; 

	public int sceneID;

	// Use this for initialization
	void Start () {
		alphaNum = 1f;
		fadeIn = true;
		instance = this; 
	}
	
	// Update is called once per frame
	void Update () {
		//Always fade in to a new scene
		if (fadeIn && alphaNum > 0f) {
			alphaNum -= .25f * Time.deltaTime;
		} else if (!fadeIn && alphaNum < 1f) {
			alphaNum += .25f * Time.deltaTime;
		}

		if (alphaNum >= 1f) {
			alphaNum = 1f;
		} else if (alphaNum <= 0f) {
			alphaNum = 0f; 
		}

		//Call the scene change coroutine when switchScene is called
		if (switchScene) {
			StartCoroutine (SceneChange ());
		}

		//Get the current scene you are on
		sceneID = SceneManager.GetActiveScene ().buildIndex;

		if (sceneID == 1) {
			superJumpUpgrade = true; 
		}

	}

	//After a short delay, change to a different scene
	IEnumerator SceneChange(){
		for (int i = 0; i < 80; i++) {
			yield return new WaitForFixedUpdate();
		}
		SceneManager.LoadScene (sceneID += 1);
	}
}
