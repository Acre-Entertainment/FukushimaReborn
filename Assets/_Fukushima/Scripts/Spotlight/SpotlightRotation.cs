using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightRotation : MonoBehaviour
{
    public SpotlightMovement spotlightMovement;
    public float rotationSpeed;
    Quaternion lookRotation;
    Vector3 direction;

    void FixedUpdate()
    {
        direction = (spotlightMovement.patrolZones[spotlightMovement.currentTarget].transform.position - gameObject.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}