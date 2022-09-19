using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mixer;

    public string parameter;

    public void setVolume(float sliderValue)
    {
        mixer.SetFloat(parameter, Mathf.Log10(sliderValue) * 20);
    }
}
