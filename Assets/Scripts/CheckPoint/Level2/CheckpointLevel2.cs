using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointLevel2 : MonoBehaviour
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
        if (PlayerPrefs.HasKey("CheckPointLevel2X") && PlayerPrefs.HasKey("CheckPointLevel2Y") && PlayerPrefs.HasKey("CheckPointLevel2Z"))
        {
            _checkpointPosition.x = PlayerPrefs.GetFloat("CheckPointLevel2X");
            _checkpointPosition.y = PlayerPrefs.GetFloat("CheckPointLevel2Y");
            _checkpointPosition.z = PlayerPrefs.GetFloat("CheckPointLevel2Z");

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

        PlayerPrefs.SetFloat("CheckPointLevel2X", _checkpointPosition.x);
        PlayerPrefs.SetFloat("CheckPointLevel2Y", _checkpointPosition.y);
        PlayerPrefs.SetFloat("CheckPointLevel2Z", _checkpointPosition.z);
    }
}
