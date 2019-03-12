using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchMovement : MonoBehaviour {

	float timer; 

	bool right; 
	bool left; 

	public float horizontalVel;

	GameObject player;
    GameObject boxPusher;

	bool flyingRight;

    public Light monarchLight;
    public Color farLightColor;
    public Color closeLightColor;

    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		right = true; 

		player = GameObject.FindGameObjectWithTag ("Player");

        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!GameManager.instance.playerFallen) {
			timer += Time.deltaTime;

			if (GameManager.instance.monarchComeAlive) {

				GameManager.instance.monarchFlying = true; 

				if (timer < 6.5f) {
					transform.Translate (Vector3.up * Time.deltaTime);

				} else {
					GameManager.instance.monarchFlying = false; 
					GameManager.instance.monarchComeAlive = false; 
					flyingRight = true; 
				}
				
			}

			if (flyingRight) {
				if (transform.position.x < 0) {
					transform.Translate (Vector2.right * Time.deltaTime * 5f);
				}
			}

		}

		if (GameManager.instance.playerFallen || GameManager.instance.sceneName == "Dream2" || GameManager.instance.sceneName == "Dream3" || GameManager.instance.sceneName == "Dream4"){


            if (GameManager.instance.monarchFlying) {
				if (transform.position.x <= -7f) {
					right = true; 
					left = false; 
				} else if (transform.position.x >= 7f) {
					left = true; 
					right = false; 
				}

				if (right) {
                    sprite.flipX = false;
					transform.Translate (Vector3.right * Time.deltaTime * 3f);
				} else if (left) {
                    sprite.flipX = true;
					transform.Translate (Vector3.left * Time.deltaTime * 3f);
				}

				player = GameObject.FindGameObjectWithTag ("Player");

                if (transform.position.y <= player.transform.position.y + 2f) {
                	transform.position = new Vector3 (transform.position.x, player.transform.position.y + 2f, transform.position.z);
                }

                boxPusher = GameObject.FindGameObjectWithTag("BoxPusher");

                if (transform.position.y <= boxPusher.transform.position.y + 2f)
                {
                    transform.position = new Vector3(transform.position.x, boxPusher.transform.position.y + 2f, transform.position.z);
                }

                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance <= 4f)
                {
                    Time.timeScale = .5f;
                    monarchLight.intensity += .25f;
                }
                else
                {
                    Time.timeScale = 1;
                    monarchLight.intensity -= .25f;
                }

                monarchLight.color = Color.Lerp(farLightColor, closeLightColor, .1f);

                if (monarchLight.intensity <= 8f)
                {
                    monarchLight.intensity = 8f;
                }

            }
		}
	}
}
