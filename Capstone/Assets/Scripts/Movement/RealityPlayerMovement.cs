﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityPlayerMovement : MonoBehaviour
{

    Vector2 vel;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    Animator anim;

    public Sprite colorRealityPlayer;

    public float accel;
    public float maxAccel;

    public GameObject dreamPlayer;
    public GameObject monarch;

    bool holdingBox;

    bool lastL;
    bool lastR;

    public LayerMask blockMask;

    public GameObject heldObject;

    bool spawnedDreamPlayer;

    bool inactive;

    bool monarchSpawned;

    bool waitingCasualWinner;
    bool watchingCasualWinner;
    bool casualWinnerGone;

    GameObject casualWinner;

    public GameObject buttonA;
    public GameObject buttonX;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (GameManager.instance.sceneName != "Weekend")
        {
            sprite.flipX = true;
        }

        lastL = false;
        lastR = true;

    }

    private void Update()
    {

        if (GameManager.instance.sceneName == "Inspiration")
        {
            sprite.sprite = colorRealityPlayer;
        }

        if (GameManager.instance.sceneName != "Inspiration" && GameManager.instance.sceneName != "Weekend")
        {
            if (!GameManager.instance.taskRead)
            {
                inactive = true;
            }
            else if (GameManager.instance.taskRead && !GameManager.instance.dreamStarted)
            {
                inactive = false;
            }

        }

        if (GameManager.instance.sceneName == "Weekend")
        {
            if (!GameManager.instance.taskRead)
            {
                inactive = true;
            } else if (GameManager.instance.taskRead && !waitingCasualWinner && !watchingCasualWinner && !casualWinnerGone)
            {
                inactive = false;
            }
        }

        //Start of tutorial button code
        if (GameManager.instance.sceneName == "BoxCloset" && !GameManager.instance.dreamStarted)
        {
            if (transform.position.x >= 4.8f || transform.position.x <= -3.2f && heldObject != null)
            {

            }
        }

        //Pickup objects
        if (Input.GetButtonDown("Fire1") && !inactive)
        {
            //Box kicking test
            if (heldObject == null && GameManager.instance.sceneName == "BoxCloset")
            {
                if (lastR)
                {
                    Vector3 facingDirection = Vector3.right;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + (-facingDirection * sprite.bounds.extents.x * .5f) + (Vector3.down * sprite.bounds.extents.y * 0.7f), facingDirection, 1.5f, blockMask);
                    heldObject = hit.collider.gameObject;
                    heldObject.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
                }
                else if (lastL)
                {
                    Vector3 facingDirection = Vector3.left;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + (-facingDirection * sprite.bounds.extents.x) + (Vector3.down * sprite.bounds.extents.y * 0.7f), facingDirection, 1.5f, blockMask);
                    heldObject = hit.collider.gameObject;
                    heldObject.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        if (heldObject != null)
        {
            holdingBox = true;
        }
        else
        {
            holdingBox = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float xInput = Input.GetAxis("Horizontal");

        //Move Left
        if (xInput < 0 && !inactive)
        {
            if (!holdingBox)
            {
                if (GameManager.instance.sceneName != "Weekend")
                {
                    anim.Play("RealityPlayerWalk");
                }
                else if ( GameManager.instance.sceneName == "Weekend")
                {
                    anim.Play("CasualWalk");
                }
            }
            else
            {
                if (GameManager.instance.sceneName != "Weekend")
                {
                    anim.Play("RealityPlayerBoxWalk");
                }
            }
            vel.x -= accel;
            sprite.flipX = true;
            lastL = true;
            lastR = false;
        }

        //Move Right
        if (xInput > 0 && !inactive)
        {
            if (!holdingBox)
            {
                if (GameManager.instance.sceneName != "Weekend")
                {
                    anim.Play("RealityPlayerWalk");
                }
                else if (GameManager.instance.sceneName == "Weekend")
                {
                    anim.Play("CasualWalk");
                }
            }
            else
            {
                if (GameManager.instance.sceneName != "Weekend")
                {
                    anim.Play("RealityPlayerBoxWalk");
                }
            }
            vel.x += accel;
            sprite.flipX = false;
            lastL = false;
            lastR = true;
        }

        //Limit the player's max velocity
        vel.x = Mathf.Max(Mathf.Min(vel.x, maxAccel), -maxAccel);

        //If you don't move right or left, then don't move
        if (Mathf.Abs(xInput) <= 0f && !inactive)
        {
            if (!holdingBox && GameManager.instance.sceneName != "Weekend")
            {
                anim.Play("RealityPlayerIdle");
            }
            else if (holdingBox && GameManager.instance.sceneName != "Weekend")
            {
                anim.Play("RealityPlayerBoxIdle");
            }
            else if (GameManager.instance.sceneName == "Weekend")
            {
                anim.Play("CasualIdle");
            }
            vel.x = 0;
        }

        //When day dream starts, spawn a dream player and the monarch
        if (!spawnedDreamPlayer && GameManager.instance.dreamStarted == true)
        {
            StartCoroutine(DayDream());
            spawnedDreamPlayer = true;
            inactive = true;
        }

        if (!monarchSpawned && GameManager.instance.sceneName == "Cobweb")
        {
            if (GameObject.FindWithTag("SpecialCobweb") == null)
            {
                Instantiate(monarch, new Vector3(0, 4f, -7f), Quaternion.identity);
                monarchSpawned = true;
            }
        }

        if (GameManager.instance.sceneName == "Weekend")
        {
            if (transform.position.x >= 55f && transform.position.x < 60.5f)
            {
                anim.Play("CasualWalk");
                waitingCasualWinner = true;
                inactive = true;
                vel.x += accel;
            }

            if (transform.position.x >= 60.5f)
            {
                vel.x = 0;
                waitingCasualWinner = false;
                watchingCasualWinner = true;
                anim.Play("CasualIdle");
            }

            if (watchingCasualWinner)
            {
                casualWinner = GameObject.FindWithTag("CasualWinner");
                if (casualWinner.transform.position.x < transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                if (casualWinner.transform.position.x >= 71f && !spawnedDreamPlayer)
                {
                    watchingCasualWinner = false;
                    casualWinnerGone = true;
                    //StartCoroutine(DayDream());
                    //spawnedDreamPlayer = true;
                    inactive = true;
                }
            }

        }

        rb.MovePosition((Vector2)transform.position + vel * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Shelf")
        {
            if (holdingBox && Input.GetButtonDown("Fire1"))
            {
                GameManager.instance.boxPlaced = true;
                heldObject.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
                holdingBox = false;
            }
        }

        if (coll.gameObject.tag == "Cobweb")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.instance.currentSpot += 1;
                Destroy(coll.gameObject);
            }
        }
    }

    IEnumerator DayDream()
    {
        for (int i = 0; i < 125; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        if (GameManager.instance.sceneName != "Weekend")
        {
            Instantiate(dreamPlayer, new Vector3(0, 0, -7f), Quaternion.identity);
            if (GameManager.instance.sceneName != "Cobweb")
            {
                Instantiate(monarch, new Vector3(0, 6f, -7f), Quaternion.identity);
            }
        }

        if (GameManager.instance.sceneName == "Weekend")
        {
            Instantiate(dreamPlayer, transform.position, Quaternion.identity);
        }
    }
}