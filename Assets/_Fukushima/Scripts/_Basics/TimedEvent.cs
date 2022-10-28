using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    public float timeUntilEvent;
    public UnityEvent eventToBeCalled;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeUntilEvent);
        eventToBeCalled.Invoke();
    }
}
