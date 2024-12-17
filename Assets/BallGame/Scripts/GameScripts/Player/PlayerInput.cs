using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] PlayerData playerData;

    private BallInputActions ballInput;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;

    private float startTime;

    #region ----------- Unity Callbacks -------------

    private void Awake()
    {
        ballInput = new BallInputActions();
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
    #endregion

    #region ------------- Player Input ----------------------

    // When Move input starts (touch begins)
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
       /* //  Ignore input if the pointer is over a UI element(for both mouse and touch)
        if (IsPointerOverUI())
        {
            return;
        }*/
      
            startTouchPosition = context.ReadValue<Vector2>();
            startTime = Time.time; // Record the time when touch begins
        
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

        Debug.Log("Swipe" + swipeDistance);
        if (swipeDistance > playerData.swipeDistanceThreshold && swipeTime < playerData.swipeTimeThreshold)
        {
           ActionHelper.onMove.Invoke(touchDelta, swipeDistance, playerData.swipeDistanceThreshold);
        }
        else
        {
           ActionHelper.onMove.Invoke(touchDelta, swipeDistance, playerData.swipeDistanceThreshold);
        }
    }
    #endregion


    #region ------------ GUI touch Checker -----------------

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
    #endregion

}
