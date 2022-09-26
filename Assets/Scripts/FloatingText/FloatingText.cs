using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public MeshRenderer mesh;

    private bool _isShowing;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = mesh.material.color;
            c.a = f;
            mesh.material.color = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        if (!_isShowing)
        {
            StartCoroutine(StartingShow());
        }
    }

    public void FadeOut()
    {

        if (_isShowing)
        {
            StartCoroutine(StartingFade());
        }
    }

    IEnumerator StartingShow()
    {
        _isShowing = true;

        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = mesh.material.color;
            c.a = f;
            mesh.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator StartingFade()
    {
        _isShowing = false;

        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = mesh.material.color;
            c.a = f;
            mesh.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
