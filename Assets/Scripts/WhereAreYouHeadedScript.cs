using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhereAreYouHeadedScript : MonoBehaviour
{
    private InstructionScript instructionScript;
    private SnowBallScript snowBallScript;
    private PlayerCameraFollowScript camera;
    public GameObject lightpost;
    public Vector3 offSet;

    private bool textFadeInHasBeenLaunched;

    public Text whereDoYouThinkYoureHeadedText;

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

        if (!textFadeInHasBeenLaunched && snowBallScript.transform.position.x > lightpost.transform.position.x)
        {
            textFadeInHasBeenLaunched = true;
            StartCoroutine(LaunchText());
        }
    }

    IEnumerator LaunchText()
    {
        float oldMoveSpeed = snowBallScript.movementSpeed;
        snowBallScript.SlowDown();
        fader.FadeIn(whereDoYouThinkYoureHeadedText);
        yield return new WaitForSeconds(3);
        fader.FadeOut(whereDoYouThinkYoureHeadedText);
        yield return new WaitForSeconds(1.5f);
        snowBallScript.movementSpeed = oldMoveSpeed;
        camera.RotateCamera(250);
        hasBeenTriggered = true;
        instructionScript.UpdateInstructionText("Hold W to head towards the unknown");
    }
}
