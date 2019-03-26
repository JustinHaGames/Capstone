using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	Vector2 vel; 

	Rigidbody2D rb; 
	BoxCollider2D box; 

	bool grounded; 
	bool stacked; 
	bool boxPushing;

	bool set;

	bool held; 

	Vector3 setPosition; 

	public float throwVel; 
	public float upThrowVel;

	public float gravity; 
	public float maxGravity;

	bool floating; 

	//Was last action left or right throw?
	bool lastL;
	bool lastR; 

	GameObject player;
    GameObject realityPlayer;

	bool thrown; 

	BoxCollider2D playerTopCollider; 
	BoxCollider2D boxTopCollider;

	GameObject boxPusher;

	//Only applicable in 4th scene
	bool safe;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		box = GetComponent<BoxCollider2D> ();

		playerTopCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
		boxTopCollider = transform.GetChild (1).GetComponent<BoxCollider2D> ();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

		player = GameObject.FindGameObjectWithTag ("Player");
		boxPusher = GameObject.FindGameObjectWithTag ("BoxPusher");

        if (GameManager.instance.sceneName == "BoxCloset")
        {
            realityPlayer = GameObject.FindWithTag("RealityPlayer");
        }

        Grounded (); 

		if (floating) {
			vel.y = 1f; 
		}
			
		if (player != null) {
			if (player.transform.position.y >= transform.position.y + 1.1f) {
				playerTopCollider.enabled = true;
			} else {
				playerTopCollider.enabled = false;
			}
		} else {
			playerTopCollider.enabled = true;
		}

		if (held) {
			boxTopCollider.enabled = false;
			box.enabled = false;
		} else {
			boxTopCollider.enabled = true;
			box.enabled = true; 
		}

		if (!held) {
			rb.MovePosition ((Vector2)transform.position + vel * Time.deltaTime);
		}

	}

	void Grounded(){
		Vector2 pt1 = transform.TransformPoint (box.offset + new Vector2 (box.size.x / 2, -box.size.y / 2));
		Vector2 pt2 = transform.TransformPoint (box.offset - (box.size / 2) + new Vector2 (0, 0));

		grounded = Physics2D.OverlapArea (pt1, pt2, LayerMask.GetMask ("Platform")) != null; 
		stacked = Physics2D.OverlapArea (pt1, pt2, LayerMask.GetMask ("BoxTop")) != null;

		if (!held) {
			boxPushing = Physics2D.OverlapArea (pt1, pt2, LayerMask.GetMask ("BoxPusher")) != null;
		}

		if (grounded && !boxPushing) {
			vel.x = 0; 
			vel.y = 0; 
			lastL = false;
			lastR = false; 
			thrown = false;
		} else {
			//If you threw the box and the last throw was not thrown left or right
			if (!held && !lastL && !lastR && !stacked) {
				vel.y += gravity;
			} else {
				//if you haven't throw it, don't apply gravity
				vel.y = 0; 
			}
		}

		if (vel.y <= maxGravity) {
			vel.y = maxGravity;
		}

		if (stacked) {
			vel.x = 0; 
			vel.y = 0; 
			lastL = false;
			lastR = false;
			thrown = false;
		}

		if (boxPushing) {
			if (!held) {
				transform.parent = boxPusher.transform;
				grounded = false;
			} else {
				transform.parent = this.transform;
			}
		}

        if (gameObject.tag == "GreyBox" && GameObject.FindWithTag("Monarch") != null)
        {
            Destroy(gameObject);
        }

        if (GameManager.instance.monarchCaught)
        {
            Destroy(gameObject);
        }


    }

	void OnCollisionEnter2D(Collision2D coll){
		if (lastL || lastR) {
			if (coll.gameObject.tag == "Box") {
				lastL = false; 
				lastR = false; 
				vel.x = 0; 
			}
			if (coll.gameObject.tag == "Wall") {
				lastL = false; 
				lastR = false; 
				vel.x = 0; 
			}
		}
			
			if (coll.gameObject.tag == "Enemy") {
				if (thrown) {
					GameObject collidedObject = coll.collider.gameObject;
					collidedObject.SendMessage ("Dead", SendMessageOptions.DontRequireReceiver);
				}
			}
	}

	public void PickUp() {
		rb.isKinematic = true; 
		transform.position =  new Vector3(realityPlayer.transform.position.x, realityPlayer.transform.position.y + .5f, realityPlayer.transform.position.z - 1f ); 
		transform.parent = realityPlayer.transform;
		held = true;
		thrown = false;
	}

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //public void Up () {
    //	transform.parent = null;
    //	rb.isKinematic = false;
    //	vel.y = upThrowVel; 
    //	lastL = false;
    //	lastR = false; 
    //	held = false; 
    //	thrown = true; 
    //}

    //public void Right () {
    //	transform.parent = null;
    //	rb.isKinematic = false;
    //	vel.x = throwVel; 
    //	lastR = true; 
    //	held = false; 
    //	thrown = true; 
    //}

    //public void Left () {
    //	transform.parent = null;
    //	rb.isKinematic = false;
    //	vel.x = -throwVel; 
    //	lastL = true;
    //	held = false; 
    //	thrown = true; 
    //}

    //public void RightDiagonal (){
    //	transform.parent = null;
    //	rb.isKinematic = false;
    //	vel.y = upThrowVel; 
    //	vel.x = throwVel;
    //	lastL = false;
    //	lastR = false; 
    //	held = false; 
    //	thrown = true; 
    //}

    //public void LeftDiagonal () {
    //	transform.parent = null;
    //	rb.isKinematic = false;
    //	vel.y = upThrowVel; 
    //	vel.x = -throwVel;
    //	lastL = false;
    //	lastR = false; 
    //	held = false; 
    //	thrown = true; 
    //}

    //public void Drop () {
    //	transform.parent = null;
    //	rb.isKinematic = false;
    //	held = false; 
    //	thrown = true;
    //}
}
