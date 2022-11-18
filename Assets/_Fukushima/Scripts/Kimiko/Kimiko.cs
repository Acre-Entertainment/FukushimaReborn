using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kimiko : MonoBehaviour
{
    public Animator animator;

    public bool playAnimation;
    // Start is called before the first frame update
    void Start()
    {
       animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation)
        {
            playAnimation = false;
            animator.SetTrigger("Play");
        }
    }
}
