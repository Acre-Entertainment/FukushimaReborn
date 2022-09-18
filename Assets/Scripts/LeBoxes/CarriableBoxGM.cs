using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableBoxGM : MonoBehaviour
{
    public GameObject[] boxLocations;

    public void activate()
    {
        foreach (GameObject box in boxLocations)
        {
            box.SetActive(true);
        }
    }
    public void dactivate()
    {
        foreach (GameObject box in boxLocations)
        {
            box.SetActive(false);
        }
    }
}
