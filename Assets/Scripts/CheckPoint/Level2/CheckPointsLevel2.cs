using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsLevel2 : MonoBehaviour
{

    [Header("CheckPointLevel")]
    [SerializeField]
    private CheckpointLevel2 _checkpointLevel2Master;
    [SerializeField]
    private GameObject _checkpointLevel2MasterGameObject;

    // Start is called before the first frame update
    void Start()
    {
        _checkpointLevel2Master = _checkpointLevel2MasterGameObject.GetComponent<CheckpointLevel2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _checkpointLevel2Master.SaveCheckPoint();
        }
    }
}
