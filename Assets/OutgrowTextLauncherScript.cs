using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class OutgrowTextLauncherScript : MonoBehaviour
{
    private SnowBallScript snowBallScript;
    public GameObject house;
    public Vector3 offSet;

    public Text tooBigText;

    public Text everybodyDoesText;

    private bool textFadeInHasBeenLaunched;
    // Start is called before the first frame update
    void Start()
    {
        snowBallScript = GameObject.FindGameObjectWithTag("Snowball").GetComponent<SnowBallScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!textFadeInHasBeenLaunched)
        {
            transform.position = snowBallScript.transform.position + offSet;
        }

        if (snowBallScript.transform.localScale.x > snowBallScript.maxScale && !textFadeInHasBeenLaunched && snowBallScript.transform.position.z > house.transform.position.z)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(LaunchOutgrowText());
        }
    }

    IEnumerator LaunchOutgrowText()
    {
        float oldSpeed = snowBallScript.movementSpeed;
        snowBallScript.movementSpeed = 0.1f;
        StartCoroutine(FadeIn(tooBigText));
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeIn(everybodyDoesText));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOut(tooBigText));
        StartCoroutine(FadeOut(everybodyDoesText));
        snowBallScript.movementSpeed = oldSpeed;
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    IEnumerator FadeIn(Text target)
    {
        for (int i = 0; i < 10; i++)
        {
            Color targetColor = target.color;
            targetColor.a += 0.1f;
            Debug.Log("fadein");
            target.color = targetColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut(Text target)
    {
        for (int i = 0; i < 10; i++)
        {
            Color targetColor = target.color;
            targetColor.a -= 0.1f;
            Debug.Log(targetColor.a);
            target.color = targetColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
