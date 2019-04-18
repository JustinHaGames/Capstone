using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    public float maxGravity;
    bool canJump;
    bool jump;
    public float baseJumpVel;
    public float jumpVel;
    public float maxJumpVel;
    public float boxBoost;
    int jumpCounter;
    bool onWall;
    bool onWallLeft;
    bool onWallRight;
    public float wallFriction;
    float slideVel;

    bool lastR;
    bool lastL;

    public GameObject heldObject;

    //Swim variables
    float waterSpeed = 1;
    bool swim;

    public LayerMask blockMask;

    //Knockback variables
    bool inactive;

    //Walljump layermask
    public LayerMask wallMask;
    public float wallJumpX;
    public float wallJumpY;

    bool falling;
    public Material playerMat;
    public Material defaultMat;

    public GameObject hookShot;
    bool shot;
    public float pullSpeed;
    public bool pull;

    float wallJumpTimer;

    //Code for mashing out of latche boxes
    public List<GameObject> latchedBoxes = new List<GameObject>();

    public int mashLimit;
    int mashNumber;
    int numberMashed;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        lastR = true;
        lastL = false;

        heldObject = null;

    }

    void Update()
    {

        if (GameManager.instance.sceneName == "Dream1")
        {
            if (falling)
            {
                sprite.material = playerMat;
                GameManager.instance.playerFallen = true;
            }
            else
            {
                sprite.material = defaultMat;
            }
        }

        //Set the proper boosts for each scene

        if (GameManager.instance.sceneName == "Dream2" || GameManager.instance.sceneName == "Dream3" || GameManager.instance.sceneName == "Dream4")
        {
            boxBoost = 25f;
        }

        if (GameManager.instance.sceneName == "BoxCloset")
        {
            boxBoost = 15f;
        }

        if (GameManager.instance.sceneName == "TapeMeasure")
        {
            boxBoost = 15f;
            pullSpeed = 18f;
        }
        else
        {
            pullSpeed = 20f;
        }

        if (canJump || swim)
        {
            if (Input.GetButtonDown("Fire1") && !jump)
            {
                jump = true;
            }
        }

        //When you let go of the jump buttons, make jump false and fall
        if (Input.GetButtonUp("Fire1"))
        {
            jump = false;
            jumpCounter = 0;
        }

        ////Pickup objects
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    //Box kicking test
        //    if (heldObject == null)
        //    {
        //        if (lastR)
        //        {
        //            Vector3 facingDirection = Vector3.right;
        //            RaycastHit2D hit = Physics2D.Raycast(transform.position + (-facingDirection * sprite.bounds.extents.x * .5f) + (Vector3.down * sprite.bounds.extents.y * 0.3f), facingDirection, 1.5f, blockMask);
        //            heldObject = hit.collider.gameObject;
        //            heldObject.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
        //        }
        //        else if (lastL)
        //        {
        //            Vector3 facingDirection = Vector3.left;
        //            RaycastHit2D hit = Physics2D.Raycast(transform.position + (-facingDirection * sprite.bounds.extents.x) + (Vector3.down * sprite.bounds.extents.y * 0.3f), facingDirection, 1.5f, blockMask);
        //            heldObject = hit.collider.gameObject;
        //            heldObject.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //    else
        //    {
        //        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        //        {
        //            heldObject.SendMessage("RightDiagonal", SendMessageOptions.DontRequireReceiver);
        //            heldObject = null;
        //        }
        //        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        //        {
        //            heldObject.SendMessage("LeftDiagonal", SendMessageOptions.DontRequireReceiver);
        //            heldObject = null;
        //        }
        //        else if (Input.GetKey(KeyCode.UpArrow))
        //        {
        //            heldObject.SendMessage("Up", SendMessageOptions.DontRequireReceiver);
        //            heldObject = null;
        //        }
        //        else if (Input.GetKey(KeyCode.RightArrow))
        //        {
        //            heldObject.SendMessage("Right", SendMessageOptions.DontRequireReceiver);
        //            heldObject = null;
        //        }
        //        else if (Input.GetKey(KeyCode.LeftArrow))
        //        {
        //            heldObject.SendMessage("Left", SendMessageOptions.DontRequireReceiver);
        //            heldObject = null;
        //        }
        //        else if (Input.GetButtonDown("Fire2"))
        //        {
        //            heldObject.SendMessage("Drop", SendMessageOptions.DontRequireReceiver);
        //            heldObject = null;
        //        }
        //    }
        //}

        //Only do this in scene 5
        //if (GameManager.instance.sceneName == "Dream2")
        //{
        //    if (!GameManager.instance.specialBoxKilled)
        //    {
        //        if (heldObject != null)
        //        {
        //            if (heldObject.tag == "SpecialBox")
        //            {
        //                GameManager.instance.specialBoxKilled = true;
        //            }
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //run Grounded function to see if you're grounded every frame
        Grounded();
        //run wallCast function to see if you're touching a wall
        wallCast();

        //Press the left and right keys to move

        float xInput = Input.GetAxis("Horizontal");

        //If right or left if pressed, accel in that direction
        if (xInput > 0)
        {
            vel.x += accel / waterSpeed;
            lastR = true;
            lastL = false;
            if (!inactive)
            {
                sprite.flipX = false;
                if (grounded)
                {
                    anim.Play("RunningAnimation");
                }
            }
        }
        if (xInput < 0)
        {
            vel.x -= accel / waterSpeed;
            lastL = true;
            lastR = false;
            if (!inactive)
            {
                sprite.flipX = true;
                if (grounded)
                {
                    anim.Play("RunningAnimation");
                }
            }
        }

        //Limit the player's max velocity
        vel.x = Mathf.Max(Mathf.Min(vel.x, maxAccel), -maxAccel);

        //If you don't move right or left, then don't move
        if ( Mathf.Abs(xInput) < 0.1f || inactive)
        {
            if (grounded)
            {
                anim.Play("Idle");
            } else
            {
                anim.Play("Jump");
            }
            vel.x = 0;
        }

        //Jump and check if the button is still being held to vary jumps

        if (mashNumber > 0)
        {
            canJump = false;

            if (Input.GetButtonDown("Fire1"))
            {
                numberMashed += 1;
            }

            if (numberMashed >= mashLimit && latchedBoxes[0] != null)
            {
                latchedBoxes[0].SendMessage("BrokeFree", SendMessageOptions.DontRequireReceiver);
                latchedBoxes.RemoveAt(0);
                latchedBoxes.TrimExcess();
                mashNumber -= numberMashed;
                numberMashed = 0;
            }
        }

        if (jump)
        {
            anim.Play("Jump");
            if (Input.GetButton("Fire1"))
            {
                switch (jumpCounter)
                {
                    case 0:
                        break;
                    case 1:
                        jumpVel += 1;
                        break;
                    case 2:

                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        jumpVel += 3;
                        break;
                    case 6:
                        jump = false;
                        break;
                }
                jumpCounter++;
                if (!swim)
                {
                    vel.y = jumpVel;
                }
                else
                {
                    vel.y = jumpVel / (waterSpeed * .75f);
                }

            }
        }

        if (jumpVel > maxJumpVel)
        {
            jumpVel = maxJumpVel;
        }

        //Walljump code
        if (GameManager.instance.wallJumpUnlocked)
        {
            if (onWall)
            {
                slideVel += gravity * wallFriction;
                vel.y = slideVel;
                wallJumpTimer -= .1f;
                if (onWallLeft)
                    vel.x = Mathf.Max(vel.x, 0);
                if (onWallRight)
                    vel.x = Mathf.Min(vel.x, 0);
                if (Input.GetButtonDown("Fire1") && wallJumpTimer <= 0f)
                {
                    vel.x += onWallLeft ? wallJumpX : -wallJumpX;
                    vel.y = wallJumpY;
                    onWall = false;
                }
            }

            if (!onWall && !grounded)
            {
                wallJumpTimer = 1f;
            }
        }

        //Hook Code
        //Hook code will be throwing a projectile and drawing a linerenderer between the player and the projectile
        //Projectile will stop after a certain distance between the player

        if (GameManager.instance.hookshotUnlocked)
        {
            GameObject shotHook = GameObject.FindWithTag("HookShot");

            if ((Input.GetButtonDown("Fire2")) && !shot)
            {
                if (lastR)
                {
                    Instantiate(hookShot, new Vector3(transform.position.x + .25f, transform.position.y + .25f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -45)));
                    shot = true;
                }

                if (lastL)
                {
                    Instantiate(hookShot, new Vector3(transform.position.x - .25f, transform.position.y + .25f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 45)));
                    shot = true;
                }
            }

            if (shotHook != null)
            {
                shot = true;

                if (pull)
                {
                    Vector3 dir = (shotHook.transform.position - transform.position).normalized;

                    vel = dir * pullSpeed;
                }

                if (onWall)
                {
                    shotHook.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
                    pull = false;
                }
            }
            else
            {
                shot = false;
            }
        }

        if (GameManager.instance.sceneName == "Dream1")
        {
            if (GameManager.instance.monarchComeAlive == true)
            {
                inactive = true;
                canJump = false;
            }
            else
            {
                inactive = false;
            }
        }

        if (GameManager.instance.sceneName == "WallJump")
        {
            if (transform.position.y >= 12f)
            {
                GameManager.instance.switchScene = true;
            }
        }

        //Apply the movement
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
            canJump = true;
            jumpCounter = 0;
            jumpVel = baseJumpVel;
            slideVel = 0;
        }
        else
        {
            canJump = false;
            if (!swim)
            {
                vel.y += gravity;
            }
            else
            {
                vel.y += gravity / waterSpeed;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        GameObject shotHook = GameObject.FindWithTag("HookShot");

        if (coll.gameObject.tag == "Enemy")
        {
            GameObject collidedObject = coll.collider.gameObject;
            if (transform.position.y >= collidedObject.transform.position.y + 1f || pull)
            {
                collidedObject.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
                slideVel = 0;
                vel.y = boxBoost;
                shotHook.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
                pull = false;
            }
            else
            {
                collidedObject.SendMessage("Latch", SendMessageOptions.DontRequireReceiver);
                latchedBoxes.Add(collidedObject);
                mashNumber += mashLimit;
            }
        }

        if (coll.gameObject.tag == "SpecialEnemy")
        {
            GameObject collidedObject = coll.collider.gameObject;
            if (transform.position.y >= collidedObject.transform.position.y + 1f || pull)
            {
                if (GameManager.instance.sceneName == "Dream2" || (GameManager.instance.sceneName == "Dream3") || (GameManager.instance.sceneName == "Dream4"))
                {
                    GameManager.instance.specialBoxKilled = true;
                }
                collidedObject.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
                slideVel = 0;
                vel.y = boxBoost;
                shotHook.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
                pull = false;
            }
        }

        if (coll.gameObject.tag == "Box")
        {
            shotHook.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
            pull = false;
        }

        if (coll.gameObject.tag == "PlayerTop")
        {
            GameObject collidedObject = coll.collider.gameObject;
            if (transform.position.y >= collidedObject.transform.position.y + 1f)
                shotHook.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
                pull = false;
            {
                collidedObject.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
            }
        }

        if (coll.gameObject.tag == "BrokenFloor")
        {
            falling = true;
        }

        if (coll.gameObject.tag == "Monarch")
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            waterSpeed = 3;
            vel.y = vel.y / 3;
            slideVel = 0;
            swim = true;
        }

        if (coll.gameObject.tag == "SpecialCobweb")
        {
            Destroy(coll.gameObject);
        }

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            waterSpeed = 3;
            swim = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            waterSpeed = 1;
            swim = false;
        }
    }

    void wallCast()
    {
        Vector2 top = (Vector2)transform.position + box.offset + (Vector2.up * (box.size.y / 2f));
        Vector2 bot = (Vector2)transform.position + box.offset - (Vector2.up * (box.size.y / 2f));
        onWallLeft = Physics2D.Raycast(top, Vector2.left, box.size.x * .75f, wallMask) || Physics2D.Raycast(bot, Vector2.left, box.size.x * .75f, wallMask);
        onWallRight = Physics2D.Raycast(top, Vector2.right, box.size.x * .75f, wallMask) || Physics2D.Raycast(bot, Vector2.right, box.size.x * .75f, wallMask);
        onWall = onWallLeft || onWallRight;

        if (onWall)
        {
            shot = true;
        }
    }

    public void Retracted()
    {
        shot = false;
    }

}