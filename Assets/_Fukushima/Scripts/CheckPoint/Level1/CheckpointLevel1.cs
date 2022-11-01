using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointLevel1 : MonoBehaviour
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

    [Header("ResetCheckPoint")]
    [SerializeField]
    private bool _resetCheckpoint;

    private void Awake()
    {
        if (_resetCheckpoint)
        {
            PlayerPrefs.DeleteKey("CheckPointLevel1X");
            PlayerPrefs.DeleteKey("CheckPointLevel1Y");
            PlayerPrefs.DeleteKey("CheckPointLevel1Z");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CheckPointLevel1X") && PlayerPrefs.HasKey("CheckPointLevel1Y") && PlayerPrefs.HasKey("CheckPointLevel1Z"))
        {
            _checkpointPosition.x = PlayerPrefs.GetFloat("CheckPointLevel1X");
            _checkpointPosition.y = PlayerPrefs.GetFloat("CheckPointLevel1Y");
            _checkpointPosition.z = PlayerPrefs.GetFloat("CheckPointLevel1Z");

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

        PlayerPrefs.SetFloat("CheckPointLevel1X", _checkpointPosition.x);
        PlayerPrefs.SetFloat("CheckPointLevel1Y", _checkpointPosition.y);
        PlayerPrefs.SetFloat("CheckPointLevel1Z", _checkpointPosition.z);
    }
}
