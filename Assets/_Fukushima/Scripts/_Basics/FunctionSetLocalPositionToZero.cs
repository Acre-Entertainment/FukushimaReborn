using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionSetLocalPositionToZero : MonoBehaviour
{
    public void callFunction()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }
}
