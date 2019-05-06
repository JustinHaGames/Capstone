using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovement : MonoBehaviour
{
    Vector2 vel;

    Rigidbody2D rb;
    BoxCollider2D box;
    SpriteRenderer sprite;

    Animator anim;

    bool grounded;

    public float accel;
    public float maxAccel;

    public float gravity;

    bool canJump;
    bool jump;
    public float jumpvel;

    bool right;
    bool left;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //right = true;

        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        canJump = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Grounded();

        timer += 1 * Time.deltaTime;

        if (timer <= 1.5f)
        {
            right = true;
            left = false;
        }

        if (timer > 1.5f && timer <= 3)
        {
            left = true;
            right = false;
        }

        if (timer > 3)
        {
            right = true;
            timer = 0;

        }

        if (right)
        {
            vel.x += accel;
            sprite.flipX = false;
            if (grounded)
            {
                anim.Play("RunningAnimation");
            } else
            {
                anim.Play("Jump");
            }
        }

        if (left)
        {
            vel.x -= accel;
            sprite.flipX = true;

            if (grounded)
            {
                anim.Play("RunningAnimation");
            } else
            {
                anim.Play("Jump");
            }
        }

        if ((timer >= .6f && timer < .7f) || timer >= 2f && timer < 2.1f)
        {
            if (canJump)
            {
                vel.y = jumpvel;
                canJump = false;
            }
        } else
        {
            canJump = true;
        }

            //Limit the player's max velocity
            vel.x = Mathf.Max(Mathf.Min(vel.x, maxAccel), -maxAccel);

        rb.MovePosition((Vector2)transform.position + vel * Time.deltaTime);

    }

    void Grounded()
    {
        Vector2 pt1 = transform.TransformPoint(box.offset + new Vector2(box.size.x / 2, -box.size.y / 2));
        Vector2 pt2 = transform.TransformPoint(box.offset - (box.size / 2) + new Vector2(0, 0));

        grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null || Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("PlayerTop")) != null;

        if (grounded)
        {
            vel.y = 0;
        }
        else
        {
            vel.y += gravity;
        }
    }

    IEnumerator activateJump()
    {
        vel.y = jumpvel;

        return null;
    }

}
