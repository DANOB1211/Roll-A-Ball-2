using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject myPartner;
    public bool canTeleport = true;

    private void Start()
    {
        canTeleport = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && canTeleport)
        {
            myPartner.GetComponent<Teleporter>().canTeleport = false;
            // Offset the y pos so it does not move into ground
            Vector3 endPos = new Vector3(myPartner.transform.position.x, myPartner.transform.position.y+1, myPartner.transform.position.z);
            other.transform.position = endPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !canTeleport)
            canTeleport = true;
    }
}
