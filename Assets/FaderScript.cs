using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn(Text target)
    {
        StartCoroutine(DoFadeIn(target));
    }

    public void FadeOut(Text target)    
    {
        StartCoroutine(DoFadeOut(target));
    }
    
    IEnumerator DoFadeIn(Text target)
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

    IEnumerator DoFadeOut(Text target)
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
