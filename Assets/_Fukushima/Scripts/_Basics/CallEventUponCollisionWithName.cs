using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEventUponCollisionWithName : MonoBehaviour
{
    public string name;
    public UnityEvent unityEvent;
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision" + gameObject.name + " and " + collision.gameObject.name);
        if(collision.gameObject.name == name)
        {
            unityEvent.Invoke();
        }
    }
}
