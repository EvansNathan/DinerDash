using System.Collections;
using System.Collections.Generic;
using Menus_UI;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public float bounceFloor;
    public float bounceCelling;
    public int bounceMultiplier;
    public int moveSpeed;
    private Rigidbody rb;
    public int numBounces;
    private float timeSinceLast = 0.0f;
    bool test=false;
    private Vector3 rotationY;
    private float rotateSpeed = 10;
    private float y;
    bool dualMove = false;
    private float dashCD = 0.0f;
    public bool dashing = false;
    private bool dashAvailable = false;

    private AudioSource boingAudio;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity.y = 2;
        boingAudio = GetComponent<AudioSource>();
        y = 999;
        rb.mass = 0;
    }

    IEnumerator BounceWait()
    {
        yield return new WaitForSeconds(3/8);
        boingAudio.pitch = Random.Range(0.4f, 2.0f);
        
        boingAudio.Play();
    }

    IEnumerator dash()
    {
        Debug.Log("Dash");
        moveSpeed = 16;
        yield return new WaitForSeconds(0.10f);
        moveSpeed = 10;
        yield return new WaitForSeconds(0.14f);
        moveSpeed = 4;
    }

    IEnumerator DashAnim()
    {
        dashing = true;
        float temp = y;
        
        for(int i=0;i<15;i++)
        {
            temp += 22.5f;
            rotationY.Set(0f, temp, 0f);
            yield return new WaitForSeconds(0.015f);

        }
        dashing = false;


    }
    
    void Update()
    {
        dashCD -= Time.deltaTime;
        if(dashCD <= 0)
        {
            dashAvailable = true;
            UIMgr.Instance.dashGroup.SetActive(true);
        }
        else {
            dashAvailable = false;
            UIMgr.Instance.dashGroup.SetActive(false);
        }
        
        timeSinceLast += Time.deltaTime;
        if(timeSinceLast>1)
        {
            //Debug.Log(numBounces/timeSinceLast);
            timeSinceLast = 0;
            numBounces = 0;
        }
        

        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            velocity.x = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            velocity.x = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity.z = moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            velocity.z = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity.z = -moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            velocity.z = 0;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(dashCD <=0)
            {
                dashCD = 3;
                //rb.AddRelativeForce(Vector3.left * 20000);
                StartCoroutine(dash());
                StartCoroutine(DashAnim());

            }
            
        }

        if (Input.GetKey(KeyCode.Period))
        {
            
           // rb.AddRelativeForce(Vector3.left * 1000);
            
        }

        if(velocity.x!=0 || velocity.z!=0)
        {
            bounceMultiplier = 3;
        }
        else
        {
            bounceMultiplier = 1;
        }

        if(rb.position.y <= bounceFloor)
        {
            
            if(test==false)
            {
                numBounces++;
            }
            test = true;

            //Debug.Log("rebound");
            velocity.y = 2*bounceMultiplier;

            //yield return new WaitForSeconds(3 / 10);
            StartCoroutine(BounceWait());
        }
        if (rb.position.y >= bounceCelling)
        {
            test = false;
            velocity.y = -2*bounceMultiplier;
        }

        //position = position + velocity * Time.deltaTime;
        //transform.localPosition = position;
        //rb.position += velocity*Time.deltaTime;
        rb.velocity = velocity;

        dualMove = false;
        
        if (velocity.x < 0)
        {
            y = 0;
            dualMove = true;
        }
        else if(velocity.x > 0)
        {
            y = 180;
            dualMove = true;
        }

        if(velocity.z < 0)
        {
            if(y==0)
            {
                y -= 45;
            }
            else if(y==180)
            {
                y += 45;
            }
            else
            {
                y = 270;
            }
        }
        else if (velocity.z > 0)
        {
            if (y == 0)
            {
                y += 45;
            }
            else if (y == 180)
            {
                y -= 45;
            }
            else
            {
                y = 90;
            }

        }
        
        if(!dashing)
        {
            rotationY.Set(0f, y, 0f);
        }

        Quaternion deltaRotation = Quaternion.Euler(rotationY);
        //rb.rotation = deltaRotation;
        rb.MoveRotation(deltaRotation);


    }
}
