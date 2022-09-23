using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterForce : MonoBehaviour
{

    [Header("Water Force")]
    [SerializeField]
    private float _force;

    [Header("What is The Axis, Choose One")]
    public bool axisX;
    public bool axisXNegative;
    public bool axisZ;
    public bool axisZNegative;

    private GameObject _playerGameObject;
    private PlayerMovement _playerScript;
    // Start is called before the first frame update
    void Start()
    {
        _playerGameObject = GameObject.Find("Player");
        _playerScript = _playerGameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (axisX)
            {
                _playerScript.ImpactX(_force);
            }
            else if (axisXNegative)
            {
                _playerScript.ImpactX(-_force);
            }
            else if (axisZ)
            {
                _playerScript.ImpactZ(-_force);
            }
            else if (axisZNegative)
            {
                _playerScript.ImpactZ(_force);
            }
        }
    }
}
