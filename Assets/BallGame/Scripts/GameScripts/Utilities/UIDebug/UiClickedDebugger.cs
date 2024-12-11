using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class UiClickedDebugger : MonoBehaviour {
    
    private void Update()
    {
        // Detect pointer or touch interactions
        if (Mouse.current.leftButton.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            DetectUIElementUnderPointer();
        }
    }

    private void DetectUIElementUnderPointer()
    {
        // Determine pointer position
        Vector2 pointerPosition = Mouse.current.position.ReadValue();

        // For touch input, override with the touch position
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            pointerPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        // Create pointer event data
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = pointerPosition
        };

        // Perform a raycast to find UI elements
        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        // Log the names of all UI elements under the pointer
        foreach (var result in raycastResults)
        {
            Debug.Log($"UI Element Clicked: {result.gameObject.name}");
        }
    }
}
