using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchInspector : MonoBehaviour
{
    public bool spaceIsOccupied;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entry");
        spaceIsOccupied = true;
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay");
        spaceIsOccupied = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("leave");
        spaceIsOccupied = false;
    }
}
