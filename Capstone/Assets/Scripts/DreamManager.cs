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
    public float alarmTime;

    float time;

    bool stop;

    public Color clearColor;
    public Color fullColor;

    public float startShowingTime;

    // Start is called before the first frame update
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Dream1")
        {
            dreamTimeHour = 1;
            dreamTimeMinTen = 3;
            dreamTimeMinOne = 0;
            timeText.text = "";
        }

        if (SceneManager.GetActiveScene().name == "Dream2")
        {
            dreamTimeHour = 1;
            dreamTimeMinTen = 3;
            dreamTimeMinOne = 0;
            timeText.text = "";
        }

        timeText.color = clearColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().name == "Dream1" && !stop)
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
                dreamTimeMinTen = 0;
            }

            time = (dreamTimeHour * 100) + (dreamTimeMinTen * 10) + dreamTimeMinOne;

            timeText.text = dreamTimeHour + ":" + dreamTimeMinTen + Mathf.FloorToInt(dreamTimeMinOne) + " AM";
        }

        if (SceneManager.GetActiveScene().name == "Dream2" && GameManager.instance.specialBoxPickedUp && !stop)
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
                dreamTimeMinTen = 0;
            }

            time = (dreamTimeHour * 100) + (dreamTimeMinTen * 10) + dreamTimeMinOne;

            timeText.text = dreamTimeHour + ":" + dreamTimeMinTen + Mathf.FloorToInt(dreamTimeMinOne) + " AM";

            if (time >= startShowingTime)
            {
                timeText.color = Color.Lerp(clearColor, fullColor, Mathf.Pow(1 - ((alarmTime - time) /(alarmTime - startShowingTime)), 3));
            }
        }

        if (time == alarmTime)
        {
            stop = true;
            GameManager.instance.switchScene = true;
            GameManager.instance.fadeIn = false;
        }

    }
}
