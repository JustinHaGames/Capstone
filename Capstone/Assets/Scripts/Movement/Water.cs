using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	bool rising;

    public float waterSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//In Dream 1, water shouldn't rise until the monarch is flying
		if (GameManager.instance.sceneName == "Dream1") {
			if (GameManager.instance.monarchFlying) {
				rising = true;
			}
		}

		//In Dream2, water shouldn't rise until the box is picked up
		if (GameManager.instance.sceneName == "Dream2" || GameManager.instance.sceneName == "Dream3" || GameManager.instance.sceneName == "Dream4") {
			if (GameManager.instance.specialBoxKilled) {
				rising = true; 
			}
		}

        //Training scene
        if (GameManager.instance.sceneName == "DreamTest")
        {
            if (GameManager.instance.specialBoxKilled)
            {
                rising = true;
            }
        }


        if (rising) {
			transform.Translate ((Vector3.down * Time.deltaTime) * -waterSpeed);
		}
	}
}
