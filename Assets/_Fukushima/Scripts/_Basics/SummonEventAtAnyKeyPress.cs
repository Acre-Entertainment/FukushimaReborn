using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SummonEventAtAnyKeyPress : MonoBehaviour
{
    public UnityEvent Event;


    void Update()
    {
        if (Input.anyKeyDown)
        {
            Event.Invoke();
        }
    }
}

