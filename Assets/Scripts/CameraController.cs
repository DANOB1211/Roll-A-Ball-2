using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraStyle {Fixed,Free}

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public CameraStyle cameraStyle;
    public Transform pivot;
    public float rotationSpeed = 1f;

    private Vector3 offset;
    private Vector3 pivotOffset;


    // Start is called before the first frame update
    void Start()
    {
        //Set the offset of the camera based on the players position
        offset = transform.position - player.transform.position;

        //The offset of the pivot from the player
        pivotOffset = pivot.position - player.transform.position;

        //the offset from the player
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //If we are using the fixed camera mode
        if (cameraStyle == CameraStyle.Fixed)
        {
            //Set camera position to be the players plus offset
            transform.position = player.transform.position + offset;
        }

        //if we are using free camera mode
        if (cameraStyle == CameraStyle.Free)
        {
            //Make pivot position follow player
            pivot.transform.position = player.transform.position + pivotOffset;
            //Work out angle from the mouse input as a quaternion
            Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            //Modify the offset by the turn angle
            offset = turnAngle * offset;
            //Set camera position to that of the pivot point plus the offset 
            transform.position = pivot.transform.position + offset;
            //make the camera look at the pivot 
            transform.LookAt(pivot);
        }
    }
}
