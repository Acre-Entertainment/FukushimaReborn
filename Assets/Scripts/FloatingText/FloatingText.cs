using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        mesh.enabled = true;
    }

    public void FadeOut()
    {

        mesh.enabled = false;
    }
}
