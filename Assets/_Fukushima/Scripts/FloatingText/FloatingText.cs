using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator _animator;

    private bool _isShowing;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        if (!_isShowing)
        {
            _isShowing = true;
            _animator.SetBool("Fade", true);
        }
    }

    public void FadeOut()
    {

        if (_isShowing)
        {
            _isShowing = false;
            _animator.SetBool("Fade", false);
        }
    }
}
