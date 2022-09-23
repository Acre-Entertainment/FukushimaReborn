using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaDoDuto : MonoBehaviour
{
    [Header("Water Attributes")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _size;

    [Header("What is The Axis, Choose One")]
    public bool axisX;
    public bool axisXNegative;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (axisX)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_size, 1, 1), Time.deltaTime * _speed);
        }
        else if (axisXNegative)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(-_size, 1, 1), Time.deltaTime * _speed);
        }

    }

    private void OnDisable()
    {
        if (axisX || axisXNegative)
        {
            transform.localScale = new Vector3(0, 1, 1);
        }
    }
}
