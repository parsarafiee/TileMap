using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMove : MonoBehaviour
{
    public GameObject ball;

    public Rigidbody2D rb;
    public Transform headOfSpaceShip;
    public float speedForward;
    public float speedSide;
    public BoxCollider2D landingCollider;

    public GameObject moveSpriteFire;

    public float angleToLand;
    public float landingSpeedDesire = 0.3f;

    public Vector2 accelerationscale;

    float landSpeed;
    bool landed = false;
    bool acceleratin;
    float accelerationTime=0;
    float maxTime = 3;

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
            acceleratin = true; 

            rb.AddForce(this.transform.up * speedForward * Time.deltaTime, ForceMode2D.Force);
        }
        else 
        {
            acceleratin = false;
            //moveSpriteFire.SetActive(false);
            //moveSpriteFire.transform.localScale = new Vector3(1, 1, 1);
            //accelerationTime = 0;
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
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (acceleratin)
        {
            accelerationTime += Time.deltaTime/ maxTime;
            float fireSpriteY = Mathf.Lerp(accelerationscale.x, accelerationscale.y, accelerationTime);
            moveSpriteFire.SetActive(true);
            Debug.Log(accelerationTime);
            moveSpriteFire.transform.localScale = new Vector3(0.5f,1 * fireSpriteY / 2, 1);

            if (accelerationTime > 1)
            {
                accelerationTime = 1;
            }
        }
        if (!acceleratin)
        {
            accelerationTime -= Time.deltaTime;
            float fireSpriteY = Mathf.Lerp(accelerationscale.x, accelerationscale.y, accelerationTime);
            moveSpriteFire.SetActive(true);
            Debug.Log(accelerationTime);
            moveSpriteFire.transform.localScale = new Vector3(0.5f, 1 * fireSpriteY / 2, 1);
            if (accelerationTime <0)
            {
                accelerationTime = 0;
            }
        }

        if (landed)
        {
            float angle = Vector3.Angle(Vector3.right, this.transform.right);
            Debug.Log(angle);
            if (angle > -angleToLand && angle < angleToLand && landSpeed  < landingSpeedDesire)
            {
                //Debug.Log("you Won");
                landed = false;
                landSpeed = 0;

            }
            else
            {
              //  Debug.Log("lose");
                landed = false;
                landSpeed = 0;
            }
        }

    }


    void Fire()
    {
        Debug.Log(headOfSpaceShip.transform.up);
        RaycastHit2D raycast = Physics2D.Raycast(headOfSpaceShip.position, headOfSpaceShip.transform.up);
        //Debug.DrawLine(headOfSpaceShip.position, raycast.point);
        Debug.Log(raycast.collider.name);
        GameObject b = Instantiate(ball, raycast.point, Quaternion.identity);
        //float reyDistance = Vector3.Distance(headOfSpaceShip.position, raycast.point);
        //Debug.Log(reyDistance);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        landSpeed = collision.relativeVelocity.y;
      ///  Debug.Log(landSpeed);
        if (collision.otherCollider == landingCollider)
        {
            landed = true;
        }
    }
}
