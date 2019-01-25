using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollowScript : MonoBehaviour
{
    public Vector3 cameraOffset;

    private GameObject snowBall;
    // Start is called before the first frame update
    void Start()
    {
        snowBall = GameObject.FindGameObjectWithTag("Snowball");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = snowBall.transform.position + cameraOffset;
    }
}
