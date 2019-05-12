using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBlock : MonoBehaviour
{

    public float movespeed;
    public float maxDist;
    float dist;

    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = Mathf.Abs(Vector3.Distance(originalPos, transform.position));


        if (GameManager.instance.sceneName == "GoingToSleep" || GameManager.instance.sceneName == "GoingToSleep 1" || GameManager.instance.sceneName == "GoingToSleep 2" || GameManager.instance.sceneName == "GoingToSleep 3")
        {
            if (dist < maxDist && SleepingManager.instance.closeEyes)
            {
                transform.Translate(Vector3.up * movespeed * Time.deltaTime);
            }
            else if (dist >= maxDist && SleepingManager.instance.closeEyes)
            {
                GameManager.instance.switchScene = true;
            }
        }
    }
}
