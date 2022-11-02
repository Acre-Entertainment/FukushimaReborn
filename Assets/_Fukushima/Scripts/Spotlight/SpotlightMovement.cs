using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightMovement : MonoBehaviour
{
    public float speed;
    public float spaceBuffer;
    Rigidbody rb;
    public List<GameObject> patrolZones = new List<GameObject>();
    public int currentTarget;
    Vector3 target, targetDistance, myTP, targetTP;
    float distance;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        target = patrolZones[currentTarget].transform.position - gameObject.transform.position;
        distance = Vector3.Distance(patrolZones[currentTarget].transform.position, gameObject.transform.position);
        if(distance != 0){targetDistance = new Vector3(target.x/distance, target.y/distance, target.z/distance);}
        rb.AddForce(targetDistance * speed);

        myTP = gameObject.transform.position;
        targetTP = patrolZones[currentTarget].transform.position;
        if(myTP.x > targetTP.x - spaceBuffer && myTP.x < targetTP.x + spaceBuffer && myTP.y > targetTP.y - spaceBuffer && myTP.y < targetTP.y + spaceBuffer && myTP.z > targetTP.z - spaceBuffer && myTP.z < targetTP.z + spaceBuffer)
        {
            currentTarget++;
            if(currentTarget >= patrolZones.Count)
            {
                currentTarget = 0;
            }
        }
    }
}
