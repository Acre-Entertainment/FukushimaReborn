using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [Header("What Is The Level")]
    public int levelNumber;

    [Header("CheckPointLevel")]
    [SerializeField]
    private CheckpointLevel1 _checkpointLevel1Master;
    [SerializeField]
    private GameObject _checkpointLevel1MasterGameObject;
    //[SerializeField]
    //private CheckpointLevel2 _checkpointLevel2;
    //[SerializeField]
    //private CheckpointLevel3 _checkpointLevel3;

    // Start is called before the first frame update
    void Start()
    {
        if(levelNumber == 1)
        {
            _checkpointLevel1Master = _checkpointLevel1MasterGameObject.GetComponent<CheckpointLevel1>();
        }
        else if (levelNumber == 2)
        {
            //_checkpointLevel2 = GetComponent<CheckpointLevel2>();
        }
        else if (levelNumber == 3)
        {
            //_checkpointLevel3 = GetComponent<CheckpointLevel3>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelNumber == 1 && other.CompareTag("Player"))
        {
            _checkpointLevel1Master.SaveCheckPoint();
        }
        else if (levelNumber == 2 && other.CompareTag("Player"))
        {
            //_checkpointLevel2.SaveCheckPoint();
        }
        else if (levelNumber == 3 && other.CompareTag("Player"))
        {
            //_checkpointLevel3.SaveCheckPoint();
        }
    }
}
