using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    GameObject playerObject, parentObject;
    PressButtonToInteract pbti;
    //MeshCollider ms;
    public GameObject anchorX, anchorXminor, anchorZ, anchorZminor;
    public bool noXMovement, noZmovement;
    bool beingPushedByX, beingPushedByZ;
    float constantX, constantZ;
    int playerFixedRotation;
    Vector3 lastPlayerLocation, currentPlayerLocation, movement, newPosition;
    bool hasParent, ignoreFrame;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        pbti = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
        //ms = gameObject.GetComponent<MeshCollider>();
        if(gameObject.transform.parent != null)
        {
            parentObject = gameObject.transform.parent.gameObject;
            hasParent = true;
        }
    }
    void FixedUpdate()
    {
        if(beingPushedByX == true || beingPushedByZ == true)
        {
            lastPlayerLocation = currentPlayerLocation;
            currentPlayerLocation = playerObject.transform.position;
            movement = currentPlayerLocation - lastPlayerLocation;
            newPosition = gameObject.transform.position + movement;
            if(beingPushedByX == false)
            {
                gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, newPosition.z);
                playerObject.transform.position = new Vector3(constantX, playerObject.transform.position.y, playerObject.transform.position.z);
            }
            if(beingPushedByZ == false)
            {
                gameObject.transform.position = new Vector3(newPosition.x, gameObject.transform.position.y, constantZ);
                playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, constantZ);
            }
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && ignoreFrame == false)
        {
            setOff();
            //Debug.Log("Stopped moving box: keypress");
        }
        ignoreFrame = false;
    }
    public void setPush()
    {
        //Debug.Log("Started pushing box.");
        ignoreFrame = true;
        if(noXMovement == false && noZmovement == false)
        {
            float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
            float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);
            float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
            float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

            float biggestDistance = Mathf.Max(distanceX, distanceXminor, distanceZ, distanceZminor);

            if(biggestDistance == distanceX){beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z;}
            if(biggestDistance == distanceXminor){beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z;}
            if(biggestDistance == distanceZ){beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x;}
            if(biggestDistance == distanceZminor){beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0;}
        }
        if(noXMovement == true && noZmovement == false)
        {
            float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
            float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

            float biggestDistance = Mathf.Max(distanceZ, distanceZminor);

            if(biggestDistance == distanceZ){beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x;}
            if(biggestDistance == distanceZminor){beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0;}
        }
        if(noXMovement == false && noZmovement == true)
        {
            float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
            float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);

            float biggestDistance = Mathf.Max(distanceX, distanceXminor);

            if(biggestDistance == distanceX){beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z;}
            if(biggestDistance == distanceXminor){beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z;}
        }
        if(noXMovement == true && noZmovement == true)
        {
            Debug.Log("ERROR: Both movements blocked");
        }

        lastPlayerLocation = playerObject.transform.position;
        currentPlayerLocation = playerObject.transform.position;
        pbti.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Ignore Character");
    }
    void setOff()
    {
        if(hasParent == true)
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

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "InteractArea")
        {
            setOff();
            //Debug.Log("Stopped moving box: trigger");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        setOff();
        //Debug.Log("Stopped moving box: collision");
    }
}
