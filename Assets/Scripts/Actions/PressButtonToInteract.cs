using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButtonToInteract : MonoBehaviour
{
    PressButtonToInteract_Object pbo;
    bool hasPressed;
    bool hasEvent;
    GameObject interactingGO;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(hasPressed == false && hasEvent == true)
            {
                pbo.Event.Invoke();
            }
            hasPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            hasPressed = false;
        }
    }

    public void callEvent()
    {
        if(hasEvent == true)
        {
            pbo.Event.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Event")
        {
            Debug.Log("AAAAAAAAAAAAA");
            interactingGO = other.gameObject;
            pbo = interactingGO.GetComponent<PressButtonToInteract_Object>();
            hasEvent = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Event")
        {
            if(interactingGO != other.gameObject)
            {
                interactingGO = other.gameObject;
                pbo = interactingGO.GetComponent<PressButtonToInteract_Object>();
                hasEvent = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Event")
        {
            hasEvent = false;
        }
    }
}
