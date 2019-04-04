using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapeMeasureManager : MonoBehaviour
{

    public GameObject tapeMeasure;

    public GameObject RealityPlayer;

    GameObject tapeMeasureTip;

    LineRenderer tapeMeasureLine;

    bool retract;

    float dist;

    // Start is called before the first frame update
    void Start()
    {
       tapeMeasureLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        tapeMeasureTip = GameObject.FindWithTag("TapeMeasure");

        if (RealityPlayer.transform.position.x <= -7f && Input.GetButtonDown("Fire2"))
        {
            Instantiate(tapeMeasure, new Vector3(-7.85f, RealityPlayer.transform.position.y, transform.position.z), Quaternion.identity);
        }

        if (RealityPlayer.transform.position.x >= 7f && Input.GetButtonDown("Fire2"))
        {
            Instantiate(tapeMeasure, new Vector3(7.85f, RealityPlayer.transform.position.y, transform.position.z), Quaternion.identity);
        }

        if (tapeMeasureTip != null)
        {
            tapeMeasureLine.SetPosition(0, new Vector3(RealityPlayer.transform.position.x, RealityPlayer.transform.position.y, RealityPlayer.transform.position.z + 1));
            tapeMeasureLine.SetPosition(1, new Vector3(tapeMeasureTip.transform.position.x, tapeMeasureTip.transform.position.y, tapeMeasureTip.transform.position.z + 1));
        }

        if (GameManager.instance.monarchCaught)
        {
            Instantiate(tapeMeasure, new Vector3(-7.85f, RealityPlayer.transform.position.y, transform.position.z), Quaternion.identity);
            RealityPlayer.transform.position = new Vector3(7.85f, RealityPlayer.transform.position.y, RealityPlayer.transform.position.z);
        }
    }
}
