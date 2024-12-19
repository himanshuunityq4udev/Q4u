using UnityEngine;

namespace state
{
    public class BallController : MonoBehaviour
    {
        public float moveSpeed = 10f; // Speed multiplier
        public StateMachine StateMachine { get; private set; }
        public int Lives { get; private set; } = 4;
        public Transform respawnPoint;

        private Rigidbody rb;
        private Vector2 moveInput;
        private BallInputActions inputActions;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputActions = new BallInputActions();

            // Bind the Move action
            inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        }


        private void Start()
        {
    
            StateMachine = new StateMachine();
            StateMachine.ChangeState(new PlayingState(this));
        }

        private void Update()
        {
            StateMachine.Update();
        }


        private void FixedUpdate()
        {
            // Apply movement in FixedUpdate
            if (StateMachine.GetType() == typeof(PlayingState) && moveInput != Vector2.zero)
            {
                Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
                rb.AddForce(movement, ForceMode.Force);
            }
        }



        public void EnableBallControl(bool enable)
        {
            rb.isKinematic = !enable;
        }

        public bool IsAtFinishLine()
        {
            // Implement logic to check if the ball has reached the finish line
            return false;
        }

        public bool IsFallen()
        {
            // Check if the ball has fallen below a certain threshold
            return transform.position.y < -10;
        }

        public void LoseLife()
        {
            Lives--;
            Debug.Log("Lives remaining: " + Lives);
        }

        public void Respawn()
        {
            transform.position = respawnPoint.position;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Respawned");
        }

        public void ShowWinScreen()
        {
            // Show the win screen UI
        }

        public void ShowGameOverScreen()
        {
            // Show the game over screen UI
        }
    }

}