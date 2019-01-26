using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouMightGetLostOnYourWayScript : MonoBehaviour
{
    private SnowBallScript snowBallScript;
    private InstructionScript instructionScript;
    private PlayerCameraFollowScript camera;
    public GameObject lightpost;
    public Vector3 offSet;

    private bool textFadeInHasBeenLaunched;

    public Text mightGetLostText;
    public Text youJustCantStopText;
    public Text youHavetToKeepMovingOnText;
    public SimplerTextScript simpler;

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
        if (!textFadeInHasBeenLaunched)
        {
            transform.position = snowBallScript.transform.position + offSet;
        }

        if (!textFadeInHasBeenLaunched && snowBallScript.transform.position.z > lightpost.transform.position.z)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(LaunchNewHouseText());
        }
    }

    IEnumerator LaunchNewHouseText()
    {
        float oldMoveSpeed = snowBallScript.movementSpeed;
        snowBallScript.SlowDown();
        fader.FadeIn(mightGetLostText);
        yield return new WaitForSeconds(4);
        fader.FadeIn(youJustCantStopText);
        instructionScript.UpdateInstructionText("Hold W to not stop");
        yield return new WaitForSeconds(1.5f);
        fader.FadeOut(mightGetLostText);
        fader.FadeOut(youJustCantStopText);
        Destroy(lightpost);
        yield return new WaitForSeconds(1.5f);
        instructionScript.FadeOut();
        snowBallScript.movementSpeed = oldMoveSpeed;
        camera.RotateCamera(220);
        yield return new WaitForSeconds(8);
        fader.FadeIn(youHavetToKeepMovingOnText);
        yield return new WaitForSeconds(2);
        instructionScript.UpdateInstructionText("Hold W to keep moving on");
        yield return new WaitForSeconds(3);
        instructionScript.FadeOut();

    }
}
