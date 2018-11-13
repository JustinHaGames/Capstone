using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.monarchFlying) {
			transform.Translate ((Vector3.down * Time.deltaTime) * .25f);
		}
	}
}
