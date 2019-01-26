using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionScript : MonoBehaviour
{

    private Text thisText;
    public Text yourHomeText;

    private FaderScript fader;

    public AudioSource audio;
    public bool hasStartedMoving;
    // Start is called before the first frame update
    void Start()
    {
        
        thisText = GetComponent<Text>();
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !hasStartedMoving)
        {
            hasStartedMoving = true;
            audio.Play();
            FadeOut();
            fader.FadeIn(yourHomeText);
        }
    }

    public void UpdateInstructionText(String instructionText)
    {
        thisText.text = instructionText;
        FadeIn();
    }

    public void FadeIn()
    {
        fader.FadeIn(thisText);
    }

    public void FadeOut()
    {
        fader.FadeOut(thisText);
    }
}
