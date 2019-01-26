using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallScript : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject camera;
    public float ballSizeIncreaseIncrement;
    public int maxScale;
    public float maxVelocity;

    public float movementSpeed;
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
        if (transform.localScale.x < maxScale)
        {
            StartCoroutine(LerpBallSize());
        }
    }

    private IEnumerator LerpBallSize()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 oldScale = transform.localScale;
        transform.localScale = new Vector3(oldScale.x + ballSizeIncreaseIncrement,
            oldScale.y + ballSizeIncreaseIncrement,
            oldScale.z + ballSizeIncreaseIncrement);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SlowDown()
    {
        movementSpeed = 0.1f;
        rb.velocity = rb.velocity * 0.25f;
    }

    void MoveBall()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizInput, 0, vertInput);
        
        Vector3 relativeMovement = camera.transform.TransformVector(movement);
        rb.AddForce(relativeMovement * movementSpeed);
        if (rb.velocity.sqrMagnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}
