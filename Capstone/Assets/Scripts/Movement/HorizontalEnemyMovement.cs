using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject box;

    GameObject player;

    public float speed;

    SpriteRenderer sprite;

    public float direction;

    Vector3 vel;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        vel.x = direction * speed;

        if (GameManager.instance.currentSpot >= 8 || transform.position.x <= -10f || transform.position.x >= 10f)
        {
            Destroy(gameObject);
        }

        rb.MovePosition(transform.position + vel * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "BoxPusher" || coll.gameObject.tag == "BoxTop" || coll.gameObject.tag == "Box")
        {
            Dead();
        }

    }

    public void Dead()
    {
        Instantiate(box, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}