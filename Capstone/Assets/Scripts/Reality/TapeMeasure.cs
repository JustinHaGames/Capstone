using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeMeasure : MonoBehaviour
{

    SpriteRenderer sprite;

    float dist;

    GameObject realityPlayer;

    bool retract;

    public float speed;

    Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < 0)
        {
            sprite.flipX = true;
            //Quaternion.EulerRotation(0, 0, -90f);
        }

        realityPlayer = GameObject.FindWithTag("RealityPlayer");

        dist = Vector3.Distance(transform.position, realityPlayer.transform.position);

        //Every time the tape measure is retraced, increase how far you can retract it next time
        if (GameManager.instance.retractLimit <= dist)
        {
            StartCoroutine(Retract());  
        }

        //Move the tape measure when retracted
        if (retract)
        {
            vel = (transform.position - realityPlayer.transform.position).normalized;
            transform.position -= vel * speed;
        }

        //Remove the tapemeasure when retracted
        if (dist <= .5f && retract)
        {
            retract = false;
            Destroy(gameObject);
        }

        if (GameManager.instance.monarchCaught)
        {
            retract = false;
        }

    }

    IEnumerator Retract()
    {
        GameManager.instance.retractLimit += 3;
        retract = true;
        GameManager.instance.currentSpot += 1;
        yield return new WaitForFixedUpdate();
    }

}
