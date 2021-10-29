using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMove : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speedForward;
    public float speedSide;
    public BoxCollider2D landingCollider;

    public float angleToLand;
    public float landingSpeedDesire = 0.3f;

    float landSpeed;
    bool landed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        PlayerController();
    }
    void PlayerController()
    {
        if (Input.GetKey(KeyCode.W))
        {

            rb.AddForce(this.transform.up * speedForward * Time.deltaTime, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {

            rb.AddForce(this.transform.up * -speedForward * Time.deltaTime, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {

            rb.AddTorque(-speedSide, ForceMode2D.Force);

        }
        if (Input.GetKey(KeyCode.A))
        {

            rb.AddTorque(speedSide, ForceMode2D.Force);
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (landed)
        {
            float angle = Vector3.Angle(Vector3.right, this.transform.right);
            Debug.Log(angle);
            if (angle > -angleToLand && angle < angleToLand && landSpeed  < landingSpeedDesire)
            {
                Debug.Log("you Won");
                landed = false;
                landSpeed = 0;

            }
            else
            {
                Debug.Log("lose");
                landed = false;
                landSpeed = 0;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        landSpeed = collision.relativeVelocity.y;
        Debug.Log(landSpeed);
        if (collision.otherCollider == landingCollider)
        {
            landed = true;
        }
    }
}
