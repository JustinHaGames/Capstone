using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchMovement : MonoBehaviour {

	bool ascend; 

	float timer; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		if (GameManager.instance.monarchComeAlive) {
			
			ascend = true; 


			if (timer < 7f) {
				transform.Translate (Vector3.up * Time.deltaTime);

			}else{
				ascend = false; 
				GameManager.instance.monarchFlying = true; 
				GameManager.instance.monarchComeAlive = false; 
			}

		}

		if (GameManager.instance.monarchFlying) {
			transform.position = new Vector3(Mathf.PingPong (Time.time * 2f, 17f) +72f, transform.position.y + (Mathf.Sin (Time.time) * .03f), transform.position.z);
			if (timer >= 40f) {
				GameManager.instance.switchScene = true;
				GameManager.instance.fadeIn = false; 
			}
		}
	}
}
