using UnityEngine;
using UnityEngine.Events;

public class ButtonActionManager : MonoBehaviour
{
    // Declare a static UnityAction that takes a string argument
    public static UnityAction<ButtonsName> OnButtonClicked;

    // Method to trigger button click events
    public static void ButtonClick(ButtonsName buttonName)
    {
        OnButtonClicked?.Invoke(buttonName);
    }
}
