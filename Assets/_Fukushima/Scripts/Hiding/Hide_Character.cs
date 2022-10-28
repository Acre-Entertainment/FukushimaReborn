using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Character : MonoBehaviour
{
    public GameObject opener;

    PlayerMovement pm;

    public static bool _canChangeThePosition;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    public void Hide()
    {
        _canChangeThePosition = true;
        pm.Hide();
        StartCoroutine(HideAnimation());
    }

    IEnumerator HideAnimation()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
        opener.SetActive(true);
    }
}
