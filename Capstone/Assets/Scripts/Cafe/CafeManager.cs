using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeManager : MonoBehaviour {

	public Text cafeTalk;

	int dialogueCount;

	public Color player;
	public Color winner;

	bool changeScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Z)) {
			dialogueCount++;
		}

		Debug.Log (dialogueCount);

		switch (dialogueCount) {
		case 0:
			cafeTalk.text = "";
			break;
		case 1: 
			cafeTalk.text = "Hey, thanks for coming.";
			cafeTalk.color = winner;	
			break;
		case 2: 
			cafeTalk.text = "No worries. This is a big award so I was curious to see what you did.";
			cafeTalk.color = player;	
			break;
		case 3:
			cafeTalk.text = "Ah. It's whatever. Nothing too far from what we did at school.";
			cafeTalk.color = winner;
			break;
		case 4: 
			cafeTalk.text = "You say that but I don't believe you.";
			cafeTalk.color = player;	
			break;
		case 5:
			cafeTalk.text = "Hey, how's the new internship?";
			cafeTalk.color = winner;
			break;
		case 6: 
			cafeTalk.text = "Start tomorrow. Pretty excited.";
			cafeTalk.color = player;
			break;
		case 7:
			cafeTalk.text = "Hope it goes well. It's almost time for the ceremony.";
			cafeTalk.color = winner;
			break;
		case 8:
			cafeTalk.text = "I'm pretty nervous.";
			cafeTalk.color = winner;
			break;
		case 9:
			cafeTalk.text = "Why? This is pretty much the start of our lives. I think being nominated for a Monarch award is a pretty good start.";
			cafeTalk.color = player;
			break;
		case 10:
			cafeTalk.text = "DON'T BE AFRAID OF FAILURE!";
			cafeTalk.color = player;
			break;
		case 11: 
			cafeTalk.text = "Jesus! Alright...";
			cafeTalk.color = winner;
			break;
		case 12:
			cafeTalk.text = "You ready to go?";
			cafeTalk.color = player;
			break;
		case 13:
			cafeTalk.text = "Yeah...";
			cafeTalk.color = winner;
			break;
		case 14:
				GameManager.instance.switchScene = true;
				GameManager.instance.fadeIn = false;
			break;
	}

}
}
