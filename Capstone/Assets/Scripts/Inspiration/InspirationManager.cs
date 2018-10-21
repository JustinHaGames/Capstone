using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspirationManager : MonoBehaviour {

	AudioSource audio; 

	public AudioClip winner; 

	public static bool gotTrophy; 

	public static bool activateScene; 

	public static bool exit; 

	public static bool standUp; 

	float exitTimer; 

	bool playAudio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> (); 
		playAudio = true; 
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			activateScene = true; 
		}

		if (activateScene) {
			if (playAudio) {
				audio.PlayOneShot (winner);
				playAudio = false; 
			}

		}

		if (gotTrophy) {
			exitTimer += 1 * Time.deltaTime;
			if (exitTimer >= 4f) {
				exit = true; 
			}
		}

	}
}
