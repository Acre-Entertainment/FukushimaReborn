using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressEscToEvent : MonoBehaviour
{
    //aperte enter e o evento sera chamado
    public UnityEvent eventToHappen;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            eventToHappen.Invoke();
        }
    }
}
