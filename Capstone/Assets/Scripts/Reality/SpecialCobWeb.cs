using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCobWeb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.dreamStarted)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -6.5f);
        }
    }
}
