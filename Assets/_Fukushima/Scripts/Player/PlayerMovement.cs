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
    private float _carryingSpeed;
    public float _jump;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _groundedTimer;
    [SerializeField]
    private float _animationSmoothTime;
    [SerializeField]
    private float _animationPlayTransition;

    [Header("Camera")]
    [SerializeField]
    private Transform _camera;

    private CharacterController _controller;
    private Animator _animator;
    private Crouch _crouch;

    private float _speed;
    private Vector3 _velocity;
    private bool _controllerGrounded;
    private bool _sprinting;
    public bool _isCarrying;
    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _groundedCurrentTimer;
    private int _moveXAnimator;
    private int _moveZAnimator;
    private int _jumpAnimation;
    private Vector2 _currentAnimationBlendVector;
    private Vector2 _animationVelocity;
    private int _layerCrouchedIndex;
    private int _layerCarryingIndex;

    public static bool custscene;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _animator = GameObject.Find("Idle").GetComponent<Animator>();
        _crouch = gameObject.GetComponent<Crouch>();

        StartCoroutine(BugController());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _moveXAnimator = Animator.StringToHash("MoveX");
        _moveZAnimator = Animator.StringToHash("MoveZ");
        _jumpAnimation = Animator.StringToHash("Jump");

        _layerCrouchedIndex = _animator.GetLayerIndex("Crouched");
        _layerCarryingIndex = _animator.GetLayerIndex("Carrying");

        custscene = false;
    }

    // Update is called once per frame
    void Update()
    {
        _controllerGrounded = _controller.isGrounded;

        if(_controllerGrounded)
        {
            _groundedCurrentTimer = _groundedTimer;
        }
        else
        {
            _groundedCurrentTimer -= Time.deltaTime;
        }

        if (_sprinting && !_crouch.isCrouched && !_isCarrying && !custscene)
        {
            _animator.SetLayerWeight(_layerCrouchedIndex, 0);
            _animator.SetLayerWeight(_layerCarryingIndex, 0);
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", true);
            _speed = _runSpeed;
        }
        else if (!_sprinting && !_crouch.isCrouched && !_isCarrying && !custscene)
        {
            _animator.SetLayerWeight(_layerCrouchedIndex, 0);
            _animator.SetLayerWeight(_layerCarryingIndex, 0);
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isRunning", false);
            _speed = _walkSpeed;
        }
        else if(!_sprinting && _crouch.isCrouched && !_isCarrying && !custscene)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetLayerWeight(_layerCrouchedIndex, 1);
            _animator.SetLayerWeight(_layerCarryingIndex, 0);
            _speed = _crouchSpeed;
        }
        else if (!_sprinting && !_crouch.isCrouched && _isCarrying && !custscene)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetLayerWeight(_layerCrouchedIndex, 0);
            _animator.SetLayerWeight(_layerCarryingIndex, 1);
            _speed = _carryingSpeed;
        }
        else if (custscene)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isRunning", false);
            _speed = _walkSpeed;
        }

        if(custscene)
       {
            if (_crouch.isCrouched)
            {
                _crouch.isCrouched = false;
            }

            if (_isCarrying)
            {
                _isCarrying = false;
            }
        }
    }

    public void Movement(Vector2 input)
    {
        _currentAnimationBlendVector = Vector2.SmoothDamp(_currentAnimationBlendVector, input, ref _animationVelocity, _animationSmoothTime);
        Vector3 movementDirection = new Vector3(_currentAnimationBlendVector.x, 0, _currentAnimationBlendVector.y).normalized;
        movementDirection.x = input.x;
        movementDirection.z = input.y;

        if(movementDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(direction.normalized * _speed * Time.deltaTime);
        }

        _velocity.y += _gravity * Time.deltaTime;

        if (_groundedCurrentTimer >= 0 && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _controller.Move(_velocity * Time.deltaTime);

        _animator.SetFloat(_moveXAnimator, _currentAnimationBlendVector.x);
        _animator.SetFloat(_moveZAnimator, _currentAnimationBlendVector.y);
    }

    public void Sprint()
    {
        if (!_crouch.isCrouched)
        {
            _sprinting = !_sprinting;
        }
        else
        {
            _sprinting = false;
        }
    }

    public void Jump()
    {
        if (_groundedCurrentTimer >= 0)
        {
            _animator.CrossFade(_jumpAnimation, _animationPlayTransition);
            StartCoroutine(FixAnimationJump());
        }
    }

    IEnumerator FixAnimationJump()
    {
        yield return new WaitForSeconds(0.5f);
        _velocity.y = Mathf.Sqrt(_jump * -3.0f * _gravity);
    }

    IEnumerator BugController()
    {
        _controller.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _controller.enabled = true;
    }

    public void Impact(float force)
    {
        Vector3 impact = Vector3.zero;
        impact.y = 0;
        impact.x = force;
        _controller.Move(impact * Time.deltaTime);
    }
}
