using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SummonEventAtAnyKeyPress : MonoBehaviour
{
    public UnityEvent unityEvent;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            unityEvent.Invoke();
        }
    }
}
