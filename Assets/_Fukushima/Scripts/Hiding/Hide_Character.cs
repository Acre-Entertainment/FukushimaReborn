using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Character : MonoBehaviour
{
    public GameObject opener;

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
        yield return new WaitForSeconds(2f);
        isHided = true;
        opener.SetActive(true);
    }
}
