using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class OneWheelVehicleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardForce = 500f; // Force to move forward/backward
    public float turnTorque = 50f;   // Torque for turning
    public float balanceForce = 50f; // Force to balance the vehicle upright

    [Header("Balance Settings")]
    public float maxTiltAngle = 45f; // Max tilt angle before applying correction

    private Rigidbody rb;
    private Vector2 moveInput; // Holds the input values for movement

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down; // Lower center of mass for stability
    }

    private void FixedUpdate()
    {
        // Handle movement
        HandleMovement();

        // Handle balance
        HandleBalance();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read movement input from the Input System
        moveInput = context.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        // Apply forward/backward force
        if (Mathf.Abs(moveInput.y) > 0.01f)
        {
            rb.AddForce(transform.forward * moveInput.y * forwardForce * Time.fixedDeltaTime, ForceMode.Force);
        }

        // Apply turning torque
        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            rb.AddTorque(transform.up * moveInput.x * turnTorque * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private void HandleBalance()
    {
        // Get the vehicle's tilt angle
        Vector3 currentUp = transform.up;
        float tiltAngle = Vector3.Angle(currentUp, Vector3.up);

        // Apply balance correction only if the tilt angle exceeds a threshold
        if (tiltAngle > 0.1f && tiltAngle < maxTiltAngle)
        {
            Vector3 tiltAxis = Vector3.Cross(currentUp, Vector3.up).normalized;
            rb.AddTorque(tiltAxis * balanceForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}
