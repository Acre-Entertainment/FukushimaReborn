using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    GameObject playerObject, parentObject;
    PressButtonToInteract pbti;
    //MeshCollider ms;
    public GameObject anchorX, anchorXminor, anchorZ, anchorZminor;
    public float distanceFromPlayerX, distanceFromPlayerZ, XOffset, ZOffset;
    float realDistanceFromPlayer;
    bool facingX, facingXMinor, facingZ, facingZMinor;
    public bool noXMovement, noZmovement;
    bool beingPushedByX, beingPushedByZ;
    float constantX, constantZ;
    int playerFixedRotation;
    Vector3 lastPlayerLocation, currentPlayerLocation, movement, newPosition;
    bool hasParent, ignoreFrame;
    private CharacterController cc;

    PlayerMovement pm;
    float startingJump;

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
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        startingJump = pm._jump;
    }
    //void FixedUpdate()
    //{
    //    if (beingPushedByX == true || beingPushedByZ == true)
    //    {
    //        if (beingPushedByX == false)
    //        {
    //            gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, playerObject.transform.position.z + realDistanceFromPlayer - ZOffset);
    //            cc.enabled = false;
    //            playerObject.GetComponent<CharacterController>().transform.position = new Vector3(constantX + XOffset, playerObject.transform.position.y, playerObject.transform.position.z);
    //            cc.enabled = true;
    //        }
    //        if (beingPushedByZ == false)
    //        {
    //            gameObject.transform.position = new Vector3(playerObject.transform.position.x + realDistanceFromPlayer - XOffset, gameObject.transform.position.y, constantZ);
    //            cc.enabled = false;
    //            playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, constantZ + ZOffset);
    //            cc.enabled = true;
    //        }
    //    }
    //}
    void Update()
    {
        if (beingPushedByX == true || beingPushedByZ == true)
        {
            if (beingPushedByX == false)
            {
                gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, playerObject.transform.position.z + realDistanceFromPlayer - ZOffset);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(constantX + XOffset, playerObject.transform.position.y, playerObject.transform.position.z);
                if(facingZ == true)
                {
                    playerObject.GetComponent<CharacterController>().transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if(facingZMinor == true)
                {
                    playerObject.GetComponent<CharacterController>().transform.eulerAngles = new Vector3(0, 0, 0);
                }
                cc.enabled = true;
            }
            if (beingPushedByZ == false)
            {
                gameObject.transform.position = new Vector3(playerObject.transform.position.x + realDistanceFromPlayer - XOffset, gameObject.transform.position.y, constantZ);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, constantZ + ZOffset);
                if (facingX == true)
                {
                    playerObject.GetComponent<CharacterController>().transform.eulerAngles = new Vector3(0, 270, 0);
                }
                if (facingXMinor == true)
                {
                    playerObject.GetComponent<CharacterController>().transform.eulerAngles = new Vector3(0, 90, 0);
                }
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

                float smallestDistance = Mathf.Min(distanceX, distanceXminor, distanceZ, distanceZminor);

                Debug.Log(smallestDistance);

                if (smallestDistance == distanceX) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; realDistanceFromPlayer = -distanceFromPlayerX; facingX = true;}
                if (smallestDistance == distanceXminor) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; realDistanceFromPlayer = distanceFromPlayerX; facingXMinor = true;}
                if (smallestDistance == distanceZ) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; realDistanceFromPlayer = -distanceFromPlayerZ; facingZ = true;}
                if (smallestDistance == distanceZminor) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0; realDistanceFromPlayer = distanceFromPlayerZ; facingZMinor = true;}
            }
            if (noXMovement == true && noZmovement == false)
            {
                float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
                float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

                float smallestDistance = Mathf.Min(distanceZ, distanceZminor);

                Debug.Log(smallestDistance);

                if (smallestDistance == distanceZ) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; realDistanceFromPlayer = -distanceFromPlayerZ; facingZ = true;}
                if (smallestDistance == distanceZminor) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0; realDistanceFromPlayer = distanceFromPlayerZ; facingZMinor = true;}
            }
            if (noXMovement == false && noZmovement == true)
            {
                float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
                float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);

                float smallestDistance = Mathf.Min(distanceX, distanceXminor);

                Debug.Log(smallestDistance);

                if (smallestDistance == distanceX) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; realDistanceFromPlayer = -distanceFromPlayerX; facingX = true;}
                if (smallestDistance == distanceXminor) { beingPushedByX = true; beingPushedByZ = false; constantZ = gameObject.transform.position.z; realDistanceFromPlayer = distanceFromPlayerX; facingXMinor = true;}
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
            pm._jump = 0;
            pm._puObject = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Character");
            ignoreFrame = true;
        }
    }
    public void setOff()
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
            beingPushedByX = false; beingPushedByZ = false; facingX = false; facingXMinor = false; facingZ = false; facingZMinor = false;
            pbti.enabled = true;
            pm._jump = startingJump;
            pm._puObject = false;
            pm.pushAndPullToIdle = true;
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
