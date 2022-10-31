using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButtonToInteract_Object : MonoBehaviour
{
    public UnityEvent Event;
    public bool hasSound;
    public bool playOneShot;
    public AudioClip oneShotSound;
    [Range(0f, 1f)]
    public float volume = 1;
}
