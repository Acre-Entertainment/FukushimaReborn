using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnabled : MonoBehaviour
{
    public new bool enabled = false;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        enabled = true;
    }

    void Update()
    {
        
    }
}
