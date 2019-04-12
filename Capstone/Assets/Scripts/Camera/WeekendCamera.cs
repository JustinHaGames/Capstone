using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekendCamera : MonoBehaviour
{
    GameObject realPlayer;

    GameObject dreamPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.dreamStarted)
        {
            realPlayer = GameObject.FindWithTag("RealityPlayer");

            transform.position = new Vector3(realPlayer.transform.position.x, transform.position.y, transform.position.z);

            if (transform.position.x <= 0)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            } else if (transform.position.x >= 60.5f)
            {
                transform.position = new Vector3(60.5f, transform.position.y, transform.position.z);
            }

        }
    }
}
