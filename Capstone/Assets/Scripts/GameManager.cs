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

	public bool monarchComeAlive; 
	public bool monarchFlying; 

	public int sceneID;
    public string sceneName;

	AudioSource audio; 

	public AudioClip flying; 

	public bool playFlying; 

	//only pertains to scene 4

	public bool playerImagining;

	public GameObject barrier;
	bool barrierMade;

	public GameObject greyBox;

	public GameObject target;

	public int targetHit;

	float targetXPos;
	float targetYPos;

	//Only pertains to scene 5

	public bool specialBoxKilled;

	public bool playerFallen;

    public bool dreamStarted;

    public bool hookshotUnlocked;
    public bool wallJumpUnlocked;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		alphaNum = 1f;
		fadeIn = true;
		instance = this; 

		audio = GetComponent<AudioSource> ();

        //Get the current scene you are on
        sceneID = SceneManager.GetActiveScene().buildIndex;
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Dream3")
        {
            hookshotUnlocked = true;
        }

        if (sceneName == "Dream4")
        {
            hookshotUnlocked = true; 
            wallJumpUnlocked = true;
        }

    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (sceneID);
		}

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

		//Only things done in scene 3
		if (sceneName == "Dream1") {
			if (playFlying) {
				if (!audio.isPlaying) {
					audio.Play ();
				}
			}
		}

		if (sceneName == "Dream2" || sceneName == "Dream3" || sceneName == "Dream4") {

            if (specialBoxKilled) {
				monarchFlying = true;
				playFlying = true;
				if (playFlying) {
					if (!audio.isPlaying) {
						audio.Play ();
					}
				}
			}

		}
			
		//Only things done in scene 4
		if (sceneName == "BoxCloset") {

			switch (targetHit) {
			case 0: 
				targetXPos = -6.5f; 
				targetYPos = -3.5f; 
				break; 
			case 1: 
				targetXPos = -4f; 
				targetYPos = -3.5f;
				break; 
			case 2: 
				targetXPos = -6.5f; 
				targetYPos = -1.5f;
				break;
			case 3:
				targetXPos = -4f;
				targetYPos = -1.5f;
				break; 
			case 4: 
				targetXPos = -6.5f; 
				targetYPos = .25f;
				break;
			case 5:
				targetXPos = -4f; 
				targetYPos = .25f; 
				break; 
			case 6: 
				targetXPos = -6.5f; 
				targetYPos = 2f; 
				break; 
			case 7:
				targetXPos = -4f; 
				targetYPos = 2f; 
				break;
			}

			if (playerImagining) {

				if (!barrierMade) {
					Instantiate (barrier, new Vector3 (0, -6.93f, 0), transform.rotation * Quaternion.Euler (0f, 0f, 90f));
					barrierMade = true;
				}

				if (GameObject.FindGameObjectWithTag ("Target") == null) {
					if (targetHit <= 7) {
						Instantiate (target, new Vector3 (targetXPos, targetYPos, -1f), Quaternion.identity);
					}
				}
			}


			if (GameObject.FindGameObjectsWithTag ("Box").Length < 8) {
				if (targetHit <= 7) {
					Instantiate (greyBox, new Vector3 (6.5f, 1f, -1f), Quaternion.identity);
				} 
			}
		}
	}

	//After a short delay, change to a different scene
	IEnumerator SceneChange(){
		for (int i = 0; i < 100; i++) {
			yield return new WaitForFixedUpdate();
		}
		SceneManager.LoadScene (sceneID += 1);
	}
}
