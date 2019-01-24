using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBelieveInYouScript : MonoBehaviour
{
    private InstructionScript instructionScript;
    private SnowBallScript snowBallScript;
    public OnlyWayForwardTestScript lastCheckPoint;
    private PlayerCameraFollowScript camera;
    public GameObject lightpost;
    public Vector3 offSet;

    private bool textFadeInHasBeenLaunched;

    public Text mightNotMeanMuchText;
    public Text iBelieveInYOuText;
    public Text followYourDreamsText;

    public bool hasBeenTriggered;
    public bool isHoldingOn = true;
    public bool hasBeenPromptedToHoldOn;

    public FaderScript fader;
    // Start is called before the first frame update
    void Start()
    {
        instructionScript = GameObject.FindGameObjectWithTag("InstructionScript").GetComponent<InstructionScript>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCameraFollowScript>();
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderScript>();
        snowBallScript = GameObject.FindGameObjectWithTag("Snowball").GetComponent<SnowBallScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenPromptedToHoldOn && !isHoldingOn)
        {
            Application.Quit();
        }
        if (!textFadeInHasBeenLaunched)
        {
            transform.position = snowBallScript.transform.position + offSet;
        }

        if (!textFadeInHasBeenLaunched && snowBallScript.transform.position.z < lightpost.transform.position.z && lastCheckPoint.hasBeenTriggered)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(LaunchText());
        }

        if (Input.GetKeyUp(KeyCode.W) && hasBeenPromptedToHoldOn)
        {
            isHoldingOn = false;
        }
    }

    IEnumerator LaunchText()
    {
        float oldMoveSpeed = snowBallScript.movementSpeed;
        snowBallScript.SlowDown();
        fader.FadeIn(mightNotMeanMuchText);
        yield return new WaitForSeconds(2);
        fader.FadeIn(iBelieveInYOuText);
        instructionScript.FadeOut();
        yield return new WaitForSeconds(2);
        fader.FadeIn(followYourDreamsText);
        yield return new WaitForSeconds(2);
        snowBallScript.movementSpeed = oldMoveSpeed;
        snowBallScript.maxScale = snowBallScript.maxScale * 15;
        fader.FadeOut(mightNotMeanMuchText);
        yield return new WaitForSeconds(1);
        fader.FadeOut(iBelieveInYOuText);
        yield return new WaitForSeconds(1);
        fader.FadeOut(followYourDreamsText);
        
        yield return new WaitForSeconds(4);
        instructionScript.UpdateInstructionText("Hold W to keep holding on");
        
        camera.RotateCamera(200);
        hasBeenTriggered = true;
        yield return new WaitForSeconds(2);
        hasBeenPromptedToHoldOn = true;
    }
}
