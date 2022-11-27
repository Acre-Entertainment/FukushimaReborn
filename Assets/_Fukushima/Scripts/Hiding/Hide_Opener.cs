using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hide_Opener : MonoBehaviour
{
    public GameObject HidedPlayer;

    public UnityEvent onAppearing;

    public static bool hasPressed;

    PlayerMovement pm;

    public AudioClip exitSound;

    public AudioSource aS;

    [Range(0f, 1f)]
    public float volume = 1;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            hasPressed = true;
            aS.PlayOneShot(exitSound, volume);
            HidedPlayer.SetActive(true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pm.Unhide();
        onAppearing.Invoke();
        yield return new WaitForSeconds(1);
        hasPressed = false;
        Hide_Character._canChangeThePosition = false;
        Hide_Character.isHided = false;
        HidedPlayer.SetActive(true);
        gameObject.SetActive(false);
    }
}
