using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClosetCamera : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            transform.position = new Vector3(0, player.transform.position.y, -10);
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(0, 0, -10);
        }

        if (GameManager.instance.monarchCaught)
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
