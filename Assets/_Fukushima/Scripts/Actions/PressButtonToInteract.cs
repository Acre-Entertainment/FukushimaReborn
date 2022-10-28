using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButtonToInteract : MonoBehaviour
{
    PressButtonToInteract_Object pbo;
    public bool hasPressed;
    public bool hasEvent;
    public GameObject interactingGO;
    [SerializeField] TMPro.TextMeshProUGUI selectedText;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(hasPressed == false && hasEvent == true)
            {
                pbo.Event.Invoke();
                selectedText.SetText("");
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
            interactingGO = other.gameObject;
            pbo = interactingGO.GetComponent<PressButtonToInteract_Object>();
            hasEvent = true;

            if(other.GetComponent<InteractText>() != null)
            {
                selectedText.SetText(other.GetComponent<InteractText>().text);
            }
            else
            {
                selectedText.SetText("[F]");
            }
        }
        else
        {
            selectedText.SetText("");
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

            if (other.GetComponent<InteractText>() != null)
            {
                selectedText.SetText(other.GetComponent<InteractText>().text);
            }
            else
            {
                selectedText.SetText("[F]");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Event")
        {
            hasEvent = false;
            selectedText.SetText("");
        }
    }
}
