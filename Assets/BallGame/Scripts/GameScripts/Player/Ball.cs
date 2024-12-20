using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Rigidbody ballRigidbody;

    #region ------------Unity Callbacks---------------

    private void Awake()
    {
  
        ballRigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().maxAngularVelocity = playerData.m_MaxAngularVelocity;
        playerData.respawnPosition = new Vector3(0, 1, 0);
    }

    private void OnEnable()
    {
        ActionHelper.onMove += MoveBall;
    }
    private void OnDisable()
    {
        ActionHelper.onMove -= MoveBall;

    }

    #endregion

    #region ------------MOVE THE BALL ---------------


    // Function to move the ball
    private void MoveBall(Vector2 _inputDelta, float _speed, float _swipeDistanceThreshold)
    {
        if (IsGrounded())
        {

            Vector3 moveDirection = new Vector3(-_inputDelta.x, 0, -_inputDelta.y).normalized;

            // Move in the camera's forward/backward direction
            Vector3 cameraForward = Camera.main.transform.forward;

            cameraForward.y = 0; // Ignore vertical movement

            // Calculate movement in camera's forward/backward direction
            Vector3 moveVector = cameraForward * moveDirection.z + Camera.main.transform.right * moveDirection.x;

            // Move the ball using Rigidbody physics
            ballRigidbody.AddForce(moveVector * _speed * _swipeDistanceThreshold, ForceMode.Force);
        }
    }


    private bool IsGrounded()
    {
        // Check if the ball is on the ground using a raycast
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f);
    }


    #endregion
}
