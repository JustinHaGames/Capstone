using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Rigidbody2D rb;
	public GameObject box;

	GameObject player;

	public float speed;

	SpriteRenderer sprite;

	Vector3 vel;
	Vector3 dir;

    bool flipVelY;

    BoxCollider2D hitBox;

    bool latch;

    float randomPos;

    GameObject specialSpawn;

    bool spawnedBox;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		dir = (player.transform.position - transform.position).normalized;

        hitBox = GetComponent<BoxCollider2D>();

        randomPos = Random.Range(-.5f, .5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (this.gameObject.tag == "Enemy")
        {
            if (player != null)
            {
                //Flip the sprite so the enemy is always looking at the player
                if (transform.position.x > player.transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
        }

        if (GameManager.instance.monarchCaught == true || transform.position.x <= -20f || transform.position.x >= 20f)
        {
            Destroy(gameObject);
        }

        vel = dir * speed;

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
		
	void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "BoxPusher" || coll.gameObject.tag == "BoxTop" || coll.gameObject.tag == "Box")
        {
           StartCoroutine("Dead");
        }

        if (coll.gameObject.tag == "Floor")
        {
            dir.y *= -1f;
        }

    }

	IEnumerator Dead (){
        if (!spawnedBox)
        {
            Instantiate(box, transform.position, Quaternion.identity);
            spawnedBox = true;
            Destroy(gameObject);
        }
        yield return null;
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
