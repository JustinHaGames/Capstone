﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspirationManager : MonoBehaviour {

	private AudioSource audio; 

	public AudioClip winner; 

	public AudioClip clap; 

	public static bool gotTrophy; 

	public static bool activateScene; 

	public static bool standUp; 

	bool playAudio;

	bool clapped; 

	public static bool moveCrowd;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> (); 
		playAudio = true; 
		clapped = false; 
	}

    private void Update()
    {
        if (GameManager.instance.paused)
        {
            if (audio.isPlaying)
                audio.Pause();

        }
        else
        {
            if (!audio.isPlaying)
                audio.UnPause();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {

		if (Input.GetButtonDown("Fire1")) {
			activateScene = true; 
		}

		if (activateScene) {
			if (playAudio) {
				audio.PlayOneShot (winner);
				playAudio = false; 
			}

		}

		if (gotTrophy) { 
			if (!clapped) {
				audio.PlayOneShot (clap);
				clapped = true; 
			}
		}

	}
}
