using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButtonToInteract : MonoBehaviour
{
    PressButtonToInteract_Object pbo;
    public float CooldownTime;
    public bool hasCooldown;
    public bool hasPressed;
    public bool hasEvent;
    public GameObject interactingGO;
    [SerializeField] TMPro.TextMeshProUGUI selectedText;
    [SerializeField] CrouchInspector itenDropInspector;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("PBTI 1");
            if(hasPressed == false && hasEvent == true && hasCooldown == false)
            {
                //Debug.Log("PBTI 2");
                if(pbo.GetComponent<PushableBox>() != null)
                {
                    //Debug.Log("PBTI 3 P");
                    if(pbo.GetComponent<PushableBox>().calculateDistanceFromPlayer(gameObject))
                    {
                        //Debug.Log("PBTI 4 P");
                        pbo.Event.Invoke();
                        selectedText.SetText("");
                        hasCooldown = true;
                        StartCoroutine(Cooldown());
                    }
                }
                else if(pbo.GetComponent<CarriableBox>() != null)
                {
                    //Debug.Log("PBTI 3 C");
                    itenDropInspector.spaceIsOccupied = false;
                    pbo.Event.Invoke();
                    selectedText.SetText("");
                }
                else
                {
                    //Debug.Log("PBTI 3 E");
                    pbo.Event.Invoke();
                    selectedText.SetText("");
                    hasCooldown = true;
                    StartCoroutine(Cooldown());
                }
            }
            hasPressed = true;
        }

        hasPressed = false;
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
    public IEnumerator Cooldown()
    {
        //Debug.Log("CooldownStarted");
        yield return new WaitForSeconds(CooldownTime);
        //Debug.Log("CooldownNearEnd");
        hasCooldown = false;
        //Debug.Log("CooldownEnd");
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
