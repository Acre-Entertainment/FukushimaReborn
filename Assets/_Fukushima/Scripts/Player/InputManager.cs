using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput; 
    private PlayerInput.OnGroundActions _onGround; 
    private PlayerMovement _movement;
    //private PlayerLook _vision;

    private void Awake()
    {

        _playerInput = new PlayerInput();
        _onGround = _playerInput.OnGround;

        _movement = GetComponent<PlayerMovement>();
        //_vision = GetComponent<PlayerLook>();

        _onGround.Pulo.performed += ctx => _movement.Jump();
        _onGround.Correr.performed += ctx => _movement.Sprint();
        //_onGround.Agachar.performed += ctx => _movement.Crouch();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_movement.canMove)
        {
            _movement.Movement(_onGround.Movimento.ReadValue<Vector2>());
        }
    }

    //private void LateUpdate()
    //{
       // _vision.Vision(_onGround.Visão.ReadValue<Vector2>());
    //}

    private void OnEnable()
    {
        _onGround.Enable();
    }

    private void OnDisable()
    {
        _onGround.Disable();
    }
}
