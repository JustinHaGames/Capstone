using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    Text txt;
    string story;

    float alpha = 1;

    AudioSource SFX;

    void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";

        SFX = GetComponent<AudioSource>();

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    private void FixedUpdate()
    {

        if (GameManager.instance.taskRead)
        {
            txt.color = new Color(txt.color.r, txt.color.b, txt.color.g, alpha -= .25f * Time.deltaTime);
        }
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            SFX.pitch = Random.Range(0.975f, 1.2f);
            SFX.PlayOneShot(SFX.clip,1);
            yield return new WaitForSeconds(0.125f);
        }
    }
}
