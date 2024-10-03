using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;
    private int pickupCount;
  

   
    void Start()
    {
        // Gets the rigidbody component attached to this gameObject  
        rb = GetComponent<Rigidbody>();
        //Gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Run the check pickups function
        CheckPickups();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        print("Yay! You Win");
    }

}
