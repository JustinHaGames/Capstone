using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		transform.Translate ((Vector3.down * Time.deltaTime) * .25f);
	}
}
