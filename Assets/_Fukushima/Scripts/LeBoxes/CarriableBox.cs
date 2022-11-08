using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarriableBox : MonoBehaviour
{
    GameObject carryPoint;
    PressButtonToInteract pbti;
    GameObject interactArea;
    MeshCollider mc;
    Rigidbody rb;
    public bool beingCarried, frameBuffer;
    public float XPositionOffset, YPositionOffset, ZPositionOffset, XRotationOffset, YRotationOffset, ZRotationOffset, releaseDistance;

    public bool hasCooldown;
    public float CooldownTime = 0.5f;

    PlayerMovement pm;
    float startingJump;
    CrouchInspector dropInspector;

    public bool isInEventArea1;
    public bool isInEventArea2;
    public bool isInEventArea3;
    public string triggerAreaTag1;
    public string triggerAreaTag2;
    public string triggerAreaTag3;
    public UnityEvent area1Event;
    public UnityEvent area2Event;
    public UnityEvent area3Event;

    TMPro.TextMeshProUGUI selectedText;

    void Start()
    {
        carryPoint = GameObject.FindGameObjectWithTag("CarryPoint");
        interactArea = GameObject.FindGameObjectWithTag("InteractArea");
        pbti = interactArea.GetComponent<PressButtonToInteract>();
        mc = gameObject.GetComponent<MeshCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        dropInspector = GameObject.FindGameObjectWithTag("DropInspector").GetComponent<CrouchInspector>();
        startingJump = pm._jump;
        selectedText = GameObject.FindGameObjectWithTag("SelectedText").GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && beingCarried == true)
        {
            setOff();
        }
        frameBuffer = false;

        if (beingCarried == true)
        {
            carryPoint.transform.localPosition = new Vector3(XPositionOffset, YPositionOffset, ZPositionOffset + 1.5f);
            carryPoint.transform.localEulerAngles = new Vector3(XRotationOffset, YRotationOffset, ZPositionOffset);
            gameObject.transform.position = new Vector3(carryPoint.transform.position.x, carryPoint.transform.position.y, carryPoint.transform.position.z);
            gameObject.transform.eulerAngles = new Vector3(carryPoint.transform.eulerAngles.x, carryPoint.transform.eulerAngles.y, carryPoint.transform.eulerAngles.z);
        }

        if (PlayerMovement.custscene)
        {
            setOff();
        }
    }
    public void setOn()
    {
        if(frameBuffer == false && beingCarried == false && !PlayerMovement.custscene)
        {
            beingCarried = true;
            pbti.hasEvent = false;
            interactArea.SetActive(false);
            rb.useGravity = false;
            pm._jump = 0;
            pm._isCarrying = true;
            gameObject.tag = "Carried";
            gameObject.layer = LayerMask.NameToLayer("Ignore Ignore Raycast");
            mc.isTrigger = true;

            frameBuffer = true;
            hasCooldown = true;
            StartCoroutine(Cooldown());
        }
    }
    public void setOff()
    {
        if(frameBuffer == false && hasCooldown == false && beingCarried == true)
        {
            if(isInEventArea1 == false && isInEventArea2 == false && isInEventArea3 == false && dropInspector.spaceIsOccupied == true)
            {
                return;
            }
            carryPoint.transform.localPosition = new Vector3(XPositionOffset, YPositionOffset, ZPositionOffset + 1.5f + releaseDistance);
            carryPoint.transform.localEulerAngles = new Vector3(XRotationOffset, YRotationOffset, ZPositionOffset);
            gameObject.transform.position = new Vector3(carryPoint.transform.position.x, carryPoint.transform.position.y, carryPoint.transform.position.z);
            gameObject.transform.eulerAngles = new Vector3(carryPoint.transform.eulerAngles.x, carryPoint.transform.eulerAngles.y, carryPoint.transform.eulerAngles.z);

            beingCarried = false;
            interactArea.SetActive(true);

            pbti.hasCooldown = true;
            StartCoroutine(pbti.Cooldown());

            rb.useGravity = true;
            pm._jump = startingJump;
            pm._isCarrying = false;
            pm.carryingToIdle = true;
            gameObject.tag = "Event";
            gameObject.layer = LayerMask.NameToLayer("Default");
            mc.isTrigger = false;

            frameBuffer = true;

            StartCoroutine(pbti.Cooldown());
            if(isInEventArea1 == true)
            {
                pbti.hasCooldown = false;
                area1Event.Invoke();
                selectedText.SetText("");
            }
            if(isInEventArea2 == true)
            {
                pbti.hasCooldown = false;
                area2Event.Invoke();
                selectedText.SetText("");
            }
            if(isInEventArea3 == true)
            {
                pbti.hasCooldown = false;
                area3Event.Invoke();
                selectedText.SetText("");
            }
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
        hasCooldown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggerAreaTag1)
        {
            isInEventArea1 = true;
            selectedText.SetText("[F]");
        }
        if(other.gameObject.tag == triggerAreaTag2)
        {
            isInEventArea2 = true;
            selectedText.SetText("[F]");
        }
        if(other.gameObject.tag == triggerAreaTag3)
        {
            isInEventArea3 = true;
            selectedText.SetText("[F]");
        }
    }
    void OnTriggerStay(Collider other)
    {
         if(other.gameObject.tag == triggerAreaTag1 || other.gameObject.tag == triggerAreaTag2 || other.gameObject.tag == triggerAreaTag3)
         {
            selectedText.SetText("[F]");
         }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == triggerAreaTag1)
        {
            isInEventArea1 = false;
            selectedText.SetText("");
        }
        if(other.gameObject.tag == triggerAreaTag2)
        {
            isInEventArea2 = false;
            selectedText.SetText("");
        }
        if(other.gameObject.tag == triggerAreaTag3)
        {
            isInEventArea3 = false;
            selectedText.SetText("");
        }
    }
}
