using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 15.0f;

    private Rigidbody rb;
    private Vector3 lastFrameVelocity;

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

            // Randomly get  -1 or 1. Ball randomly flies in either P1 or P2 Direction. 
            int randomDirection = Random.Range( 0, 2 ) * 2 - 1;
            rb.velocity = transform.forward * m_Speed * randomDirection;
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
            lockoutTime = Time.time + 1.5f;
            transform.position = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            canBallMove = false;
        }
        else if (collision.gameObject.CompareTag("AI Wall"))
        {
            // Update P1 Score since ball hit Player2 Goal
            GameManager.UpdateScore( 1 );
            lockoutTime = Time.time + 1.5f;
            transform.position = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
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

    public void SetSpeed(float speed) { // example cmd for console
        this.m_Speed = speed;
        Debug.Log("BALL SPEED SET!!!! BATCHEST: " + speed);
    }
}
