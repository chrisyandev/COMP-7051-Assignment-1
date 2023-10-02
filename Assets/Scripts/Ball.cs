using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 15.0f;

    private Rigidbody rb;
    private Vector3 lastFrameVelocity;

    private Coroutine activeCorutine;
    private float lockoutTime;
    private bool canBallMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetStartVelocity();
    }

    void Update()
    {
        if ( canBallMove ) 
        {
            lastFrameVelocity = rb.velocity;
        }
        
        if (!canBallMove && Time.time >= lockoutTime )
        {
            canBallMove = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = lastFrameVelocity ;
        }
    }

    private void SetStartVelocity()
    {
        rb.velocity = transform.forward * m_Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player Wall"))
        {
            // Update P2 Score since ball hit Player1 Goal
            GameManager.UpdateScore( 2 );
            lockoutTime = Time.time + 3.0f;
            transform.position = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            //rb.velocity = transform.forward * m_Speed;
            canBallMove = false;
        }
        else if (collision.gameObject.CompareTag("AI Wall"))
        {
            // Update P1 Score since ball hit Player2 Goal
            GameManager.UpdateScore( 1 );
            lockoutTime = Time.time + 3.0f;
            transform.position = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            //rb.velocity = -transform.forward * m_Speed;
            canBallMove = false;
        }
        else
        {
            Bounce(collision);
        }
    }
    

    private void Bounce(Collision collision)
    {
        float speed = lastFrameVelocity.magnitude;
        Vector3 collisionNormal = collision.contacts[0].normal;
        Vector3 bounceDirection = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = bounceDirection * speed;
    }
}
