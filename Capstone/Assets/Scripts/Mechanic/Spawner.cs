using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject Enemy;

    public GameObject horizontalEnemy;

    //Randomly spawn chasing enemy
	float timer;

	float randomNum;

	bool chosen;

    //Randomly spawn horizontal enemy
    float horizontalTimer;

    float horizontaRandomNum;

    bool horizontalChosen;

	// Use this for initialization
	void Start () {
		chosen = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.sceneName == "Dream2" || GameManager.instance.sceneName == "Dream3" || GameManager.instance.sceneName == "Dream4")
        {
            if (GameManager.instance.specialBoxKilled)
            {
                timer += 1 * Time.deltaTime;
                horizontalTimer += 1 * Time.deltaTime;
            }

            if (!chosen)
            {
                randomNum = Random.Range(5, 10);
                chosen = true;
            }

            if (!horizontalChosen)
            {
                horizontaRandomNum = Random.Range(10, 20);
                horizontalChosen = true;
            }

            if (timer >= randomNum)
            {
                Instantiate(Enemy, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                chosen = false;
                timer = 0;
            }

            if (horizontalTimer >= horizontaRandomNum)
            {
                Instantiate(horizontalEnemy, new Vector3(transform.position.x, transform.position.y - 2f, 1), Quaternion.identity);
                horizontalChosen = false;
                horizontalTimer = 0;
            }
        }

        if (GameManager.instance.sceneName == "BoxCloset")
        {

            if (GameManager.instance.currentSpot >= 3)
            {
                timer += 1 * Time.deltaTime;
                horizontalTimer += 1 * Time.deltaTime;
            }

            if (!chosen)
            {
                randomNum = Random.Range(5, 10);
                chosen = true;
            }

            if (!horizontalChosen)
            {
                horizontaRandomNum = Random.Range(10, 20);
                horizontalChosen = true;
            }

            if (timer >= randomNum)
            {
                Instantiate(Enemy, new Vector3(transform.position.x, transform.position.y, -7f), Quaternion.identity);
                chosen = false;
                timer = 0;
            }

            if (horizontalTimer >= horizontaRandomNum)
            {
                Instantiate(horizontalEnemy, new Vector3(transform.position.x, transform.position.y - 3f, -7f), Quaternion.identity);
                horizontalChosen = false;
                horizontalTimer = 0;
            }

        }

        if (GameManager.instance.sceneName == "TapeMeasure")
        {
            if (GameManager.instance.currentSpot >= 3)
            {
                horizontalTimer += 1 * Time.deltaTime;
            }

            if (!horizontalChosen)
            {
                horizontaRandomNum = Random.Range(5, 15);
                horizontalChosen = true;
            }

            if (horizontalTimer >= horizontaRandomNum)
            {
                Instantiate(horizontalEnemy, new Vector3(transform.position.x, transform.position.y - 2f, -7f), Quaternion.identity);
                horizontalChosen = false;
                horizontalTimer = 0;
            }
        }
    }
}
