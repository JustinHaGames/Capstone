using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceFemale : MonoBehaviour {

	SpriteRenderer sprite;

	public Sprite femaleAudience;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (InspirationManager.moveCrowd) {
			sprite.sprite = femaleAudience;
		}
	}
}