using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspirationPlayerMovement : MonoBehaviour
{
    Vector2 vel;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    Animator anim;

    public Sprite RealityPlayer;

    public float accel;
    public float maxAccel;

    bool lastL;
    bool lastR;

    bool inactive;

    bool transformed;

    public GameObject blueParticles;
    public GameObject yellowParticles;

    bool leave;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        lastL = false;
        lastR = true;

        if (GameManager.instance.sceneName == "Inspiration")
        {
            inactive = true;
            StartCoroutine(Transformation());
        }



    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameManager.instance.sceneName == "GoingToSleep")
        {
            inactive = false;
            leave = true;
            transformed = true;
        }

        float xInput = Input.GetAxis("Horizontal");

        //Move Left
        if (leave)
        {
            if (!inactive)
            {
                if (transformed)
                {
                    anim.Play("RealityPlayerWalk");
                }
            }

            vel.x -= accel;
            if (!inactive)
            {
                sprite.flipX = true;
            }
            lastL = true;
            lastR = false;
        }

        //Limit the player's max velocity
        vel.x = Mathf.Max(Mathf.Min(vel.x, maxAccel), -maxAccel);

        if (transformed && !leave)
        {
            anim.Play("RealityPlayerIdle");
        }


        if (!inactive)
        {
            rb.MovePosition((Vector2)transform.position + vel * Time.deltaTime);
        }

        if (transform.position.x <= -10f && GameManager.instance.sceneName == "Inspiration")
        {
            GameManager.instance.switchScene = true;
            GameManager.instance.fadeIn = false;
        }

        if (transform.position.x <= -6.5f &&  GameManager.instance.sceneName == "GoingToSleep")
        {
            Destroy(gameObject);
        }

    }

    IEnumerator Transformation()
    {
        for (int i = 0; i < 300; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        sprite.sprite = RealityPlayer;
        anim.enabled = true;
        transformed = true;
        Instantiate(blueParticles, transform.position, Quaternion.identity);
        Instantiate(yellowParticles, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        inactive = false;
        leave = true;
    }

}