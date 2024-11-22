using UnityEngine;

public class AnimatedController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    [Header("jump Parameters")]
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private float horizontalInput;
    private bool isGrounded;
    private bool shouldJump;

    private void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        horizontalInput = inputHandler.MoveInput.x;

        shouldJump = inputHandler.JumpTrigger && isGrounded;

        if(horizontalInput != 0)
        {
            FlipSprite(horizontalInput);
        }
    }



    private void FixedUpdate()
    {
        ApplyMovement();
        //Jumping
        if (shouldJump)
        {
            ApplyJump();
        }

    }
    void ApplyMovement()
    {
        //Sprint
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);

        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocityY);

       
    }

    void ApplyJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        shouldJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void FlipSprite(float horizontalMovement)
    {
        if(horizontalMovement < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
