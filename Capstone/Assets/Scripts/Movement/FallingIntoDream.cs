using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIntoDream : MonoBehaviour
{

    public float speed;
    public float scaleSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
        transform.localScale -= new Vector3(scaleSpeed,scaleSpeed, 0);

        if (transform.localScale.x <= 0) {
            transform.localScale = new Vector3(0, 0, 0);
            GameManager.instance.switchScene = true;
            GameManager.instance.fadeIn = false;
        }
    }
}
