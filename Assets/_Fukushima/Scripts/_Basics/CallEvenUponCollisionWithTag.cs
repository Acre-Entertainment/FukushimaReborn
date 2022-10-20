using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEvenUponCollisionWithTag : MonoBehaviour
{
    public string tag;
    public UnityEvent unityEvent;
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("collision" + gameObject.name);
        if(collision.gameObject.tag == tag)
        {
            unityEvent.Invoke();
        }
    }
}
