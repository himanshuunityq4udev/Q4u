using UnityEngine;
using UnityEngine.InputSystem;
public class TouchManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset plyerControls;

    [Header("Action Map Name Refrence")]
    [SerializeField] private string actionMapName = "Touch";

    [Header("Action Name References")]
    [SerializeField] private string PrimaryContact = "PrimaryContact";
    [SerializeField] private string touchPress = "PrimaryPosition";


    //refrence of the input action
    private InputAction touchPositionAction;
    private InputAction touchPressAction;


    public Vector2 TouchPositionInput { get; private set; }
    public float TouchPressInput { get; private set; }

    public static TouchManager Instance { get; private set; }

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        touchPositionAction = plyerControls.FindActionMap(actionMapName).FindAction(PrimaryContact);
        touchPressAction = plyerControls.FindActionMap(actionMapName).FindAction(touchPress);
        TouchPressed();
    }


    void TouchPressed()
    {
        touchPressAction.performed += context =>{
            touchPositionAction.performed += context => TouchPositionInput = context.ReadValue<Vector2>();
        };


        touchPressAction.canceled += context =>
        {
            touchPositionAction.performed += context => TouchPositionInput = Vector2.zero;

        };
    }



    private void OnEnable()
    {
        touchPressAction.Enable();
    }
    private void OnDisable()
    {
        touchPressAction.Disable();
    }

    private void Update()
    {

        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(TouchPositionInput.x, 0f, TouchPositionInput.y));

        Debug.Log(position);
    }


}
