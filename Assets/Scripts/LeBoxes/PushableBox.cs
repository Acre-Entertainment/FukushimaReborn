using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    GameObject playerObject, parentObject;
    PressButtonToInteract pbti;
    MeshCollider ms;
    public GameObject anchorX, anchorXminor, anchorZ, anchorZminor;
    public bool beingPushedByX, beingPushedByZ;
    public float constantX, constantZ;
    int playerFixedRotation;
    Vector3 lastPlayerLocation, currentPlayerLocation, movement, newPosition;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        pbti = GameObject.FindGameObjectWithTag("InteractArea").GetComponent<PressButtonToInteract>();
        ms = gameObject.GetComponent<MeshCollider>();
        parentObject = gameObject.transform.parent.gameObject;
    }
    void FixedUpdate()
    {
        if(beingPushedByX == true || beingPushedByZ == true)
        {
            lastPlayerLocation = currentPlayerLocation;
            currentPlayerLocation = playerObject.transform.position;
            movement = currentPlayerLocation - lastPlayerLocation;
            newPosition = gameObject.transform.position + movement;
            if(beingPushedByX == false){gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, newPosition.z);}
            if(beingPushedByZ == false){gameObject.transform.position = new Vector3(newPosition.x, gameObject.transform.position.y, constantZ);}
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            {
                setOff();
            }
    }
    public void setPush()
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

        lastPlayerLocation = playerObject.transform.position;
        currentPlayerLocation = playerObject.transform.position;
        pbti.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Ignore Character");
    }
    void setOff()
    {
        gameObject.transform.parent = parentObject.transform;
        beingPushedByX = false; beingPushedByZ = false;
        pbti.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "InteractArea")
        {
            setOff();
        }
    }
}
