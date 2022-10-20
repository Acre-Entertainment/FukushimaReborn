using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Opener : MonoBehaviour
{
    public GameObject HidedPlayer;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            HidedPlayer.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
