using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMale : MonoBehaviour {

	SpriteRenderer sprite;

	public Sprite maleAudience;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (InspirationManager.moveCrowd) {
			sprite.sprite = maleAudience;
		}
	}
}
