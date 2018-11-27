using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnCollisionEnter2D(Collision2D coll){
//		if (coll.gameObject.tag == "Box") {
//			GameManager.instance.targetHit += 1; 
//			Destroy (gameObject);
//		}
//	}

	public void Destroy(){GameManager.instance.targetHit += 1; 
		Destroy (gameObject);
	}

}
