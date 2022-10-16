using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public float colliderCrouchHeight, colliderCrouchAltitude;
    public float controllerCrouchHeight, controllerCrouchAltitude;
    float startingConHeight, startingConAltitude, startingColHeight, startingColAltitude, startingJump;
    CharacterController ccon;
    CapsuleCollider ccol;
    PlayerMovement pm;
    public bool isCrouched;
    bool ignoreFrame;
    public GameObject crouchInspector;

    PressButtonToInteract pressButtonToInteract;

    void Start()
    {
        ccon = gameObject.GetComponent<CharacterController>();
        startingConHeight = ccon.height;
        startingConAltitude = ccon.center.y;

        ccol = gameObject.GetComponent<CapsuleCollider>();
        startingColHeight = ccol.height;
        startingColAltitude = ccol.center.y;

        pm = gameObject.GetComponent<PlayerMovement>();
        startingJump = pm._jump;

        pressButtonToInteract = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouched == false && pressButtonToInteract.enabled == true)
            {
                ccon.height = controllerCrouchHeight;
                ccon.center = new Vector3(0, controllerCrouchAltitude, 0);

                ccol.height = colliderCrouchHeight;
                ccol.center = new Vector3(0, colliderCrouchAltitude, 0);

                pressButtonToInteract.enabled = false;
                isCrouched = true;
                pm._jump = 0;

                ignoreFrame = true;
            }
            if (isCrouched == true && crouchInspector.GetComponent<CrouchInspector>().spaceIsOccupied == false && ignoreFrame == false)
            {
                ccon.height = startingConHeight;
                ccon.center = new Vector3(0, startingConAltitude, 0);

                ccol.height = startingColHeight;
                ccol.center = new Vector3(0, startingColAltitude, 0);

                pressButtonToInteract.enabled = true;
                isCrouched = false;
                pm._jump = startingJump;
            }
            ignoreFrame = false;
        }
    }
}
