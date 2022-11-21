using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DutoDeAgua : MonoBehaviour
{
    [Header("Water")]
    [SerializeField]
    private GameObject _water;

    [Header("Time")]
    [SerializeField]
    private float _timeSquirt;
    [SerializeField]
    private float _timeStop;

    [Header("Start")]
    [SerializeField]
    private bool _start;
    [SerializeField]
    private float _startWaitTime;

    [Header("Sound")]
    [SerializeField]
    private bool soundController;
    [SerializeField]
    private AudioClip waterJetSound;
    [SerializeField]
    [Range(0, 1)]
    private float volume;
    [SerializeField]
    private AudioSource audioSource;

    public UnityEvent onStartWater;
    public UnityEvent onEndWater;
    // Start is called before the first frame update
    void Start()
    {
        if (_start)
        {
            StartCoroutine(Squirt());
        }
        else
        {
            StartCoroutine(WaitStart());
        }
    }

    IEnumerator Squirt()
    {
        _water.SetActive(true);
        onStartWater.Invoke();
        if(soundController) audioSource.PlayOneShot(waterJetSound, volume);
        yield return new WaitForSeconds(_timeSquirt);
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        _water.SetActive(false);
        onEndWater.Invoke();
        yield return new WaitForSeconds(_timeStop);
        StartCoroutine(Squirt());
    }

    IEnumerator WaitStart()
    {
        _water.SetActive(false);
        yield return new WaitForSeconds(_startWaitTime);
        StartCoroutine(Squirt());
    }
}
