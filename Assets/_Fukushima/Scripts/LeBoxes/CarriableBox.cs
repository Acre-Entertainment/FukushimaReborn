using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarriableBox : MonoBehaviour
{
    GameObject carryPoint;
    PressButtonToInteract pbti;
    MeshCollider mc;
    Rigidbody rb;
    public bool beingCarried, frameBuffer;
    public float XPositionOffset, YPositionOffset, ZPositionOffset, XRotationOffset, YRotationOffset, ZRotationOffset, releaseDistance;

    PlayerMovement pm;
    float startingJump;

    public bool isInEventArea1;
    public bool isInEventArea2;
    public string triggerAreaTag1;
    public string triggerAreaTag2;
    public UnityEvent area1Event;
    public UnityEvent area2Event;

    void Start()
    {
        carryPoint = GameObject.FindGameObjectWithTag("CarryPoint");
        pbti = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
        mc = gameObject.GetComponent<MeshCollider>();
        rb = gameObject.GetComponent<Rigidbody>();

        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        startingJump = pm._jump;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && beingCarried == true)
        {
            setOff();
            //Debug.Log("setOff");
        }
        frameBuffer = false;

        if (beingCarried == true)
        {
            carryPoint.transform.localPosition = new Vector3(XPositionOffset, YPositionOffset, ZPositionOffset + 1.5f);
            carryPoint.transform.localEulerAngles = new Vector3(XRotationOffset, YRotationOffset, ZPositionOffset);
            gameObject.transform.position = new Vector3(carryPoint.transform.position.x, carryPoint.transform.position.y, carryPoint.transform.position.z);
            gameObject.transform.eulerAngles = new Vector3(carryPoint.transform.eulerAngles.x, carryPoint.transform.eulerAngles.y, carryPoint.transform.eulerAngles.z);
        }
    }
    public void setOn()
    {
        if(frameBuffer == false && beingCarried == false)
        {
            beingCarried = true;
            pbti.enabled = true;
            pbti.hasEvent = false;
            rb.useGravity = false;
            pm._jump = 0;
            pm._isCarrying = true;
            gameObject.tag = "Carried";
            mc.isTrigger = true;

            frameBuffer = true;
            //Debug.Log("on");
        }
    }
    void setOff()
    {
        if(frameBuffer == false && beingCarried == true)
        {
            carryPoint.transform.localPosition = new Vector3(XPositionOffset, YPositionOffset, ZPositionOffset + 1.5f + releaseDistance);
            carryPoint.transform.localEulerAngles = new Vector3(XRotationOffset, YRotationOffset, ZPositionOffset);
            gameObject.transform.position = new Vector3(carryPoint.transform.position.x, carryPoint.transform.position.y, carryPoint.transform.position.z);
            gameObject.transform.eulerAngles = new Vector3(carryPoint.transform.eulerAngles.x, carryPoint.transform.eulerAngles.y, carryPoint.transform.eulerAngles.z);



            beingCarried = false;
            pbti.enabled = true;
            rb.useGravity = true;
            pm._jump = startingJump;
            pm._isCarrying = false;
            pm.carryingToIdle = true;
            gameObject.tag = "Event";
            mc.isTrigger = false;

            frameBuffer = true;
            //Debug.Log("off");
        }
    }

    void OnTriggerEnter(Collision other)
    {
        if(other.gameObject.tag == triggerAreaTag1)
        {
            isInEventArea1 = true;
        }
        if(other.gameObject.tag == triggerAreaTag2)
        {
            isInEventArea2 = true;
        }
    }
    void OnTriggerExit(Collision other)
    {
        if(other.gameObject.tag == triggerAreaTag1)
        {
            isInEventArea1 = false;
        }
        if(other.gameObject.tag == triggerAreaTag2)
        {
            isInEventArea2 = false;
        }
    }
}
