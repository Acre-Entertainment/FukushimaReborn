using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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
    [SerializeField]
    private float _pushAndPullSpeed;
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

    [Header("Sound")]
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _jumpSFX;
    [SerializeField]
    [Range(0f, 1f)]
    private float jumpVolume;
    [SerializeField]
    [Range(0f, 1f)]
    private float landVolume;
    [SerializeField]
    [Range(0f, 1f)]
    private float waterSplashVolume;
    [SerializeField]
    private AudioClip _landSFX;
    private bool wasOnAir;
    [SerializeField]
    private AudioClip _waterSplashSFX;

    [Header("Others")]
    public bool canMove;
    public bool _isCarrying;
    public bool _puObject;
    public bool crouchToIdle;
    public bool carryingToIdle;
    public bool pushAndPullToIdle;

    public UnityEvent jumpWaterEvent;
    public UnityEvent jumpEvent;
    public string WaterTag;
    public bool isOnWater;

    private CharacterController _controller;
    private Animator _animator;
    private Crouch _crouch;

    private float _speed;
    private Vector3 _velocity;
    private bool _controllerGrounded;
    private bool _sprinting;
    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _groundedCurrentTimer;
    private int _moveXAnimator;
    private int _moveZAnimator;
    private int _jumpAnimation;
    private int _hideAnimation;
    private int _unhideAnimation;
    private int _drowingAnimation;
    private int _eletrocutedAnimation;
    private int _surrenderAnimation;
    private Vector2 _currentAnimationBlendVector;
    private Vector2 _animationVelocity;
    private int _layerCrouchedIndex;
    private int _layerCarryingIndex;
    private int _layerPuObjectIndex;
    private float _layerWeightVelocity;
    private float _currentCrouchedLayer;
    private float _currentCarryingLayer;
    private float _currentPuObjectLayer;
    private bool _isJumping;
    private bool _fixBug;
    private bool _isDead;

    public static bool custscene;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

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
        _unhideAnimation = Animator.StringToHash("Unhide");
        _hideAnimation = Animator.StringToHash("Hide");
        _drowingAnimation = Animator.StringToHash("Drowing");
        _eletrocutedAnimation = Animator.StringToHash("Eletrocuted");
        _surrenderAnimation = Animator.StringToHash("Surrender");


        _layerCrouchedIndex = _animator.GetLayerIndex("Crouched");
        _layerCarryingIndex = _animator.GetLayerIndex("Carrying");
        _layerPuObjectIndex = _animator.GetLayerIndex("Pull/Push");

        custscene = false;
    }

    // Update is called once per frame
    void Update()
    {
        _controllerGrounded = _controller.isGrounded;
        _animator.SetBool("Grounded", _controllerGrounded);

        if(_controllerGrounded)
        {
            _groundedCurrentTimer = _groundedTimer;
            _isJumping = false;
        }
        else
        {
            _groundedCurrentTimer -= Time.deltaTime;
        }


        if(!_isJumping && !_controllerGrounded && _fixBug)
        {
            _animator.SetLayerWeight(_layerCrouchedIndex, 0);
            _animator.SetLayerWeight(_layerCarryingIndex, 0);
            _animator.SetLayerWeight(_layerPuObjectIndex, 0);
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            _isCarrying = false;
            _puObject = false;
            while (!_isDead)
            {
                _animator.Play(_jumpAnimation);
                break;
            }
        }

        _currentCrouchedLayer = _animator.GetLayerWeight(_layerCrouchedIndex);
        _currentCarryingLayer = _animator.GetLayerWeight(_layerCarryingIndex);
        _currentPuObjectLayer = _animator.GetLayerWeight(_layerPuObjectIndex);

        if (crouchToIdle)
        {
            AnimationCrouchToIdle();
            StartCoroutine(FixBugSmoothAnimation());
        }

        if (carryingToIdle)
        {
            AnimationCarryingToIdle();
            StartCoroutine(FixBugSmoothAnimation());
        }

        if (pushAndPullToIdle)
        {
            AnimationPushAndPullToIdle();
            StartCoroutine(FixBugSmoothAnimation());
        }

        if (_sprinting && !_crouch.isCrouched && !_isCarrying && !_puObject && !custscene)
        {
            if (_controller.isGrounded)
            {
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isRunning", true);
            }
            _speed = _runSpeed;
        }
        else if (!_sprinting && !_crouch.isCrouched && !_isCarrying && !_puObject && !custscene)
        {
            if (_controller.isGrounded)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isRunning", false);
            }
            _speed = _walkSpeed;
        }
        else if(!_sprinting && _crouch.isCrouched && !_isCarrying && !_puObject && !custscene)
        {
            if (_controller.isGrounded)
            {
                _animator.SetBool("isRunning", false);
                _animator.SetLayerWeight(_layerCrouchedIndex, Mathf.SmoothDamp(_currentCrouchedLayer, 1, ref _layerWeightVelocity, _animationSmoothTime));
            }
            _speed = _crouchSpeed;
        }
        else if (!_sprinting && !_crouch.isCrouched && _isCarrying && !_puObject && !custscene)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetLayerWeight(_layerCarryingIndex, Mathf.SmoothDamp(_currentCarryingLayer, 1, ref _layerWeightVelocity, _animationSmoothTime));
            _speed = _carryingSpeed;
        }
        else if (!_sprinting && !_crouch.isCrouched && !_isCarrying && _puObject && !custscene)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetLayerWeight(_layerPuObjectIndex, Mathf.SmoothDamp(_currentPuObjectLayer, 1, ref _layerWeightVelocity, _animationSmoothTime));
            _speed = _pushAndPullSpeed;
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

            if (_puObject)
            {
                _puObject = false;
            }
        }

        if(!_controllerGrounded)
        {
            wasOnAir = true;
        }
        else
        {
            if(wasOnAir)
            {
                wasOnAir = false;

                if(isOnWater == true)
                {
                    _audioSource.PlayOneShot(_waterSplashSFX, waterSplashVolume);
                    jumpWaterEvent.Invoke();
                }
                else
                {
                    _audioSource.PlayOneShot(_landSFX, landVolume);
                    jumpEvent.Invoke();
                }
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
        if (_groundedCurrentTimer >= 0 && !custscene)
        {
            _isJumping = true;
            _animator.CrossFade(_jumpAnimation, _animationPlayTransition);
            _velocity.y = Mathf.Sqrt(_jump * -3.0f * _gravity);
            _audioSource.PlayOneShot(_jumpSFX, jumpVolume);


        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == WaterTag)
        {
            isOnWater = true;
        }
        else
        {
            isOnWater = false;
        }
    }

    public void Hide()
    {
        canMove = false;
        _animator.applyRootMotion = true;
        _animator.CrossFade(_hideAnimation, _animationPlayTransition);
    }

    public void Unhide()
    {
        _animator.CrossFade(_unhideAnimation, _animationPlayTransition);
        StartCoroutine(HideUnhideCanMove());
    }

    IEnumerator HideUnhideCanMove()
    {
        yield return new WaitForSeconds(1f);
        _animator.applyRootMotion = false;
        canMove = true;
    }

    public void AnimationCrouchToIdle()
    {
        while(crouchToIdle)
        {
            _animator.SetLayerWeight(_layerCrouchedIndex, Mathf.SmoothDamp(_currentCrouchedLayer, 0, ref _layerWeightVelocity, _animationSmoothTime)); 
            break;
        }
    }

    public void AnimationCarryingToIdle()
    {
        while (carryingToIdle)
        {
            _animator.SetLayerWeight(_layerCarryingIndex, Mathf.SmoothDamp(_currentCarryingLayer, 0, ref _layerWeightVelocity, _animationSmoothTime));
            break;
        }
    }

    public void AnimationPushAndPullToIdle()
    {
        while (pushAndPullToIdle)
        {
            _animator.SetLayerWeight(_layerPuObjectIndex, Mathf.SmoothDamp(_currentPuObjectLayer, 0, ref _layerWeightVelocity, _animationSmoothTime));
            break;
        }
    }

    IEnumerator BugController()
    {
        _controller.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _controller.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _fixBug = true;
    }

    IEnumerator FixBugSmoothAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        crouchToIdle = false;
        carryingToIdle = false;
        pushAndPullToIdle = false;
    }

    public void Impact(float force)
    {
        Vector3 impact = Vector3.zero;
        impact.y = 0;
        impact.x = force;
        _controller.Move(impact * Time.deltaTime);
    }

    public void Impact2(float force)
    {
        Vector3 impact = Vector3.zero;
        impact.y = 0;
        impact.z = force;
        _controller.Move(impact * Time.deltaTime);
    }

    public void Dead(int typeOfDeath)
    {
        _controller.enabled = false;
        _isDead = true;
        canMove = false;
        if(typeOfDeath == 0)
        {
            _crouch.isCrouched = false;
            _animator.CrossFade(_drowingAnimation, _animationPlayTransition);
        }
        else if (typeOfDeath == 1)
        {
            _crouch.isCrouched = false;
            _animator.CrossFade(_surrenderAnimation, _animationPlayTransition);
        }
        else if (typeOfDeath == 2)
        {
            _crouch.isCrouched = false;
            _animator.CrossFade(_eletrocutedAnimation, _animationPlayTransition);
        }
    }
}
