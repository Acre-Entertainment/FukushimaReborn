using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WalkEvent : MonoBehaviour
{
    public UnityEvent walk;
    public AudioSource [] Walksound;

    public void Walking()
    {
            Walksound[0].Play();
            walk.Invoke();
    }

    private void Step(AnimationEvent animationEvent)
    {
        PlayerMovement.footstep?.Invoke(animationEvent);
    }

    private void ActionStep(AnimationEvent animationEvent)
    {
        PlayerMovement.actionFootstep?.Invoke(animationEvent);
    }
}
