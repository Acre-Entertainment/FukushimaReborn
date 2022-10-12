using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableBox : MonoBehaviour
{
    GameObject carryPoint;
    PressButtonToInteract pbti;
    //MeshCollider mc;
    Rigidbody rb;
    public bool beingCarried, frameBuffer;
    CarriableBoxGM cbgm;
    public bool inPosition;
    public GameObject potencial;

    void Start()
    {
        carryPoint = GameObject.FindGameObjectWithTag("CarryPoint");
        pbti = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
        //mc = gameObject.GetComponent<MeshCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        cbgm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<CarriableBoxGM>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && beingCarried == true)
        {
            setOff();
            Debug.Log("setOff");
        }
        frameBuffer = false;
    }
    void FixedUpdate()
    {
        if(beingCarried == true)
        {
            gameObject.transform.position = carryPoint.transform.position;
            gameObject.transform.rotation = carryPoint.transform.rotation;
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
            gameObject.tag = "Carried";
            cbgm.activate();

            frameBuffer = true;
            Debug.Log("on");
        }
    }
    void setOff()
    {
        if(frameBuffer == false && beingCarried == true)
        {
            beingCarried = false;
            pbti.enabled = true;
            rb.useGravity = true;
            gameObject.tag = "Event";
            cbgm.deactivate();
            if(inPosition == true)
            {
                gameObject.transform.position = potencial.transform.position;
                Destroy(potencial);
                this.enabled = false;
            }

            frameBuffer = true;
            Debug.Log("off");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //setOff();
    }
}
