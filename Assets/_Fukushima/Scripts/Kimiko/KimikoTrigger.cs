using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimikoTrigger : MonoBehaviour
{
    public Kimiko _kimiko;
    // Start is called before the first frame update
    void Start()
    {
        _kimiko = GameObject.Find("Kimiko").GetComponent<Kimiko>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _kimiko.playAnimation = true;
        }
    }
}
