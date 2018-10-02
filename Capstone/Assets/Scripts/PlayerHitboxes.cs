using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxes : MonoBehaviour {

	public GameObject hitboxRight;
	public GameObject hitboxLeft; 

	bool canAttack; 
	// Use this for initialization
	void Start () {
		canAttack = false; 
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift)) || (Input.GetKeyDown(KeyCode.RightArrow))  && canAttack == true) {
			hitboxRight.SetActive (true);
			canAttack = false;
			StartCoroutine (HitboxActive ());
		}

		if ((Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift)) || (Input.GetKeyDown(KeyCode.LeftArrow)) && canAttack == true) {
			hitboxLeft.SetActive (true);
			canAttack = false;
			StartCoroutine (HitboxActive ());
		}
	}

	IEnumerator HitboxActive(){
		for (int i = 0; i < 8; i++) {
			yield return new WaitForFixedUpdate();
		}
		hitboxRight.SetActive (false);
		hitboxLeft.SetActive (false);
		canAttack = true;
	}
}