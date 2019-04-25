using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualWinner : MonoBehaviour
{

    bool left;
    bool right;
    bool stop;

    public float speed;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        right = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (left)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            anim.Play("CasualWinnerWalk");
            right = false;
        }

        if (right)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            anim.Play("CasualWinnerWalk");
            left = false;
        }

        if (stop)
        {
            left = false;
            right = false;
            WeekendManager.instance.winnerStopped = true;
            anim.Play("CasualWinnerIdle");
        }

        if (transform.position.x >= 74f)
        {
            stop = true;
        }

    }
}
