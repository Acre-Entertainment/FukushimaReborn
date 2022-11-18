using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustsceneTrigger : MonoBehaviour
{
    public bool activeCustscene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && activeCustscene)
        {
            PlayerMovement.custscene = true;
            PlayerMovement.canChangeInput = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !activeCustscene)
        {
            PlayerMovement.custscene = false;
            PlayerMovement.custsceneToIdle = true;
            PlayerMovement.canChangeInput = false;
        }
    }
}
