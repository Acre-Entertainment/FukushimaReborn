using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsLevel3 : MonoBehaviour
{
    [Header("CheckPointLevel")]
    [SerializeField]
    private CheckpointLevel3 _checkpointLevel3Master;
    [SerializeField]
    private GameObject _checkpointLevel3MasterGameObject;

    // Start is called before the first frame update
    void Start()
    {
        _checkpointLevel3Master = _checkpointLevel3MasterGameObject.GetComponent<CheckpointLevel3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _checkpointLevel3Master.SaveCheckPoint();
        }
    }
}
