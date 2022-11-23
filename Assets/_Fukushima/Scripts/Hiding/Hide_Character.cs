using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hide_Character : MonoBehaviour
{
    public GameObject opener;

    public UnityEvent onDisappearing;

    PlayerMovement pm;

    public static bool _canChangeThePosition;
    public static bool isHided;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        _canChangeThePosition = false;
        isHided = false;
    }
    public void Hide()
    {
        _canChangeThePosition = true;
        pm.Hide();
        StartCoroutine(HideAnimation());
    }

    IEnumerator HideAnimation()
    {
        yield return new WaitForSeconds(1f);
        isHided = true;
        opener.SetActive(true);
        gameObject.SetActive(false);
        onDisappearing.Invoke();
    }
}
