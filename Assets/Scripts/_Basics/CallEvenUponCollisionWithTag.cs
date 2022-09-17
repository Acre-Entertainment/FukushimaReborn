using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEvenUponCollisionWithTag : MonoBehaviour
{
    public string tag;
    public UnityEvent unityEvent;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == tag)
        {
            unityEvent.Invoke();
        }
    }
}
