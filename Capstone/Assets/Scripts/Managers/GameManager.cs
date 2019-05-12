using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager instance;

    //The transparency number for the fade screen that goes from 0-1
    public float alphaNum;
    public bool fadeIn;

    public bool switchScene;
    bool changingScene;

    public bool playerSwitch;

    public bool monarchComeAlive;
    public bool monarchFlying;

    public int sceneID;
    public string sceneName;

    private AudioSource audioSource;

    public AudioClip flying;

    public bool playFlying;

    //Assign a task before the fade-in with tutorial scenes

   public bool taskRead;

    //Only pertains to the box closet scene

    public bool specialBoxKilled;

    public bool playerFallen;

    public bool dreamStarted;

    public bool hookshotUnlocked;
    public bool wallJumpUnlocked;

    public GameObject[] boxSpots = new GameObject[2];
    public int currentSpot;
    public Sprite boxSprite;

    public bool boxPlaced;

    public bool monarchCaught;

    public int retractLimit;

    public bool readText;

    int pauseCounter;

    public bool paused;

    int pauseOptionCounter;
    public GameObject resume;
    public GameObject returnToTitle;

    public Color selectedColor;
    public Color unselectedColor;

    // Use this for initialization

    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        Cursor.visible = false;

        Time.timeScale = 1;
        alphaNum = 1f;
        fadeIn = true;
        changingScene = false;

        readText = false;

        audioSource = GetComponent<AudioSource>();

        //Get the current scene you are on
        sceneID = SceneManager.GetActiveScene().buildIndex;
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Dream3" || sceneName == "TapeMeasure")
        {
            hookshotUnlocked = true;
        }

        if (sceneName == "Dream4")
        {
            hookshotUnlocked = true;
            wallJumpUnlocked = true;
        }

        if (sceneName == "Cobweb")
        {
            wallJumpUnlocked = true;
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause")  && GameManager.instance.sceneName != "Title")
        {
            pauseCounter += 1;
        }

        switch (pauseCounter)
        {
            case 0:
                paused = false;
                break;
            case 1:
                paused = true;
                alphaNum = .85f;
                Time.timeScale = 0f;
                break;
            case 2:
                alphaNum = 0;
                Time.timeScale = 1;
                pauseCounter = 0;
                break;
        }

        float yInput = Input.GetAxisRaw("Vertical");

        if (yInput > 0)
        {
            pauseOptionCounter = 0;
        }

        if (yInput < 0)
        {
            pauseOptionCounter = 1;
        }

        if (Mathf.Abs(yInput) < 0.1f)
        {
            yInput = 0;
        }

        if (!paused)
        {
            resume.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            returnToTitle.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            pauseOptionCounter = 0;
        }
        else
        {
            if (pauseOptionCounter == 0)
            {
                resume.GetComponent<SpriteRenderer>().color = selectedColor;
                returnToTitle.GetComponent<SpriteRenderer>().color = unselectedColor;

                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    pauseCounter = 2;
                }
            }

            if (pauseOptionCounter == 1)
            {
                resume.GetComponent<SpriteRenderer>().color = unselectedColor;
                returnToTitle.GetComponent<SpriteRenderer>().color = selectedColor;

                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    SceneManager.LoadScene("Title");
                    pauseCounter = 2;
                }

            }
        }

        if (paused)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }

        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneID);
        }

        //Always fade in to a new scene
        if (fadeIn && alphaNum > 0f)
        {
            if (sceneName == "BoxCloset" || sceneName == "TapeMeasure" || sceneName == "Cobweb" || sceneName == "Weekend")
            {
                if (Input.GetButtonDown("Fire1") && readText)
                {
                    taskRead = true;
                }

                if (taskRead)
                {
                    alphaNum -= .4f * Time.deltaTime;
                }
            }
            else
            {
                alphaNum -= .5f * Time.deltaTime;
            }

        }
        else if (!fadeIn && alphaNum < 1f)
        {
            alphaNum += .4f * Time.deltaTime;
        }

        if (alphaNum >= 1f)
        {
            alphaNum = 1f;
        }
        else if (alphaNum <= 0f)
        {
            alphaNum = 0f;
        }

        //Call the scene change coroutine when switchScene is called
        if (switchScene)
        {
            if (!changingScene)
            {
                StartCoroutine(SceneChange());
                changingScene = true;
            }
        }

        //Only things done in scene 3
        if (sceneName == "Dream1")
        {
            if (playFlying)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }

        if (sceneName == "Dream2" || sceneName == "Dream3" || sceneName == "Dream4")
        {

            if (specialBoxKilled)
            {
                monarchFlying = true;
                playFlying = true;
                if (playFlying)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
            }

        }

        if (boxPlaced)
        {
            SpriteRenderer spriteRef = boxSpots[currentSpot].GetComponent<SpriteRenderer>();
            spriteRef.sprite = boxSprite;
            currentSpot += 1;
            boxPlaced = false;
        }

        switch (currentSpot)
        {
            case 0:
                break;
            case 1:
                alphaNum = .15f;
                break;
            case 2:
                alphaNum = .3f;
                break;
            case 3:
                alphaNum = .45f;
                break;
            case 4:
                fadeIn = false;
                alphaNum += .25f * Time.deltaTime;
                dreamStarted = true;
                break;
        }
        
        if (monarchCaught)
        {
            boxPlaced = true;
            fadeIn = true;
            alphaNum += .5f * Time.deltaTime;
            StartCoroutine(DayDreamDone());
        }

    }


    //After a short delay, change to a different scene
    IEnumerator SceneChange()
    {
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        SceneManager.LoadScene(sceneID += 1);
    }

    IEnumerator DayDreamDone()
    {
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(SceneChange());
    }
}