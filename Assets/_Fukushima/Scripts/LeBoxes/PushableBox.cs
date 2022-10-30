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
    public float minimunDistanceFromAnchor = 0.75f;
    public float collisionBuffer = 0.1f;
    float realDistanceFromPlayer;
    bool facingX, facingXMinor, facingZ, facingZMinor;
    public bool noXMovement, noZmovement;
    bool beingPushedByX, beingPushedByZ;
    float constantX, constantZ;
    int playerFixedRotation;
    Vector3 lastPlayerLocation, currentPlayerLocation, movement, newPosition;
    bool hasParent, ignoreFrame, movingX, movingXminor, movingZ, movingZminor;
    float previousX, previousZ;
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
    void Update()
    {
        if (beingPushedByX == true || beingPushedByZ == true)
        {
            if (beingPushedByZ == true)
            {
                previousZ = gameObject.transform.position.z;
                gameObject.transform.position = new Vector3(constantX, gameObject.transform.position.y, playerObject.transform.position.z + realDistanceFromPlayer - ZOffset);
                if(previousZ < gameObject.transform.position.z)
                {
                    movingZ = true;
                    movingZminor = false;
                }
                if(previousZ > gameObject.transform.position.z)
                {
                    movingZ = false;
                    movingZminor = true;
                }

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
            if (beingPushedByX == true)
            {
                previousX = gameObject.transform.position.x;
                gameObject.transform.position = new Vector3(playerObject.transform.position.x + realDistanceFromPlayer - XOffset, gameObject.transform.position.y, constantZ);
                if(previousX < gameObject.transform.position.x)
                {
                    movingX = true;
                    movingXminor = false;
                }
                if(previousX > gameObject.transform.position.x)
                {
                    movingX = false;
                    movingXminor = true;
                }
                
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
        if (ignoreFrame == false && beingPushedByX == false && beingPushedByZ == false)
        {
            ignoreFrame = true;
            if (noXMovement == false && noZmovement == false)
            {
                float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
                float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);
                float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
                float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

                float smallestDistance = Mathf.Min(distanceX, distanceXminor, distanceZ, distanceZminor);

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

                if (smallestDistance == distanceZ) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; realDistanceFromPlayer = -distanceFromPlayerZ; facingZ = true;}
                if (smallestDistance == distanceZminor) { beingPushedByX = false; beingPushedByZ = true; constantX = gameObject.transform.position.x; playerFixedRotation = 0; realDistanceFromPlayer = distanceFromPlayerZ; facingZMinor = true;}
            }
            if (noXMovement == false && noZmovement == true)
            {
                float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
                float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);

                float smallestDistance = Mathf.Min(distanceX, distanceXminor);

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
            gameObject.tag = "Untagged";
            ignoreFrame = true;
        }
    }
    public void setOff()
    {
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
            gameObject.tag = "Event";
        }
    }
    public bool calculateDistanceFromPlayer(GameObject gopull)
    {
        if (noXMovement == false && noZmovement == false)
        {
            float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
            float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);
            float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
            float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);
            float smallestDistance = Mathf.Min(distanceX, distanceXminor, distanceZ, distanceZminor);
            if(smallestDistance <= minimunDistanceFromAnchor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (noXMovement == true && noZmovement == false)
        {
            float distanceZ = Vector3.Distance(playerObject.transform.position, anchorZ.transform.position);
            float distanceZminor = Vector3.Distance(playerObject.transform.position, anchorZminor.transform.position);

            float smallestDistance = Mathf.Min(distanceZ, distanceZminor);

            if(smallestDistance <= minimunDistanceFromAnchor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (noXMovement == false && noZmovement == true)
        {
            float distanceX = Vector3.Distance(playerObject.transform.position, anchorX.transform.position);
            float distanceXminor = Vector3.Distance(playerObject.transform.position, anchorXminor.transform.position);
            float smallestDistance = Mathf.Min(distanceX, distanceXminor);
            if(smallestDistance <= minimunDistanceFromAnchor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.layer != 9 && other.gameObject.tag != "Player")
        {
            setBuffer();
            setOff();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer != 9 && other.gameObject.tag != "Player")
        {
            setBuffer();
            setOff();
        }
    }

    void setBuffer()
    {
        if(beingPushedByX == true)
        {
            if(movingX == true)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - collisionBuffer, gameObject.transform.position.y, gameObject.transform.position.z);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x - collisionBuffer, playerObject.transform.position.y, playerObject.transform.position.z);
                cc.enabled = true;
            }
            if(movingXminor == true)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + collisionBuffer, gameObject.transform.position.y, gameObject.transform.position.z);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x + collisionBuffer, playerObject.transform.position.y, playerObject.transform.position.z);
                cc.enabled = true;
            }
        }
        if(beingPushedByZ == true)
        {
            if(movingZ == true)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - collisionBuffer);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z - collisionBuffer);
                cc.enabled = true;
            }
            if(movingZminor == true)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + collisionBuffer);
                cc.enabled = false;
                playerObject.GetComponent<CharacterController>().transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z + collisionBuffer);
                cc.enabled = true;
            }
        }
    }
}
