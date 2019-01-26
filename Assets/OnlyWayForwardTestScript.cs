using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyWayForwardTestScript : MonoBehaviour
{
    private InstructionScript instructionScript;
    private SnowBallScript snowBallScript;
    public WhereAreYouHeadedScript lastCheckPoint;
    private PlayerCameraFollowScript camera;
    public GameObject lightpost;
    public Vector3 offSet;

    private bool textFadeInHasBeenLaunched;

    public Text onlyWayForwardText;
    public Text soLetsKeepMovingText;

    public bool hasBeenTriggered;

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

        if (!textFadeInHasBeenLaunched && snowBallScript.transform.position.z < lightpost.transform.position.z && lastCheckPoint.hasBeenTriggered)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(LaunchText());
        }
    }

    IEnumerator LaunchText()
    {
        float oldMoveSpeed = snowBallScript.movementSpeed;
        snowBallScript.SlowDown();
        fader.FadeIn(onlyWayForwardText);
        yield return new WaitForSeconds(1);
        fader.FadeIn(soLetsKeepMovingText);
        yield return new WaitForSeconds(3);
        snowBallScript.movementSpeed = oldMoveSpeed * 1.5f;
        instructionScript.UpdateInstructionText("Hold W to keep going");
        hasBeenTriggered = true;
    }
}
