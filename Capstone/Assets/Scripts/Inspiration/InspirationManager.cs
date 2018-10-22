using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspirationManager : MonoBehaviour {

	AudioSource audio; 

	public AudioClip winner; 

	public AudioClip clap; 

	public static bool gotTrophy; 

	public static bool activateScene; 

	public static bool standUp; 

	float clapTimer; 

	bool playAudio;

	bool clapped; 



	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> (); 
		playAudio = true; 
		clapped = false; 
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
			clapTimer += 1 * Time.deltaTime;
			if (clapTimer >= 1f) {
				standUp = true; 
			}
			if (!clapped) {
				audio.PlayOneShot (clap);
				clapped = true; 
			}
		}

	}
}
