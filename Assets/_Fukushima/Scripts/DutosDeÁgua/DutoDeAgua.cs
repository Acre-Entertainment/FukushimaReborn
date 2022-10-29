using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Squirt()
    {
        _water.SetActive(true);
        yield return new WaitForSeconds(_timeSquirt);
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        _water.SetActive(false);
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
