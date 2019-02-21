using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    GameObject player;

    Vector3 vel;

    float dist;

    public float speed;
    public float length;

    bool retract;
    bool stop;

    LineRenderer hookLine;

    float pullSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        vel = (transform.position - player.transform.position).normalized;

        hookLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= length && !retract && !stop)
        {
            transform.position += vel * speed;
        }
        else
        {
            if (!stop)
            {
                retract = true;
            }
        }

        if (retract)
        {
            vel = (transform.position - player.transform.position).normalized;
            transform.position -= vel * speed;
        }

        //Draws the hookshot line
        hookLine.SetPosition(0, player.transform.position);
        hookLine.SetPosition(1, transform.position);

        if (dist <= .5f && retract)
        {
            player.SendMessage("Retracted", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            stop = true;
            player.GetComponent<PlayerMovement>().pull = true;

        }

        if (coll.gameObject.tag == "BoxPusher")
        {
            Destroy(gameObject);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}