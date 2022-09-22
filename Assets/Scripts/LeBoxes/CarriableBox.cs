using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableBox : MonoBehaviour
{
    GameObject carryPoint;
    PressButtonToInteract pbti;
    //MeshCollider mc;
    Rigidbody rb;
    public bool beingCarried;
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
        if(Input.GetKeyDown(KeyCode.F))
        {
            setOff();
        }
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
        beingCarried = true;
        pbti.enabled = false;
        //mc.enabled = false;
        rb.useGravity = false;
        gameObject.tag = "Carried";
        cbgm.activate();
    }
    void setOff()
    {
        beingCarried = false;
        pbti.enabled = true;
        //mc.enabled = true;
        rb.useGravity = true;
        gameObject.tag = "Event";
        cbgm.deactivate();
        if(inPosition == true)
        {
            gameObject.transform.position = potencial.transform.position;
            Destroy(potencial);
            this.enabled = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        setOff();
    }
}
