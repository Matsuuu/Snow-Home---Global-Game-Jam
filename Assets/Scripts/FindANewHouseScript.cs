using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindANewHouseScript : MonoBehaviour
{
    private SnowBallScript snowBallScript;
    private InstructionScript instructionScript;
    public GameObject lightpost;
    public Vector3 offSet;

    private bool textFadeInHasBeenLaunched;

    public Text newHouseText;
    public Text itsNotText;

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
        fader.FadeIn(newHouseText);
        yield return new WaitForSeconds(4);
        fader.FadeIn(itsNotText);
        yield return new WaitForSeconds(2);
        instructionScript.UpdateInstructionText("Hold W to feel like home");
        yield return new WaitForSeconds(1);
        snowBallScript.movementSpeed = oldMoveSpeed;
        yield return new WaitForSeconds(3);
        instructionScript.FadeOut();
    }
}
