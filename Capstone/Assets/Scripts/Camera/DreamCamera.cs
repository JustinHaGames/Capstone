using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamCamera : MonoBehaviour {

	public Transform player; 

	public float yTopDif;

	public float yBottomDif; 

	float timer;

	bool moving; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.instance.sceneName == "Dream1") {

			if (!GameManager.instance.playerFallen) {
				transform.position = new Vector3 (player.position.x, transform.position.y, transform.position.z);

				if (transform.position.x <= -81.2f) {
					transform.position = new Vector3 (-81.2f, transform.position.y, transform.position.z);
				} 
			} else {

				transform.position = new Vector3 (0, player.position.y, transform.position.z);

				if (transform.position.y <= -0.4f) {
					transform.position = new Vector3 (0, -0.4f, transform.position.z);
				}
			}
		}

		if (GameManager.instance.sceneName == "Dream2" || GameManager.instance.sceneName == "Dream3" || GameManager.instance.sceneName == "Dream4") {


            Vector3 movePosition = new Vector3 (0, transform.position.y, transform.position.z);

			if (player.transform.position.y >= transform.position.y + yTopDif) {
				moving = true;
				movePosition.y = Mathf.Lerp (transform.position.y, player.position.y, timer);
			} else if (player.transform.position.y <= transform.position.y - yBottomDif) {
				moving = true;
				movePosition.y = Mathf.Lerp (transform.position.y, player.position.y, timer);
			} else {
				moving = false; 
			}

			if (moving) {
				timer += Time.deltaTime;
			} else {
				timer = 0;
			}

            //transform.position = movePosition;
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);

            if (transform.position.y <= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
	}
}
