using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;    // Speed of forward movement
    public float swipeSensitivity = 1f; // Adjust sensitivity of swiping

    private Rigidbody rb;
    private Vector2 lastTouchPosition; // Track the last touch position
    private Vector2 deltaSwipe;       // The swipe difference

    private bool isTouching = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                deltaSwipe = touch.position - lastTouchPosition;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                deltaSwipe = Vector2.zero;
                isTouching = false;
            }
        }
    }

    private void PerformMovement()
    {
        // Always move forward
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;

        // Adjust direction based on swipe
        Vector3 swipeDirection = new Vector3(deltaSwipe.x, 0, deltaSwipe.y) * swipeSensitivity;
        Vector3 movement = forwardMovement + swipeDirection;

        // Apply movement to Rigidbody
        rb.AddForce(movement, ForceMode.Force);

        // Optional: Rotate the ball to face its movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }
}
