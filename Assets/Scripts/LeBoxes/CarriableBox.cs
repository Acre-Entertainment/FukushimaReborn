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
    public float XPositionOffset, YPositionOffset, ZPositionOffset, XRotationOffset, YRotationOffset, ZRotationOffset;

    PlayerMovement pm;
    float startingJump;

    void Start()
    {
        carryPoint = GameObject.FindGameObjectWithTag("CarryPoint");
        pbti = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
        //mc = gameObject.GetComponent<MeshCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        cbgm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<CarriableBoxGM>();

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
            gameObject.transform.position = new Vector3(carryPoint.transform.position.x + XPositionOffset, carryPoint.transform.position.y + YPositionOffset, carryPoint.transform.position.z + ZPositionOffset);
            gameObject.transform.eulerAngles = new Vector3(carryPoint.transform.eulerAngles.x + XRotationOffset, carryPoint.transform.eulerAngles.y + YRotationOffset, carryPoint.transform.eulerAngles.z + ZRotationOffset);
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
            gameObject.tag = "Carried";
            cbgm.activate();

            frameBuffer = true;
            //Debug.Log("on");
        }
    }
    void setOff()
    {
        if(frameBuffer == false && beingCarried == true)
        {
            beingCarried = false;
            pbti.enabled = true;
            rb.useGravity = true;
            pm._jump = startingJump;
            gameObject.tag = "Event";
            cbgm.deactivate();
            if(inPosition == true)
            {
                gameObject.transform.position = potencial.transform.position;
                Destroy(potencial);
                this.enabled = false;
            }

            frameBuffer = true;
            //Debug.Log("off");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //setOff();
    }
}
