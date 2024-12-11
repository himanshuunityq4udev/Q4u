using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxX = 2f;
    float horizontalInput;
    private BallInputActions ballInput;

    private void Awake()
    {
        ballInput = new BallInputActions();
    }

    private void OnEnable()
    {
        ballInput.BallControls.Enable();
        ballInput.BallControls.Move.started += OnMoveStarted;
    }
    private void OnDisable()
    {
        ballInput.BallControls.Move.started -= OnMoveStarted;
        ballInput.BallControls.Disable();
      
    }
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        MovePaddle(move.x);

    }

    private void MovePaddle(float value)
    {
        horizontalInput = value;
        if ((horizontalInput > 0 && transform.position.x < maxX) | (horizontalInput < 0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime;
        }
    }

 
}
