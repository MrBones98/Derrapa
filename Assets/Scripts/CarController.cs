using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Speed")]
    private float _driveSpeed=0;
    [SerializeField] private float _backingSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _turnStrenght;
    [SerializeField] private float _accelerationRange; // probably 
    [SerializeField] private float _backAccelerationRange; // probably 


    [SerializeField] private Rigidbody _sphereFollow;
    [SerializeField] private GameObject _carVisual;
    private CharacterController _characterController;
    private Camera _camera;
    private PlayerInputManager _playerInputManager;
    private Vector3 _currentMovement;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        print(_playerInputManager);
        
    }
    private void Start()
    {
        _sphereFollow.transform.parent = null;
        _playerInputManager = PlayerInputManager.Instance;
    }
    private void Update()
    {
        HandleMovement();
        //HandleRotation();
        _carVisual.transform.position = _sphereFollow.transform.position;
    }
    private void FixedUpdate()
    {
        _sphereFollow.AddForce(_currentMovement* 1000f);
        print(_currentMovement);
    }

    private void HandleRotation()
    {
        throw new NotImplementedException();
    }

    private void HandleMovement()
    {
        //gotta add one for the backacceleration 
        float speed = 0;
        if (_playerInputManager.MoveInput.y > 0)
        {
            speed = (_playerInputManager.AccelerateInput  ? _accelerationRange : 1f); // is it inputing accelration? then the range applied, if not : 1f speed
        }
        else if (_playerInputManager.MoveInput.y < 0)
        {
            speed = (_playerInputManager.AccelerateInput  ? _backAccelerationRange: 1f); // is it inputing accelration? then the range applied, if not : 1f speed
        }
        //remove the movement y as direct input for the car lmao marico raro
        //Vector3 inputDirection = new Vector3(_playerInputManager.MoveInput.x,0f, _playerInputManager.MoveInput.y);
        Vector3 inputDirection = new Vector3(0f,0f, _playerInputManager.MoveInput.y);
        //Vector3 worldDireciton = transform.TransformDirection(inputDirection);
        print(_playerInputManager.MoveInput);

        //worldDireciton.Normalize();
        //_currentMovement.x = worldDireciton.x;
        //_currentMovement.z = worldDireciton.z*speed;// this is the one for acceleration, test here
        //print(speed);
        inputDirection.Normalize();
        _currentMovement.x = inputDirection.x;
        _currentMovement.z = inputDirection.z*speed;// this is the one for acceleration, test here
        print(speed);
        //change it to rigidbody
        //_characterController.Move((_currentMovement)*Time.deltaTime);
    }
}
