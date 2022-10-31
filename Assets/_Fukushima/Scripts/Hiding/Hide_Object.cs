using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Object : MonoBehaviour
{
    public Animator _animator;
    private Transform _player;
    public bool open;
    public Transform point;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerPosition()
    {
        _player.position = point.position;
        //_player.rotation = point.rotation = Quaternion.identity;
        open = true;
    }

    public void Close()
    {
        open = false;
    }
}
