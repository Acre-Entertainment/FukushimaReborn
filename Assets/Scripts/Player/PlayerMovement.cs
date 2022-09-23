using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Attributes")] //Atributos do Player
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _crouchSpeed;
    [SerializeField]
    private float _jump;
    [SerializeField]
    private float _gravity = -9.81f;

    private CharacterController _controller;
    private float _speed;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _sprinting;
    private bool _lerpCrouch;
    private bool _crouching;
    private float _crouchTimer;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        StartCoroutine(BugController());
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _controller.isGrounded;

        if (_sprinting && !_crouching)
        {
            _speed = _runSpeed;
        }
        else if (!_sprinting && !_crouching)
        {
            _speed = _walkSpeed;
        }
        else if(!_sprinting && _crouching)
        {
            _speed = _crouchSpeed;
        }

        if (_lerpCrouch)
        {
            _crouchTimer += Time.deltaTime;
            float p = _crouchTimer / 1;
            p *= p;

            if (_crouching)
            {
                _controller.height = Mathf.Lerp(_controller.height, 1, p);
            }
            else
            {
                _controller.height = Mathf.Lerp(_controller.height, 2, p);
            }

            if (p > 1)
            {
                _lerpCrouch = false;
                _crouchTimer = 0;
            }
        }
    }

    public void Movement(Vector2 input)
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection.x = input.x;
        movementDirection.z = input.y;
        _controller.Move(transform.TransformDirection(movementDirection) * _speed * Time.deltaTime);
        _velocity.y += _gravity * Time.deltaTime;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void Sprint()
    {
        if (!_crouching)
        {
            _sprinting = !_sprinting;
        }
        else
        {
            _sprinting = false;
        }
    }

    public void Crouch()
    {
        _crouching = !_crouching;
        _crouchTimer = 0;
        _lerpCrouch = true;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jump * -3.0f * _gravity);
        }
    }

    IEnumerator BugController()
    {
        _controller.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _controller.enabled = true;
    }

    public void ImpactX(float forceX)
    {
        Vector3 impact = Vector3.zero;
        impact.y = 0;
        impact.x = forceX;
        _controller.Move(transform.TransformDirection(impact) * Time.deltaTime);
    }

    public void ImpactZ(float forceZ)
    {
        Vector3 impact = Vector3.zero;
        impact.y = 0;
        impact.z = forceZ;
        _controller.Move(transform.TransformDirection(impact) * Time.deltaTime);
    }
}
