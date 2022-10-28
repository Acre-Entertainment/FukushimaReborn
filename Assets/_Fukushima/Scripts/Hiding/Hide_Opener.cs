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
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pm.Unhide();
        yield return new WaitForSeconds(4);
        Hide_Character._canChangeThePosition = false;
        gameObject.SetActive(false);
    }
}
