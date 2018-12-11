using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamRotate : MonoBehaviour {

	public GameObject monarch;

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (monarch.transform.position, new Vector3(0,0,1f), speed * Time.deltaTime);
	}
}
