using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    bool rising;

    bool stop;

    public float poolSpeed;
    // Use this for initialization
    void Start()
    {
        rising = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rising && !stop)
        {
            transform.Translate((Vector3.up * Time.deltaTime) * poolSpeed);
        }

        if (transform.position.y >= 54.285f)
        {
            stop = true;
        }

    }
}
