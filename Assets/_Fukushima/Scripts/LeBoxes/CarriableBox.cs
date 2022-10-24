using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableBox : MonoBehaviour
{
    GameObject carryPoint;
    PressButtonToInteract pbti;
    MeshCollider mc;
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
        mc = gameObject.GetComponent<MeshCollider>();
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
            mc.enabled = false;
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
            pm._isCarrying = false;
            pm.carryingToIdle = true;
            gameObject.tag = "Event";
            mc.enabled = true;
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
