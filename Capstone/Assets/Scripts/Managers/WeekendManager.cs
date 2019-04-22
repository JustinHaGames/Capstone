using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekendManager : MonoBehaviour
{

    public GameObject casualPlayer;
    public GameObject casualWinner;

    bool winnerSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (casualPlayer.transform.position.x >= 60.5f && !winnerSpawned)
        {
            Instantiate(casualWinner, new Vector3(50, -2.5f, casualPlayer.transform.position.z), Quaternion.identity);
            winnerSpawned = true;
        }
    }
}
