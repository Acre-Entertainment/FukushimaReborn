using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    GameObject playerObject, parentObject;
    PressButtonToInteract pbti;
    //MeshCollider ms;
    public GameObject anchorX, anchorXminor, anchorZ, anchorZminor;
    public float distanceFromPlayer, XOffset, ZOffset;
    float realDistanceFromPlayer;
    public bool noXMovement, noZmovement;
    bool beingPushedByX, beingPushedByZ;
    float constantX, constantZ;
    int playerFixedRotation;
    Vector3 lastPlayerLocation, currentPlayerLocation, movement, newPosition;
    bool hasParent, ignoreFrame;
    private CharacterController cc;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        cc = playerObject.GetComponent<CharacterController>();
        pbti = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
        //ms = gameObject.GetComponent<MeshCollider>();
        if (gameObject.transform.parent != null)
        {
            parentObject = gameObject.transform.parent.gameObject;
            hasParent = true;
        }
    }
    void FixedUpdate()
    {
        //if (beingPushedByX == true || beingPushedByZ == true)
        //{
        //    if (beingPushedByX == false)
        //    {
        //        gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, playerObject.transform.position.z + realDistanceFromPlayer - ZOffset);
        //        cc.enabled = false;
        //        playerObject.GetComponent<CharacterController>().transform.position = new Vector3(constantX + XOffset, playerObject.transform.position.y, playerObject.transform.position.z);
        //        cc.enabled = true;
        //    }
        //    if (beingPushedByZ == false)
        //    {
        //        gameObject.transform.position = new Vector3(playerObject.transform.position.x + realDistanceFromPlayer - XOffset, gameObject.transform.position.y, constantZ);
        //        cc.enabled = false;
        //        playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, constantZ + ZOffset);
        //        cc.enabled = true;
        //    }
        //}
    }
    void Update()
    {
        if (beingPushedByX == true || beingPushedByZ == true)
        {
            if (beingPushedByX == false)
            {
                gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, playerObject.transform.position.z + realDistanceFromPlayer - ZOffset);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(constantX + XOffset, playerObject.transform.position.y, playerObject.transform.position.z);
                cc.enabled = true;
            }
            if (beingPushedByZ == false)
            {
                gameObject.transform.position = new Vector3(playerObject.transform.position.x + realDistanceFromPlayer - XOffset, gameObject.transform.position.y, constantZ);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, constantZ + ZOffset);
                cc.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && (beingPushedByX == true || beingPushedByZ == true))
        {
            setOff();
        }
        ignoreFrame = false;
    }
    public void setPush()
    {
        //Debug.Log("setPush");
        if (ignoreFrame == false && beingPushedByX == false && beingPushedByZ == false)
        {
            //Debug.Log("setPushTrue");
            ignoreFrame = true;
            if (noXMovement == false && noZmovement == false)
            {
                float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
                float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);
                float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
                float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

                float biggestDistance = Mathf.Max(distanceX, distanceXminor, distanceZ, distanceZminor);

                if (biggestDistance == distanceX) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; realDistanceFromPlayer = distanceFromPlayer; }
                if (biggestDistance == distanceXminor) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; realDistanceFromPlayer = -distanceFromPlayer; }
                if (biggestDistance == distanceZ) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; realDistanceFromPlayer = distanceFromPlayer; }
                if (biggestDistance == distanceZminor) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0; realDistanceFromPlayer = -distanceFromPlayer; }
            }
            if (noXMovement == true && noZmovement == false)
            {
                float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
                float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

                float biggestDistance = Mathf.Max(distanceZ, distanceZminor);

                if (biggestDistance == distanceZ) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; }
                if (biggestDistance == distanceZminor) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0; }
            }
            if (noXMovement == false && noZmovement == true)
            {
                float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
                float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);

                float biggestDistance = Mathf.Max(distanceX, distanceXminor);

                if (biggestDistance == distanceX) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; }
                if (biggestDistance == distanceXminor) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; }
            }
            if (noXMovement == true && noZmovement == true)
            {
                Debug.Log("ERROR: Both movements blocked");
            }

            if (beingPushedByX == false)
            {
                cc.enabled = false;
                playerObject.transform.position = new Vector3(gameObject.transform.position.x + XOffset, playerObject.transform.position.y, gameObject.transform.position.z - realDistanceFromPlayer + ZOffset);
                cc.enabled = true;
            }
            if (beingPushedByZ == false)
            {
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(gameObject.transform.position.x - realDistanceFromPlayer + XOffset, playerObject.transform.position.y, gameObject.transform.position.z + ZOffset);
                cc.enabled = true;
            }

            pbti.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ignore Character");
            ignoreFrame = true;
        }
    }
    void setOff()
    {
        //Debug.Log("setOff");
        if (ignoreFrame == false)
        {
            //Debug.Log("setOffTrue");
            ignoreFrame = true;
            if (hasParent == true)
            {
                gameObject.transform.parent = parentObject.transform;
            }
            else
            {
                gameObject.transform.parent = null;
            }
            beingPushedByX = false; beingPushedByZ = false;
            pbti.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractArea")
        {
            setOff();
            //Debug.Log("Stopped moving box: trigger");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //setOff();
        //Debug.Log("Stopped moving box: collision");
    }
}
