using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("UI Stuff")]
    public GameObject gameOverScreen;

    public float speed = 1.0f;
    private Rigidbody rb;
    private int pickupCount;
    private Timer timer;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    CameraController cameraController;
  

   
    void Start()
    {
        // Gets the rigidbody component attached to this gameObject  
        rb = GetComponent<Rigidbody>();
        //Gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Run the check pickups function
        CheckPickups();
        //Gets the timer object 
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //Game over screen code
        gameOverScreen.SetActive(false);
        //Reset point code
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;

        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (resetting)
            return;
        
        //Store the horizontal axis value in a float 
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the horizontal axis value in a float 
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new Vector 3 based on the horizontal and vertical values 
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to our rigidbody from our movement vector * speed variable 
        rb.AddForce(movement * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, 1000, 0));
        }

        if(cameraController.cameraStyle == CameraStyle.Free)
        {
            //rotates player to the direction of camera
            transform.eulerAngles = Camera.main.transform.eulerAngles;
            //translates the input vectors into coordinates 
            movement = transform.TransformDirection(movement);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            //Destroy the collided object 
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount--;
            //Run the check pickups function
            CheckPickups();
        }
    }

    private void CheckPickups()
    {
        
        print("Pickups left: " + pickupCount);
        if(pickupCount == 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        timer.StopTimer();
        gameOverScreen.SetActive(true);
        print("Yay! You Win. Your Time Was " + timer.GetTime().ToString("F2"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }

}
