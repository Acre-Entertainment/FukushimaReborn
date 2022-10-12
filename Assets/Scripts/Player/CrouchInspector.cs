using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchInspector : MonoBehaviour
{
    public bool spaceIsOccupied;

    private void OnTriggerEnter(Collider other)
    {
        spaceIsOccupied = true;
    }
    private void OnTriggerStay(Collider other)
    {
        spaceIsOccupied = true;
    }
    private void OnTriggerExit(Collider other)
    {
        spaceIsOccupied = false;
        Debug.Log("EXIT");
    }
}
