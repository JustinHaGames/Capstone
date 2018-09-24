using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

	public Renderer rend; 

	// Use this for initialization
	void Start () {
		//get the sprite renderer and then set the transparency alpha to 0 (transparent)
		rend = GetComponent<Renderer>();
		rend.material.color = new Color (0f, 0f, 0f, 0f);
	}

	// Update is called once per frame
	void Update () {
		//Update the transparenct of the fade depending on the player movement
		rend.material.color = new Color (0f, 0f, 0f, GameManager.instance.alphaNum);
	}
}
