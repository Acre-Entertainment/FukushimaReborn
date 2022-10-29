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
                if(pbo.GetComponent<PushableBox>() != null)
                {
                    if(pbo.GetComponent<PushableBox>().calculateDistanceFromPlayer(gameObject))
                    {
                        pbo.Event.Invoke();
                        selectedText.SetText("");
                    }
                }
                else
                {
                    pbo.Event.Invoke();
                    selectedText.SetText("");
                }
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

            //if(other.GetComponent<InteractText>() != null)
            //{
            //    selectedText.SetText(other.GetComponent<InteractText>().text);
            //}

            if(other.GetComponent<PushableBox>() != null)
            {
                if(other.GetComponent<PushableBox>().calculateDistanceFromPlayer(gameObject))
                {
                    selectedText.SetText("[F]");
                }
                else
                {
                    selectedText.SetText("");
                }
            }
            else
            {
                selectedText.SetText("[F]");
            }
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

            //if (other.GetComponent<InteractText>() != null)
            //{
            // selectedText.SetText(other.GetComponent<InteractText>().text);
            //}

            if(other.GetComponent<PushableBox>() != null)
            {
                if(other.GetComponent<PushableBox>().calculateDistanceFromPlayer(gameObject))
                {
                    selectedText.SetText("[F]");
                }
                else
                {
                    selectedText.SetText("");
                }
            }
            else
            {
                selectedText.SetText("[F]");
            }


            if(other.GetComponent<Hide_Object>() != null)
            {
                if (other.GetComponent<Hide_Object>().open == false)
                {
                    if (Hide_Character._canChangeThePosition)
                    {
                        other.GetComponent<Hide_Object>().PlayerPosition();
                    }
                }
                else if (other.GetComponent<Hide_Object>().open == true)
                {
                    if (Hide_Character._canChangeThePosition)
                    {
                        other.GetComponent<Hide_Object>().Close();
                    }
                }
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
