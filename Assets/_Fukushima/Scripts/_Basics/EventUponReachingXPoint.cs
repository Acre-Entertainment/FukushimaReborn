using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventUponReachingXPoint : MonoBehaviour
{
    public float XPoint;
    public bool reversed;

    public UnityEvent onActivation;

    void Update()
    {
        if(reversed == true)
        {
            if(gameObject.transform.position.x <= XPoint && reversed == false)
            {
                onActivation.Invoke();
            }
        }
        else
        {
            if(gameObject.transform.position.x >= XPoint && reversed == false)
            {
                onActivation.Invoke();
            }
        }
        
    }
}
