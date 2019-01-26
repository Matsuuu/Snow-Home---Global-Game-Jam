using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class OutgrowTextLauncherScript : MonoBehaviour
{
    private SnowBallScript snowBallScript;
    private InstructionScript instructionScript;
    public GameObject house;
    public Vector3 offSet;

    public Text tooBigText;

    public Text everybodyDoesText;
    public Text soYouMoveOnText;

    private bool textFadeInHasBeenLaunched;

    public FaderScript fader;
    // Start is called before the first frame update
    void Start()
    {
        instructionScript = GameObject.FindGameObjectWithTag("InstructionScript").GetComponent<InstructionScript>();
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderScript>();
        snowBallScript = GameObject.FindGameObjectWithTag("Snowball").GetComponent<SnowBallScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!textFadeInHasBeenLaunched)
        {
            transform.position = snowBallScript.transform.position + offSet;
        }

        if (snowBallScript.transform.localScale.x > snowBallScript.maxScale / 2 && !textFadeInHasBeenLaunched && snowBallScript.transform.position.z > house.transform.position.z + 5)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(LaunchOutgrowText());
        }
    }

    IEnumerator LaunchOutgrowText()
    {
        float oldSpeed = snowBallScript.movementSpeed;
        snowBallScript.SlowDown();
        fader.FadeIn(tooBigText);
        yield return new WaitForSeconds(5);
        fader.FadeIn(everybodyDoesText);
        yield return new WaitForSeconds(2);
        fader.FadeOut(tooBigText);
        fader.FadeOut(everybodyDoesText);
        fader.FadeIn(soYouMoveOnText);
        snowBallScript.movementSpeed = oldSpeed;
        yield return new WaitForSeconds(1);
        instructionScript.UpdateInstructionText("Hold W to move on");
        yield return new WaitForSeconds(4);
        instructionScript.FadeOut();
        GetComponent<OutgrowTextLauncherScript>().enabled = false;
    }

}
