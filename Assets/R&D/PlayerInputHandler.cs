using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("2D Controoler"), Tooltip("Any changes in runtime will not reflected>")]
    [SerializeField] private bool is2D = false;

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset plyerControls;

    [Header("Action Map Name Refrence")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";

    //refrence of the input action
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    [Header("Deadzone Values")]
    [SerializeField] private float leftStickDeadzoneValue;


    //Gatter and Setter
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTrigger { get; private set; }
    public float SprintValue { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

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

        moveAction = plyerControls.FindActionMap(actionMapName).FindAction(move);
        if (!is2D)
        {
            lookAction = plyerControls.FindActionMap(actionMapName).FindAction(look);
        }
        jumpAction = plyerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = plyerControls.FindActionMap(actionMapName).FindAction(sprint);
        RegisterInputAction();

        //DeadZone for controller
        InputSystem.settings.defaultDeadzoneMin = leftStickDeadzoneValue;
        PrintDevices();
    }

    void RegisterInputAction()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        if (!is2D)
        {
            lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
            lookAction.canceled += context => LookInput = Vector2.zero;
        }
        jumpAction.performed += context => JumpTrigger = true;
        jumpAction.canceled += context => JumpTrigger = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;
    }


    private void OnEnable()
    {
        moveAction.Enable();
        if (!is2D)
        {
            lookAction.Enable();
        }
        jumpAction.Enable();
        sprintAction.Enable();

        InputSystem.onDeviceChange += OnDeviceChange;
    }
    private void OnDisable()
    {
        moveAction.Disable();
        if (!is2D)
        {
            lookAction.Disable();
        }
        jumpAction.Disable();
        sprintAction.Disable();

        InputSystem.onDeviceChange -= OnDeviceChange;
    }


    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                Debug.Log("Device Disconnected: " + device.name);
                break;
            case InputDeviceChange.Reconnected:
                Debug.Log("Device Connected: " + device.name);
                break;
        }
    }

    void PrintDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device.enabled)
            {
                Debug.Log("Active Device: " + device.name);
            }
        }
    }
}

