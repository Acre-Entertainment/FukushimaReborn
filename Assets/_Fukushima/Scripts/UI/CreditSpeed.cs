using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreditSpeed : MonoBehaviour
{
    public UnityEvent SpaceDown;
    public UnityEvent SpaceUp;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceDown.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            SpaceUp.Invoke();
        }
    }
}
