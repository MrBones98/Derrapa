using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _playerControls;

    [Header("Action Map name References")]
    [SerializeField] private string _actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string _move = "Move";
    [SerializeField] private string _look = "Look";
    [SerializeField] private string _brake = "Brake";
    [SerializeField] private string _accelerate = "Accelerate";

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _brakeAction;
    private InputAction _accelerateAction;

    public Vector2 MoveInput { get;private set; }
    public Vector2 LookInput { get;private set; }
    public bool BrakeInput { get;private set; }
    public bool AccelerateInput { get;private set; }

    public static PlayerInputManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
        _lookAction = _playerControls.FindActionMap(_actionMapName).FindAction(_look);
        _brakeAction = _playerControls.FindActionMap(_actionMapName).FindAction(_brake);
        _accelerateAction= _playerControls.FindActionMap(_actionMapName).FindAction(_accelerate);

        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += context => MoveInput = Vector2.zero;

        _lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        _lookAction.canceled += context => LookInput = Vector2.zero;

        _brakeAction.performed += context => BrakeInput = true;
        _brakeAction.canceled += context => BrakeInput = false;
        
        _accelerateAction.performed += context => AccelerateInput = true;
        _accelerateAction.canceled+= context => AccelerateInput = false;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _brakeAction.Enable();
        _accelerateAction.Enable();
    }
    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _brakeAction.Disable();
        _accelerateAction.Disable();
    }
}
