using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudarTamanhoComWaterStopper : MonoBehaviour
{
    public GameObject objectScaled;
    public ParticleSystem waterStuff;

    float distanceFromObject;

    void OnTriggerStay(Collider other)
    {

        if(other.GetComponent<PushableBox>() != null)
        {
            distanceFromObject = other.transform.position.z - objectScaled.transform.position.z;
            if(distanceFromObject > 0){distanceFromObject = -distanceFromObject;}
            objectScaled.transform.localScale = new Vector3(distanceFromObject, 1, 1);

            var main = waterStuff.main;
            main.startLifetime = -distanceFromObject/10;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PushableBox>() != null)
        {
            var main = waterStuff.main;
            main.startLifetime = 0.7f;
        }
    }
}
