using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekendCamera : MonoBehaviour
{
    GameObject realPlayer;

    GameObject dreamPlayer;

    bool dreamMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dreamMode)
        {
            realPlayer = GameObject.FindWithTag("RealityPlayer");

            transform.position = new Vector3(realPlayer.transform.position.x, transform.position.y, transform.position.z);

            if (transform.position.x <= 0)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            } else if (transform.position.x >= 60.5f)
            {
                dreamMode = true;
                transform.position = new Vector3(60.5f, transform.position.y, transform.position.z);
            }
        } 

        if (dreamMode)
        {
            realPlayer = GameObject.FindWithTag("RealityPlayer");

            transform.position = new Vector3(60.5f, realPlayer.transform.position.y, transform.position.z);

            if (transform.position.y <= 0)
            {
                transform.position = new Vector3(60.5f, 0, transform.position.z);
            } else
            {
                transform.position = new Vector3(60.5f, realPlayer.transform.position.y, transform.position.z);
            }
        }

    }
}
