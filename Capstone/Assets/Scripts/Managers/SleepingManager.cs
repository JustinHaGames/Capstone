using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingManager : MonoBehaviour
{

    public static SleepingManager instance;

    public bool closeEyes;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        closeEyes = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
