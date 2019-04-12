using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject box;

    GameObject player;

    public float speed;

    public float direction;

    Vector3 vel;

    bool latch;

    float randomPos;

    GameObject specialSpawn;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        randomPos = Random.Range(-.5f, .5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        vel.x = direction * speed;

        if (GameManager.instance.monarchCaught == true || transform.position.x <= -20f || transform.position.x >= 20f)
        {
            Destroy(gameObject);
        }

        if (latch)
        {
            rb.isKinematic = true;
            gameObject.layer = 23;
            gameObject.tag = "Latch";
            transform.position = new Vector3(player.transform.position.x + randomPos, player.transform.position.y + randomPos, player.transform.position.z + randomPos);
        }

        if (!latch)
        {
            rb.MovePosition(transform.position + vel * Time.deltaTime);
        }
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

    public void Latch()
    {
        latch = true;
    }

    public void BrokeFree()
    {
        specialSpawn = Instantiate(box, transform.position, Quaternion.identity);
        specialSpawn.SendMessage("PopUp", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}