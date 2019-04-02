using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceText : MonoBehaviour
{

    GameObject tapeMeasureTip;
    GameObject realityPlayer;

    float dist;

    public Text distText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tapeMeasureTip = GameObject.FindWithTag("TapeMeasure");
        realityPlayer = GameObject.FindWithTag("RealityPlayer");

        if (tapeMeasureTip != null)
        {
            dist = Vector3.Distance(tapeMeasureTip.transform.position, realityPlayer.transform.position);
            distText.text = "" + Mathf.FloorToInt(dist);
        }
        else if (tapeMeasureTip == null || Mathf.Abs(Mathf.FloorToInt(dist)) <=3f)
        {
            distText.text = "";
        }

        if (realityPlayer.transform.position.x > tapeMeasureTip.transform.position.x)
        {
            transform.position = Camera.main.WorldToScreenPoint(new Vector3(tapeMeasureTip.transform.position.x + (dist / 2), tapeMeasureTip.transform.position.y, tapeMeasureTip.transform.position.z));
        }
        else
        {
            transform.position = Camera.main.WorldToScreenPoint(new Vector3(tapeMeasureTip.transform.position.x - (dist / 2), tapeMeasureTip.transform.position.y, tapeMeasureTip.transform.position.z));

        }

    }

}
