using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsLevel1 : MonoBehaviour
{

    [Header("CheckPointLevel")]
    [SerializeField]
    private CheckpointLevel1 _checkpointLevel1Master;
    [SerializeField]
    private GameObject _checkpointLevel1MasterGameObject;

    // Start is called before the first frame update
    void Start()
    {
        _checkpointLevel1Master = _checkpointLevel1MasterGameObject.GetComponent<CheckpointLevel1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _checkpointLevel1Master.SaveCheckPoint();
        }
    }
}
