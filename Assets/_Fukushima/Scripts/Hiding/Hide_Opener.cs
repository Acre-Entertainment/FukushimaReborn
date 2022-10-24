using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Opener : MonoBehaviour
{
    public GameObject HidedPlayer;

    PlayerMovement pm;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            HidedPlayer.SetActive(true);
            gameObject.SetActive(false);
            pm.Unhide();
        }
    }
}
