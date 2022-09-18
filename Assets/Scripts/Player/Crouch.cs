using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public float colliderCrouchHeight, colliderCrouchAltitude;
    public float controllerCrouchHeight, controllerCrouchAltitude;
    float startingConHeight, startingConAltitude, startingColHeight, startingColAltitude;
    CharacterController ccon;
    CapsuleCollider ccol;
    public bool isCrouched;

    void Start()
    {
        ccon = gameObject.GetComponent<CharacterController>();
        startingConHeight = ccon.height;
        startingConAltitude = ccon.center.y;

        ccol = gameObject.GetComponent<CapsuleCollider>();
        startingColHeight = ccol.height;
        startingColAltitude = ccol.center.y;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouched == false)
            {
                ccon.height = controllerCrouchHeight;
                ccon.center = new Vector3(0, controllerCrouchAltitude, 0);

                ccol.height = colliderCrouchHeight;
                ccol.center = new Vector3(0, colliderCrouchAltitude, 0);

                isCrouched = true;
            }
            else
            {
                ccon.height = startingConHeight;
                ccon.center = new Vector3(0, startingConAltitude, 0);

                ccol.height = startingColHeight;
                ccol.center = new Vector3(0, startingColAltitude, 0);

                isCrouched = false;
            }
        }
    }
}
