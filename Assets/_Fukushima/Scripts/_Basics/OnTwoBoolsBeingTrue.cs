using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTwoBoolsBeingTrue : MonoBehaviour
{
    public bool value1, value2;
    public UnityEvent unityEvent;

    public void changeValue1(bool newValue)
    {
        value1 = newValue;
        checkValues();
    }
    public void changeValue2(bool newValue)
    {
        value2 = newValue;
        checkValues();
    }

    void checkValues()
    {
        if(value1 == true && value2 == true)
        {
            unityEvent.Invoke();
        }
    }
}
