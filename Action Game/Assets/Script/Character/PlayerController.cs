using GameInsideGame.Component.Character;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

//Get Input
public class PlayerController : CharacterBehavior
{
    public static PlayerController playerInstance { get; private set; }
    [SerializeField] private Camera _camera;
    
    //Movement
    private const float normalSpeed = 5;
    private const float sprintSpeed = 10;

    //Camera
    private static float _targetDirection;
    private static float _angle;
    /// <summary>
    /// Use for Reference only
    /// </summary>
    private float _turnRef;
    private const float _turnSmoothTime = 0.1f;

    //Ability
    public GameObject projectileRoot;

    private void Awake()
    {
        if (playerInstance != null)
            Destroy(playerInstance);
        playerInstance = this;
    }

    protected override void Update()
    {
        ExecuteCamera();
        ExecuteInput();
        base.Update();
    }

    private void ExecuteInput()
    {
        if (_characterController.isGrounded && _velocity.y <= 0f)
        {
            _velocity.y = -0.05f;
            if (Input.GetButtonDown("Jump") && _isRolling == false)
            {
                Rolling();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Combat();
        }
        _velocityInput.x = Input.GetAxisRaw("Horizontal");
        _velocityInput.z = Input.GetAxisRaw("Vertical");
        _targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;        
    }

    private void ExecuteCamera()
    {
        if (_velocityInput.magnitude >= 0.1f)
        {
            //Calculate direction of player, reference by camera.
            _targetDirection = Mathf.Atan2(_velocityInput.x, _velocityInput.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetDirection, ref _turnRef, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        }
    }

    
}
