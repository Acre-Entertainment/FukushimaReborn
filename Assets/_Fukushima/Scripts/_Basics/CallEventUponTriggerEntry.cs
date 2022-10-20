using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEventUponTriggerEntry : MonoBehaviour
{
    public string tag;
    public UnityEvent unityEvent;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == tag)
        {
            unityEvent.Invoke();
        }
    }
}