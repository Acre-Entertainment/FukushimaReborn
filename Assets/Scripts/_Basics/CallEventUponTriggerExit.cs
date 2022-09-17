using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEventUponTriggerExit : MonoBehaviour
{
    public string tag;
    public UnityEvent unityEvent;
    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == tag)
        {
            unityEvent.Invoke();
        }
    }
}