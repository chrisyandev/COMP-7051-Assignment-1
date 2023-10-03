using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 10.0f;

    private Rigidbody rb;
    private Vector3 lastFrameVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * m_Speed;
    }

    void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player Wall"))
        {
            transform.position = Vector3.zero;
            rb.velocity = -transform.forward * m_Speed;
        }
        else if (collision.gameObject.CompareTag("AI Wall"))
        {
            transform.position = Vector3.zero;
            rb.velocity = transform.forward * m_Speed;
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

    public void SetSpeed(float speed) { // example cmd for console
        this.m_Speed = speed;
    }
}
