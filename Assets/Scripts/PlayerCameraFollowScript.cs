using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerCameraFollowScript : MonoBehaviour
{
    public Vector3 cameraOffset;
    public Vector3 cameraRotation;
    public float cameraRotationSpeed;

    private enum CameraOrientation
    {
        BACK,
        LEFT,
        FRONT,
        RIGHT
    }

    private CameraOrientation currentOrientation = CameraOrientation.BACK;

    

    private GameObject snowBall;
    // Start is called before the first frame update
    void Start()
    {
        snowBall = GameObject.FindGameObjectWithTag("Snowball");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(ChangeCameraOrientation());
        }

        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = snowBall.transform.position + cameraOffset;
        transform.LookAt(snowBall.transform);
    }

    public void RotateCamera()
    {
        StartCoroutine(ChangeCameraOrientation());
    }

    public void RotateCameraReverse()
    {
        
    }

    IEnumerator ChangeCameraOrientation()
    {
        float newX = GetNewX();
        float newZ = GetNewZ();
        currentOrientation = GetNextOrientation();
       
        for (int i = 0; i < 200; i++)
        {
            cameraOffset = cameraOffset + new Vector3(newX, 0, newZ);
            yield return new WaitForSeconds(0.01f);
        }
    }

    float GetNewX()
    {
        float newX = 0;
        switch (currentOrientation)
        {
            case CameraOrientation.BACK:
            case CameraOrientation.RIGHT:
                newX = -0.1f;
                break;
            case CameraOrientation.LEFT:
            case CameraOrientation.FRONT:
                newX = 0.1f;
                break;
        }

        return newX;
    }

    float GetNewZ()
    {
        float newZ = 0;
        switch (currentOrientation)
        {
            case CameraOrientation.BACK:
            case CameraOrientation.LEFT:
                newZ = 0.1f;
                break;
            case CameraOrientation.FRONT:
            case CameraOrientation.RIGHT:
                newZ = -0.1f;
                break;
        }

        return newZ;
    }

    CameraOrientation GetNextOrientation()
    {
        CameraOrientation newOrientation = CameraOrientation.BACK;
        switch (currentOrientation)
        {
            case CameraOrientation.BACK:
                newOrientation = CameraOrientation.LEFT;
                break;
            case CameraOrientation.LEFT:
                newOrientation = CameraOrientation.FRONT;
                break;
            case CameraOrientation.FRONT:
                newOrientation = CameraOrientation.RIGHT;
                break;
            default:
                newOrientation = CameraOrientation.BACK;
                break;
        }

        return newOrientation;
    }

}
