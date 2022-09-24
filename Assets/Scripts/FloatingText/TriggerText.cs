using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{

    [Header("FloatingText")]
    [SerializeField]
    private FloatingText _floatingTextScript;

    [Header("What The Trigger Does, Choose Only One")]
    public bool fadeIn;
    public bool fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        //_floatingTextScript = GetComponent<FloatingText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeIn)
            {
                _floatingTextScript.FadeIn();
            }
            else if (fadeOut)
            {
                _floatingTextScript.FadeOut();
            }
        }
    }
}
