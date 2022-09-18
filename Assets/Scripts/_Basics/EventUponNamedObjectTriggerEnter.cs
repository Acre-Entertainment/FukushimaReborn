using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventUponNamedObjectTriggerEnter : MonoBehaviour
{
    public string objectName;
    public UnityEvent unityEvent;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == objectName)
        {
            unityEvent.Invoke();
        }
    }
}

