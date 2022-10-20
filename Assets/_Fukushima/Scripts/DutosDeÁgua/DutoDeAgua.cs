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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Squirt());
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
}
