using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DreamManager : MonoBehaviour
{

    public Text timeText;

    float dreamTimeHour;
    float dreamTimeMinTen;
    float dreamTimeMinOne;

    public float timeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Dream2")
        {
            dreamTimeHour = 1;
            dreamTimeMinTen = 3;
            dreamTimeMinOne = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Dream2")
        {
            dreamTimeMinOne += 1 * Time.deltaTime * timeSpeed;

            if (dreamTimeMinOne >= 10)
            {
                dreamTimeMinTen += 1;
                dreamTimeMinOne = 0;
            }

            if (dreamTimeMinTen >= 6)
            {
                dreamTimeHour += 1;
                //dreamTimeMinOne = 0;
                dreamTimeMinTen = 0;
            }

            timeText.text = dreamTimeHour + ":" + dreamTimeMinTen + Mathf.FloorToInt(dreamTimeMinOne) + " AM";
        }
    }
}
