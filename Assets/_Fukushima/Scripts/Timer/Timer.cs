using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    private GameObject _target;

    [Header("Timer")]
    [SerializeField]
    private float _activeTime;
    [SerializeField]
    private float _desactiveTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Active());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Active()
    {
        _target.SetActive(true);
        yield return new WaitForSeconds(_activeTime);
        StartCoroutine(Desactive());
    }

    IEnumerator Desactive()
    {
        _target.SetActive(false);
        yield return new WaitForSeconds(_desactiveTime);
        StartCoroutine(Active());
    }
}
