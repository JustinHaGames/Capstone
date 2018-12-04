using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceFemale : MonoBehaviour {

	SpriteRenderer sprite;

	public Sprite femaleAudience;

	float xPos = 0;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		xPos = Random.Range (0f, 2f);
	}

	// Update is called once per frame
	void Update () {
		if (InspirationManager.moveCrowd) {
			sprite.sprite = femaleAudience;
			if (transform.position.x < xPos) {
				transform.Translate (Vector3.right * 5f * Time.deltaTime);
			}
		}
	}
}