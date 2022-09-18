using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventUponNamedObjectTriggerExit : MonoBehaviour
{
    public string objectName;
    public UnityEvent unityEvent;
    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.name == objectName)
        {
            unityEvent.Invoke();
        }
    }
}

