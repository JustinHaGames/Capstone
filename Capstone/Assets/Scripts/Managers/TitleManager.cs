using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    int selection = 0;

    public GameObject playButton;
    public GameObject quitButton;

    public Color selectedColor;
    public Color unselectedColor;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Reset all variables when game resets
        GameManager.instance.monarchComeAlive = false;
        GameManager.instance.monarchFlying = false;
        GameManager.instance.playFlying = false;
    }

    private void Update()
    {
        if (GameManager.instance.paused)
        {
            if (audioSource.isPlaying)
                audioSource.Pause();

        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.UnPause();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yInput = Input.GetAxis("Vertical");

        if (yInput > 0)
        {
            selection = 0;
        }

        if (yInput < 0)
        {
            selection = 1;
        }

        if (Mathf.Abs(yInput) < 0.1f)
        {
            yInput = 0;
        }

        if (selection == 0)
        {
            playButton.GetComponent<SpriteRenderer>().color = selectedColor;
            quitButton.GetComponent<SpriteRenderer>().color = unselectedColor;

            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                GameManager.instance.switchScene = true;
                GameManager.instance.fadeIn = false;
            }
        }

        if (selection == 1)
        {
            playButton.GetComponent<SpriteRenderer>().color = unselectedColor;
            quitButton.GetComponent<SpriteRenderer>().color = selectedColor;

            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Application.Quit();
            }

        }

    }
}
