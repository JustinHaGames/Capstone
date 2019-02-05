using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingToAwards : MonoBehaviour
{

    bool left;
    bool right;

    SpriteRenderer sprite;

    public float speed;

    public GameObject player;

    bool inactive;

    bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);

        if (!inactive)
        {
            if (left)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                sprite.flipX = true;
            }

            if (right)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                sprite.flipX = false;
            }
        }

        if (transform.position.x <= -7.45f)
        {
            transform.position = new Vector3(-7.45f, transform.position.y, transform.position.z);
        }

        if (transform.position.x >= 12f)
        {
            if (!spawned)
            {
                inactive = true;
                Instantiate(player, new Vector3(transform.position.x + 4f, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                spawned = true;
            }
        }

    }
}
