using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplerTextScript : MonoBehaviour
{
    private SnowBallScript snowBallScript;
    public Vector3 offSet;

    private bool textFadeInHasBeenLaunched;
    private bool textHasBeenShown;

    public Text simplerText;
    public FaderScript fader;

    public Text youHaveToKeepMovingOnText;

    private void Start()
    {
        snowBallScript = GameObject.FindGameObjectWithTag("Snowball").GetComponent<SnowBallScript>();
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderScript>();
    }

    private void Update()
    {
        if (!textHasBeenShown)
        {
            transform.position = snowBallScript.transform.position + offSet;
        }
        if (!textFadeInHasBeenLaunched && snowBallScript.transform.position.x > youHaveToKeepMovingOnText.transform.position.x)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(ShowSimplerText());
        }
    }

    public void LaunchText()
    {
    }

    private IEnumerator ShowSimplerText()
    {
        Debug.Log("Launch");
        yield return new WaitForSeconds(6);
        Debug.Log("Text");
        textHasBeenShown = true;
        fader.FadeIn(simplerText);
    }
}
