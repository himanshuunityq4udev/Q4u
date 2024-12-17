using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BallPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float swipeDistanceThreshold = 20;  // Minimum distance for a swipe in units
    public float swipeTimeThreshold = 0.5f;

    [SerializeField] private float m_MaxAngularVelocity = 25;

 
    private Rigidbody ballRigidbody;

    private BallInputActions ballInput;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;

    private float startTime;
    private Health ballHealth;



    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        ballInput = new BallInputActions();
        ballHealth = GetComponent<Health>();
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
    }
    private void OnEnable()
    {
        ballInput.BallControls.Enable();
        ballInput.BallControls.Move.started += OnMoveStarted;
        ballInput.BallControls.Move.canceled += OnMoveCanceled;
        
    }
    private void OnDisable()
    {
        // Unsubscribe from input events when the object is disabled
        ballInput.BallControls.Move.started -= OnMoveStarted;
        ballInput.BallControls.Move.canceled -= OnMoveCanceled;
        ballInput.BallControls.Disable();
    }

    private void Update()
    {
        GameOver();

    }


    public void GameOver()
    {
        /*if (transform.position.y < -30)
        {
            if (ballHealth.life > 0)
            {
               // ballHealth.UpdateBallLife(-1);
                transform.position = ballHealth.GetRespanPosition();
                StopTheBall();

            }
            else
            {
                StopTheBall();
                ballRigidbody.isKinematic = true;
                ballRigidbody.useGravity = false;
                Destroy(transform.gameObject);
                Debug.Log("Playagain");
            }


        }*/
    }

    public void StopTheBall()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.linearVelocity = Vector3.zero;
    }

    // When Move input starts (touch begins)
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        // Ignore input if the pointer is over a UI element (for both mouse and touch)
        if (IsPointerOverUI())
        {
            return;
        }

        startTouchPosition = context.ReadValue<Vector2>();
        startTime = Time.time; // Record the time when touch begins
        
    }


    // Helper function to check if the touch is over a UI element
    private bool IsPointerOverUI()
    {
        // Check if there's at least one touch input
        if (Touchscreen.current.touches.Count > 0)
        {
            // Iterate through all touches (typically you'll want the first one)
            var touch = Touchscreen.current.touches[0];

            // Create PointerEventData using touch position
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = touch.position.ReadValue() // Use the touch position
            };

            // Raycast against all UI elements
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            return raycastResults.Count > 0;
        }
        else
        {
            // If no active touch, use mouse input
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue() // Use mouse position
            };

            // Raycast against all UI elements
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            return raycastResults.Count > 0;
        }
    }



    // When Move input is canceled (touch ends)
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        currentTouchPosition = context.ReadValue<Vector2>();
        Vector2 touchDelta = currentTouchPosition - startTouchPosition;

        float swipeTime = Time.time - startTime; // Calculate swipe time
        float swipeDistance = touchDelta.magnitude; // Calculate swipe distance

        Debug.Log("Swipe"+swipeDistance);
        if (swipeDistance > swipeDistanceThreshold && swipeTime < swipeTimeThreshold)
        {
            //swipeDistance += moveSpeed;
            MoveBall(touchDelta, swipeDistance);
        }
        else
        { 
            MoveBall(touchDelta, swipeDistance);
        }
    }

        // Function to move the ball
    private void MoveBall(Vector2 inputDelta, float speed)
    {
        if (IsGrounded())
        {

            Vector3 moveDirection = new Vector3(-inputDelta.x, 0, -inputDelta.y).normalized;

            // Move in the camera's forward/backward direction
            Vector3 cameraForward = Camera.main.transform.forward;

            cameraForward.y = 0; // Ignore vertical movement

            // Calculate movement in camera's forward/backward direction
            Vector3 moveVector = cameraForward * moveDirection.z + Camera.main.transform.right * moveDirection.x;

            // Move the ball using Rigidbody physics
            ballRigidbody.AddForce(moveVector * speed * swipeDistanceThreshold, ForceMode.Force);
        }
    }


    private bool IsGrounded()
    {
        // Check if the ball is on the ground using a raycast
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f);
    }

   
    
}