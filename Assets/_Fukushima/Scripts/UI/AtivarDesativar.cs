using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarDesativar : MonoBehaviour
{
    public bool useShortcut;
    public KeyCode shortCut;
    public GameObject ativar1;
    public GameObject ativar2;
    public GameObject ativar3;
    public GameObject ativar4;
    public GameObject desativar1;
    public GameObject desativar2;
    public GameObject desativar3;
    public GameObject desativar4;
    public GameObject desativar5;
    public GameObject desativar6;

    public void Trocar()
    {
        if(ativar1 != null)
        {
            ativar1.SetActive(true);
        }
        if (ativar2 != null)
        {
            ativar2.SetActive(true);
        }
        if (ativar3 != null)
        {
            ativar3.SetActive(true);
        }
        if (ativar4 != null)
        {
            ativar4.SetActive(true);
        }

        if (desativar1 != null)
        {
            desativar1.SetActive(false);
        }
        if (desativar2 != null)
        {
            desativar2.SetActive(false);
        }
        if (desativar3 != null)
        {
            desativar3.SetActive(false);
        }
        if (desativar4 != null)
        {
            desativar4.SetActive(false);
        }
        if (desativar5 != null)
        {
            desativar5.SetActive(false);
        }
        if (desativar6 != null)
        {
            desativar6.SetActive(false);
        }
    }

    private void Update()
    {
        if(useShortcut)
        {
            if (Input.GetKeyDown(shortCut))
            {
                Trocar();
            }
        }
    }
}
