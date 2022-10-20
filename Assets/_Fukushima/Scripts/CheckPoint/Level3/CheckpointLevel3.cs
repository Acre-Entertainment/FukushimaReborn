using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointLevel3 : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private GameObject _player;

    [Header("CheckPoints")]
    [SerializeField]
    private GameObject[] _checkpoints;

    [Header("Position")]
    [SerializeField]
    private Vector3 _checkpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CheckPointLevel3X") && PlayerPrefs.HasKey("CheckPointLevel3Y") && PlayerPrefs.HasKey("CheckPointLevel3Z"))
        {
            _checkpointPosition.x = PlayerPrefs.GetFloat("CheckPointLevel3X");
            _checkpointPosition.y = PlayerPrefs.GetFloat("CheckPointLevel3Y");
            _checkpointPosition.z = PlayerPrefs.GetFloat("CheckPointLevel3Z");

            _player.transform.position = _checkpointPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCheckPoint()
    {
        _checkpointPosition = _player.transform.position;

        PlayerPrefs.SetFloat("CheckPointLevel3X", _checkpointPosition.x);
        PlayerPrefs.SetFloat("CheckPointLevel3Y", _checkpointPosition.y);
        PlayerPrefs.SetFloat("CheckPointLevel3Z", _checkpointPosition.z);
    }
}
