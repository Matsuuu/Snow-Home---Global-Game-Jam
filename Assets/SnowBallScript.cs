using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallScript : MonoBehaviour
{

    private Rigidbody rb;
    public float ballSizeIncreaseIncrement;

    public int movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }

    public void IncreaseBallSize()
    {
        Vector3 oldScale = transform.localScale;
        transform.localScale = new Vector3(oldScale.x + ballSizeIncreaseIncrement,
            oldScale.y + ballSizeIncreaseIncrement,
            oldScale.z + ballSizeIncreaseIncrement);
    }

    void MoveBall()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizInput, 0, vertInput);
        rb.AddForce(movement * movementSpeed);
    }
}
