using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Character : MonoBehaviour
{
    public GameObject opener;
    public void Hide()
    {
        //colokar animação
        gameObject.SetActive(false);
        opener.SetActive(true);
    }
}
