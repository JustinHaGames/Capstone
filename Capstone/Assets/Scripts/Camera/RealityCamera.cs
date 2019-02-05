using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityCamera : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            GameObject dreamPlayer = GameObject.FindWithTag("Player");
            transform.position = new Vector3(dreamPlayer.transform.position.x, dreamPlayer.transform.position.y, transform.position.z);
        }
    }
}
