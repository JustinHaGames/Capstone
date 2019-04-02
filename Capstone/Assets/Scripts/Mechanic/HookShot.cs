using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    GameObject player;

    Vector3 vel;

    float dist;
    // Vector2 angle;

    public float speed;
    public float length;

    bool retract;
    bool stop;

    LineRenderer hookLine;

    float pullSpeed;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (xInput == 0 && yInput == 0)
        {
            vel = (transform.position - player.transform.position).normalized;
        }
        else
        {
            vel = new Vector2(xInput, yInput);
        }

        sprite = GetComponent<SpriteRenderer>();

        if (player.transform.position.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            sprite.flipX = false;
        }

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
        hookLine.SetPosition(0, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1));
        hookLine.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1));

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

        if (coll.gameObject.tag == "Enemy")
        {
            stop = true;
            player.GetComponent<PlayerMovement>().pull = true;
        }

        if (coll.gameObject.tag == "BoxTop" || coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Box")
        {
            retract = true;
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}