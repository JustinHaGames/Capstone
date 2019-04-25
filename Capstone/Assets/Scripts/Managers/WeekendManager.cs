using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekendManager : MonoBehaviour
{

    //Singleton
    public static WeekendManager instance;

    public GameObject casualPlayer;
    public GameObject casualWinner;

    public bool winnerSpawned;
    public bool winnerStopped;

    public bool poolDreamStarted;

    public GameObject playerPool;
    public bool spawnPlayerPool;

    public GameObject weekendWalls;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (casualPlayer.transform.position.x >= 60.5f)
        {
            StartCoroutine(spawnWinner());
        }

        if (spawnPlayerPool)
        {
            StartCoroutine(spawnPool());
        }
    }

    IEnumerator spawnWinner()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        if (!winnerSpawned)
        {
            Instantiate(casualWinner, new Vector3(50, -2.5f, casualPlayer.transform.position.z), Quaternion.identity);
            Instantiate(weekendWalls, new Vector3(54.5f, 24,-1), Quaternion.Euler(new Vector3(0,0,90)));
        }
        winnerSpawned = true;
    }

    IEnumerator spawnPool()
    {
        Instantiate(playerPool, new Vector3(59.95f, -6.85f, transform.position.z), Quaternion.identity);
        spawnPlayerPool = false;
        return null;
    }
}
