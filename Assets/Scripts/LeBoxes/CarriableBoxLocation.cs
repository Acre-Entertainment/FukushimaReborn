using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableBoxLocation : MonoBehaviour
{
    MeshRenderer mr;

    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fuck");
        if(other.tag == "Carried")
        {
            other.GetComponent<CarriableBox>().inPosition = true;
            other.GetComponent<CarriableBox>().potencial = gameObject;
            mr.material.color = Color.green;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Carried")
        {
            other.GetComponent<CarriableBox>().inPosition = false;
            mr.material.color = Color.red;
        }
    }
}
