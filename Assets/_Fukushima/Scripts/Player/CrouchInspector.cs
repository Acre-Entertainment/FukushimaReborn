using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchInspector : MonoBehaviour
{
    public bool spaceIsOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Carried" && other.gameObject.tag != "Player" && other.gameObject.tag != "TriggerCollision" && other.gameObject.tag != "InteractArea")
        {
            spaceIsOccupied = true;
            Debug.Log("this fuck is fault of " + other.gameObject.name);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != "Carried" && other.gameObject.tag != "Player" && other.gameObject.tag != "TriggerCollision" && other.gameObject.tag != "InteractArea")
        {
            spaceIsOccupied = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Carried" && other.gameObject.tag != "Player" && other.gameObject.tag != "TriggerCollision" && other.gameObject.tag != "InteractArea")
        {
            spaceIsOccupied = false;
        }
    }
}
