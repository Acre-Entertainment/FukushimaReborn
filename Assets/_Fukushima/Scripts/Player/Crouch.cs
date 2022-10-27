using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public float colliderCrouchHeight, colliderCrouchAltitude;
    public float controllerCrouchHeight, controllerCrouchAltitude;
    public float collisionHeight, collisionCrouchAltitude;
    public float angleFix;
    float startingConHeight, startingConAltitude, startingColHeight, startingColAltitude, startingJump, startingTrigHeight, startingTrigAltitude;
    CharacterController ccon;
    CapsuleCollider ccol;
    PlayerMovement pm;
    public bool isCrouched;
    bool ignoreFrame;
    public GameObject crouchInspector;
    public CapsuleCollider triggerCollision;

    PressButtonToInteract pressButtonToInteract;

    void Start()
    {
        ccon = gameObject.GetComponent<CharacterController>();
        startingConHeight = ccon.height;
        startingConAltitude = ccon.center.y;

        ccol = gameObject.GetComponent<CapsuleCollider>();
        startingColHeight = ccol.height;
        startingColAltitude = ccol.center.y;

        startingTrigHeight = triggerCollision.height;
        startingTrigAltitude = triggerCollision.center.y;

        pm = gameObject.GetComponent<PlayerMovement>();
        startingJump = pm._jump;

        pressButtonToInteract = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
    }
    void Update()
    {
        //if(isCrouched == true)
        //{
        //    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + angleFix, gameObject.transform.eulerAngles.z);
        //}

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouched == false && pressButtonToInteract.enabled == true)
            {
                ccon.height = controllerCrouchHeight;
                ccon.center = new Vector3(0, controllerCrouchAltitude, 0);

                ccol.height = colliderCrouchHeight;
                ccol.center = new Vector3(0, colliderCrouchAltitude, 0);

                triggerCollision.height = collisionHeight;
                triggerCollision.center = new Vector3(0, colliderCrouchAltitude, 0);

                pressButtonToInteract.enabled = false;
                pm._jump = 0;
                isCrouched = true;

                ignoreFrame = true;
            }
            if (isCrouched == true && crouchInspector.GetComponent<CrouchInspector>().spaceIsOccupied == false && ignoreFrame == false)
            {
                ccon.height = startingConHeight;
                ccon.center = new Vector3(0, startingConAltitude, 0);

                ccol.height = startingColHeight;
                ccol.center = new Vector3(0, startingColAltitude, 0);

                triggerCollision.height = startingTrigHeight;
                triggerCollision.center = new Vector3(0, startingTrigAltitude, 0);

                pressButtonToInteract.enabled = true;
                pm._jump = startingJump;
                isCrouched = false;
                pm.crouchToIdle = true;
            }
            ignoreFrame = false;
        }
    }
}
