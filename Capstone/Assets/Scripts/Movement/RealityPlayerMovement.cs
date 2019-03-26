using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityPlayerMovement : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	SpriteRenderer sprite;

    Animator anim;

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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator>();
		sprite.flipX = true;

        lastL = false;
        lastR = true;
	}

    private void Update()
    {
        //Pickup objects
        if (Input.GetButtonDown("Fire1") && !inactive)
        {
            //Box kicking test
            if (heldObject == null)
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

        if (heldObject != null){
            holdingBox = true;
        }
        else
        {
            holdingBox = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
    
        float xInput = Input.GetAxis("Horizontal");

        //Move Left
        if (xInput < 0) {
            if (!holdingBox)
            {
                anim.Play("RealityPlayerWalk");
            }
            else
            {
                anim.Play("RealityPlayerBoxWalk");
            }
            vel.x -= accel;
			sprite.flipX = true;
            lastL = true;
            lastR = false;
		}

        //Move Right
		if (xInput > 0) {
            if (!holdingBox)
            {
                anim.Play("RealityPlayerWalk");
            }
            else
            {
                anim.Play("RealityPlayerBoxWalk");
            }
            vel.x += accel;
			sprite.flipX = false;
            lastL = false;
            lastR = true;
		}

		//Limit the player's max velocity
        vel.x = Mathf.Max (Mathf.Min (vel.x, maxAccel), -maxAccel);
        //If you don't move right or left, then don't move
        if (Mathf.Abs(xInput) < 0.1f)
        {
            if (!holdingBox)
            {
                anim.Play("RealityPlayerIdle");
            } else
            {
                anim.Play("RealityPlayerBoxIdle");
            }
            vel.x = 0;
        }

        if (!spawnedDreamPlayer && GameManager.instance.dreamStarted == true)
        {
            StartCoroutine(DayDream());
            spawnedDreamPlayer = true;
            inactive = true;
        }

        rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);

	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
   
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
    }

    IEnumerator DayDream()
    {
        for (int i = 0; i < 75; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        Instantiate(dreamPlayer, new Vector3(0, 0, -7f), Quaternion.identity);
        Instantiate(monarch, new Vector3(0, 6f, -7f), Quaternion.identity);
    }
}